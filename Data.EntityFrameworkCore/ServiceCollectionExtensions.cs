using Kloc.Common.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Kloc.Common.Data.EntityFrameworkCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository<TContext, TRepositoryService, TRepositoryImplementation, TAggregateRoot>(this IServiceCollection services)
            where TContext : DbContext
            where TRepositoryImplementation : RepositoryBase<TAggregateRoot, Guid>, TRepositoryService
            where TRepositoryService : class, IRepository<TAggregateRoot, Guid>
            where TAggregateRoot : class, IAggregateRoot<Guid>
        {
            services.AddRepository<TContext, TRepositoryService, TRepositoryImplementation, TAggregateRoot, Guid>();

            return services;
        }

        public static IServiceCollection AddRepository<TContext, TRepositoryService, TRepositoryImplementation, TAggregateRoot, TKey>(this IServiceCollection services)
            where TContext : DbContext
            where TRepositoryImplementation : RepositoryBase<TAggregateRoot, TKey>, TRepositoryService
            where TRepositoryService : class, IRepository<TAggregateRoot, TKey>
            where TAggregateRoot : class, IAggregateRoot<TKey>
        {

            services.AddScoped<TRepositoryService, TRepositoryImplementation>();
            services.AddScoped(provider => provider.GetService<TContext>().Set<TAggregateRoot>());

            return services;
        }

        public static IServiceCollection AddUnitOfWork<TContext, TUnitOfWorkService, TUnitOfWorkImplementation>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
            where TContext : DbContext
            where TUnitOfWorkService : class, IUnitOfWork
            where TUnitOfWorkImplementation : UnitOfWorkBase, TUnitOfWorkService
        {
            services.AddDbContext<TContext>(optionsAction);
            services.AddScoped<TUnitOfWorkService, TUnitOfWorkImplementation>();

            return services;
        }
    }
}
