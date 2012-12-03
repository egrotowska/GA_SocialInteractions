using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions
{
    class Program
    {
        static void Main(string[] args)
        {
            // solution: 1 1 1 1 0 1 0 0 0 0 0
            // sum of values = 309

            int numberOfObjects = 10;
            int numberOfKnapsacks = 1;

            int[][] weights = new int[numberOfKnapsacks][];
            int[] values = new int[numberOfObjects];
            int[] constraints = new int[numberOfKnapsacks];

            weights[0] = new int[numberOfObjects];
            weights[0][0] = 23;
            weights[0][1] = 31;
            weights[0][2] = 29;
            weights[0][3] = 44;
            weights[0][4] = 53;
            weights[0][5] = 38;
            weights[0][6] = 63;
            weights[0][7] = 85;
            weights[0][8] = 89;
            weights[0][9] = 82;

            values[0] = 92;
            values[1] = 57;
            values[2] = 49;
            values[3] = 68;
            values[4] = 60;
            values[5] = 43;
            values[6] = 67;
            values[7] = 54;
            values[8] = 87;
            values[9] = 72;

            constraints[0] = 165;
                        
            Knapsack knapsack = new Knapsack(weights, values, constraints);
            GA_GT ga_gt = new GA_GT();

            GA_GT.numberOfEpochs = 100;
            GA_GT.chromosomeLength = numberOfObjects;
            GA_GT.populationSize = 10;
            GA_GT.gameModel = new PrisonersDilemma();
            GA_GT.knapsack = knapsack;
            GA_GT.weightGA = 0.8;
            GA_GT.weightGT = 0.2;
            GA_GT.cheatingDegree = 50;
            GA_GT.cheaterRate = 0.1;
            GA_GT.crossoverRate = 0.75;
            GA_GT.mutationRate = 1 / GA_GT.chromosomeLength;

            ga_gt.RunGA_GT();
            Console.Read();
        }
    }
}
