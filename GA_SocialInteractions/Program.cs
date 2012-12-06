using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_SocialInteractions.tests;
using GA_SocialInteractions.knapsacks;

namespace GA_SocialInteractions
{
    class Program
    {
        static void Main(string[] args)
        {
            TestPopulation t = new TestPopulation();

            int numberOfObjects = 10;
            int constraint = 165;

            Knapsack knapsack = (new KnapsackSample(numberOfObjects, constraint)).knapsack;
            init_static_GA_GT(knapsack, numberOfObjects);
           knapsack.Show();
            Console.ReadLine();

            GA_GT ga_gt = new GA_GT();
            
            ga_gt.RunGA_GT().Show(); 
            Console.Read(); 
        }

        static void init_static_GA_GT(Knapsack knapsack, int numberOfObjects)
        {
            GA_GT.numberOfEpochs = 1000;
            GA_GT.chromosomeLength = numberOfObjects;
            GA_GT.populationSize = 500;

            GA_GT.gameModel = new PrisonersDilemma();
            GA_GT.knapsack = knapsack;
            GA_GT.weightGA = 0.8;
            GA_GT.weightGT = 0.2;
            GA_GT.cheatingDegree = 50;
            GA_GT.cheaterRate = 0.1;
            GA_GT.crossoverRate = 0.75;

            GA_GT.mutationRate = 1.0 / GA_GT.chromosomeLength;

            Console.Read();
        }

    }
}
