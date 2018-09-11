using Kloc.Common.Blockchaining;
using RandomNameGeneratorLibrary;
using System;
using System.Linq;

namespace BlockChainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var chain = new Blockchain<Vote>();
            var numberGenerator = new Random();
            var personGenerator = new PersonNameGenerator(numberGenerator);

            var candidates = personGenerator.GenerateMultipleFirstAndLastNames(5).Select(e => new Candidate(e)).ToList();

            for (var x = 0; x < 100000; x++)
            {
                var vote = new Vote(new Voter(personGenerator.GenerateRandomFirstAndLastName()), candidates[numberGenerator.Next(candidates.Count)]);
                chain.Add(vote);
            }

            //Console.WriteLine("ID\tPrevious Hash\t\t\t\t\tHash\t\t\t\t\t\t\tTimestamp");
            var totals = new int[candidates.Count];
            foreach (var block in chain.Chain)
            {
                if (block.Data != null)
                {
                    totals[candidates.IndexOf(block.Data.Candidate)]++;
                }
                //Console.WriteLine($"{block.Id}\t{block?.PreviousHash ?? "",-44}\t{block.Hash,-44}\t{block.Timestamp}\t{block.Data?.Name ?? ""}:{block.Data?.Candidate ?? ""}");
            }
            for (var x = 0; x < candidates.Count; x++)
            {
                Console.WriteLine($"{candidates[x].FullName,-30}\t{totals[x]:#,#}");
            }
            Console.WriteLine();
            Console.WriteLine($"Has blockchain been tampered with? {!chain.IsValid()}");

            Console.ReadKey();
        }
    }

    [Serializable]
    public class Vote
    {
        public Vote(Voter voter, Candidate candidate)
        {
            Voter = voter;
            Candidate = candidate;
        }

        public Voter Voter { get; private set; }
        public Candidate Candidate { get; private set; }
    }

    [Serializable]
    public class Voter
    {
        public Voter(string fullName)
        {
            FullName = fullName;
        }

        public string FullName { get; private set; }
    }

    [Serializable]
    public class Candidate
    {
        public Candidate(string fullName)
        {
            FullName = fullName;
        }

        public string FullName { get; private set; }
    }
}
