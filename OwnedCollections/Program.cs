using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using RandomNameGeneratorLibrary;
using System;
using System.Collections.Generic;

namespace OwnedCollections
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddDbContext<MyDbContext>(options => options.UseSqlServer("Server=(local);Database=OwnedCollections;Trusted_Connection=True;"));

            var numberGenerator = new Random();
            var personGenerator = new PersonNameGenerator(numberGenerator);

            using (var provider = services.BuildServiceProvider())
            {
                using (var db = provider.CreateScope().ServiceProvider.GetService<MyDbContext>())
                {
                    if(!await db.Parents.AnyAsync())
                    {
                        var parents = new List<Parent>();

                        for (var x = 0; x < 100; x++)
                        {
                            parents.Add(new Parent(Guid.NewGuid(), personGenerator.GenerateRandomFirstName(), personGenerator.GenerateRandomLastName()));
                        }

                        foreach (var parent in parents)
                        {
                            for (var x = 0; x < numberGenerator.Next(0, 6); x++)
                            {
                                new Child(parent, Guid.NewGuid(), personGenerator.GenerateRandomFirstName(), parent.LastName);
                            }
                        }

                        await db.Parents.AddRangeAsync(parents);
                        await db.SaveChangesAsync();
                    }
                }

                using (var db = provider.CreateScope().ServiceProvider.GetService<MyDbContext>())
                {
                    var parents = db.Parents;

                    foreach (var parent in parents)
                    {
                        Console.WriteLine($"{parent.FirstName} {parent.LastName}");

                        foreach (var child in parent.Children)
                        {
                            Console.WriteLine($"   {child.FirstName} {child.LastName}");
                        }

                        Console.WriteLine();
                    }
                }

            }
        }
    }

    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Parent> Parents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ParentConfiguration());
        }
    }

    public class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.HasKey(e => e.Id);
            builder.OwnsMany(e => e.Children, sa=> { sa.Property(e=>e. });
        }
    }

    public class Parent
    {
        private readonly HashSet<Child> children = new HashSet<Child>();

        public Parent(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        public IReadOnlyCollection<Child> Children => children;

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        internal void AddChild(Child child)
        {
            children.Add(child);
        }
    }

    public class Child
    {
        private Child(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        public Child(Parent parent, Guid id, string firstName, string lastName) : this(id, firstName, lastName)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            parent.AddChild(this);
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }

    public class DbFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            return new MyDbContext(new DbContextOptionsBuilder<MyDbContext>().UseSqlServer("Server=(local);Database=OwnedCollections;Trusted_Connection=True;").Options);
        }
    }
}
