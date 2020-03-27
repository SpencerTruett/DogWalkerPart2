using PawsitivelyBestDogWalkerPart2.Data;
using PawsitivelyBestDogWalkerPart2.Models;
using System;
using System.Collections.Generic;

namespace PawsitivelyBestDogWalkerPart2
{
    class Program
    {
        static void Main(string[] args)
        {
            WalkerRepository walkerRepo = new WalkerRepository();
            Console.WriteLine("Getting All Walkers:"); ;

            Console.WriteLine();

            List<Walker> allWalkers = walkerRepo.GetAllWalkers();

            foreach (Walker wkr in allWalkers)
            {
                Console.WriteLine($"{wkr.Id}: {wkr.Name}, {wkr.Neighborhood}");

                Console.WriteLine("----------------------------");
                Console.WriteLine("Getting Walker with Id 1");

                Walker singleWalker = walkerRepo.GetWalkerById(1);

                Console.WriteLine($"{singleWalker.Id} {singleWalker.Name}");
            }

        }
    }
}
