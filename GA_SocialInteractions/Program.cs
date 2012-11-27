using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions
{
    class Program
    {
        static void Main(string[] args) // it's random code with random numbers :P
        {
            int numberOfObjects = 10;
            int numberOfKnapsacks = 1;

            int[][] weights = new int[numberOfKnapsacks][];
            int[] values = new int[numberOfObjects];
            int[] constraints = new int[numberOfKnapsacks];
            
            for (int i = 0; i < numberOfKnapsacks; i++)
            {
                weights[i] = new int[numberOfObjects];

                for (int j = 0; j < numberOfObjects; j++)
                {
                    weights[i][j] = j + 1;
                }

                constraints[i] = 15;
            }

            for (int i = 0; i < numberOfObjects; i++)
            {
                values[i] = 2 * i;
            }
            
            Knapsack knapsack = new Knapsack(weights, values, constraints);
            GA_GT ga_gt = new GA_GT();

            GA_GT.numberOfEpochs = 100;
            GA_GT.chromosomeLength = 10;
            GA_GT.populationSize = 10;
            GA_GT.gameModel = new PrisonersDilemma();
            GA_GT.knapsack = knapsack;
            GA_GT.weightGA = 0.1;
            GA_GT.weightGT = 0.1;
            GA_GT.cheatingDegree = 0.1;
            GA_GT.cheaterRate = 0.1;
            GA_GT.crossoverRate = 0.1;
            GA_GT.mutationRate = 0.1;

            ga_gt.RunGA_GT();
            Console.Read();
        }
    }
}
