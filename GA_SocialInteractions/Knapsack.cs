using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions
{
    class Knapsack
    {
        int numberOfKnapsacks;
        int numberOfWeights;
        public int NumberOfWeights
        {
            get { return numberOfWeights; }
        }

        public int NumberOfKnapsacks
        {
            get { return numberOfKnapsacks; }
        }
        int[][] weights;            // wagi przedmiotow
        int[] constraints;          // ograniczenia wagowe plecakow

        public Knapsack(int[][] weights, int[] constraints)
        {
            this.weights = weights;

            this.numberOfWeights = weights[0].Length;
            this.numberOfKnapsacks = weights.Length;

            for (int i = 0; i < numberOfKnapsacks; i++)
            {
                if (weights[i].Length != numberOfWeights)
                    throw new Exception("Argument exception: weights");
            }

            this.constraints = constraints;

            if (constraints.Length != numberOfKnapsacks)
                throw new Exception("Argument exception: constraints");
        }

        public int GetWeight(int i, int j)
        {
            return weights[i][j];
        }

        public int GetConstraint(int i)
        {
            return constraints[i];
        }
    }
}
