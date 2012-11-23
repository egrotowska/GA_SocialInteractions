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
            int knapsackSize = 10;
            int weightsSize = 5;

            int[][] weights = new int[knapsackSize][];
            int[] constraints = new int[knapsackSize];
            
            for (int i = 0; i < knapsackSize; i++)
            {
                weights[i] = new int[weightsSize];
                for (int j = 0; j < weightsSize; j++)
                {
                    weights[i][j] = j * i + i - j;
                }
            }
            for (int i = 0; i < knapsackSize; i++)
            {
                constraints[i] = i;
            }
            
            Knapsack knapsack = new Knapsack(weights, constraints);
            GA_GT ga_gt = new GA_GT(10, knapsack, 1, 1, 1, 1, 1, 1, 1, 1);
            ga_gt.RunGA_GT();
            Console.Read();
        }
    }
}
