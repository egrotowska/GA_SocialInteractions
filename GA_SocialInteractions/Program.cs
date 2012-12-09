using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_SocialInteractions.tests;

namespace GA_SocialInteractions
{
    class Program
    {
        static void Main(string[] args)
        {
            TestPopulation t = new TestPopulation();

            string path = @"..\..\samples\MK250.txt";
            KnapsackList knapsackList = InputOutput.ReadInput(path);

            init_static_GA_GT(knapsackList);

            GA_GT ga_gt = new GA_GT();
            
            ga_gt.RunGA_GT().Show(); 

            Console.ReadLine(); 
        }

        static void init_static_GA_GT(KnapsackList knapsackList)
        {
            GA_GT.gameModel = new PrisonersDilemma();
            GA_GT.knapsackList = knapsackList;
            GA_GT.weightGA = 0.8;
            GA_GT.weightGT = 0.2;
            GA_GT.cheatingDegree = 50;
            GA_GT.cheaterRate = 0.1;
            GA_GT.crossoverRate = 0.75;

            GA_GT.mutationRate = 1.0 / GA_GT.chromosomeLength;

            GA_GT.numberOfEpochs = 100;
            GA_GT.chromosomeLength = InputOutput.NUMBER_OF_OBJECTS;
            GA_GT.populationSize = 50;
        }
    }
}
