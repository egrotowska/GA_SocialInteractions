using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions
{
    class Knapsack
    {
        int numberOfKnapsacks;
        int numberOfObjects;
        public int NumberOfObjects
        {
            get { return numberOfObjects; }
        }

        public int NumberOfKnapsacks
        {
            get { return numberOfKnapsacks; }
        }
        int[][] weights;
        int[] constraints;
        int[] values;

        public Knapsack(int[][] weights, int[] values, int[] constraints)
        {
            this.weights = weights;
            this.numberOfObjects = weights[0].Length;
            this.numberOfKnapsacks = weights.Length;

            for (int i = 0; i < numberOfKnapsacks; i++)
            {
                if (weights[i].Length != numberOfObjects)
                    throw new ArgumentException("Knapsack constructor: weights");
            }

            this.values = values;

            if (values.Length != numberOfObjects)
                throw new ArgumentException("Knapsack constructor: values");

            this.constraints = constraints;

            if (constraints.Length != numberOfKnapsacks)
                throw new ArgumentException("Knapsack constructor: constraints");
        }

        public int GetWeight(int i, int j)
        {
            if (i + 1 > numberOfKnapsacks || j + 1 > numberOfObjects)
                throw new ArgumentOutOfRangeException("Knapsack.GetWeight");

            return weights[i][j];
        }

        public int GetValue(int i)
        {
            if(i + 1 > numberOfObjects)
                throw new ArgumentOutOfRangeException("Knapsack.GetValue");

            return values[i];
        }

        public int GetConstraint(int i)
        {
            if (i + 1 > numberOfKnapsacks)
                throw new ArgumentOutOfRangeException("Knapsack.GetConstraint");

            return constraints[i];
        }
    }
}
