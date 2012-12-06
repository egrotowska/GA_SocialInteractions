using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions.knapsacks
{
    class KnapsackSample
    {
        Knapsack knapsack_;
        public Knapsack knapsack
        {
            get { return knapsack_; }
            set { knapsack_ = value; }
        }

        int constraint;
        int[] weights;
        int[] values;

        public KnapsackSample(int size, int constraint)
        {
            this.constraint = constraint;
            generateValues(size);
            generateWeights(size);
            knapsack_ = new Knapsack(weights, values, constraint);
        }

        private void generateWeights(int size)
        {
            weights = new int[size];
            for (int j = 0; j < size; j++)
            {
                weights[j] = j + 1;
            }
            
            //weights[0] = 23;
            //weights[1] = 31;
            //weights[2] = 29;
            //weights[3] = 44;
            //weights[4] = 53;
            //weights[5] = 38;
            //weights[6] = 63;
            //weights[7] = 85;
            //weights[8] = 89;
            //weights[9] = 82;

        }
            
        private void generateValues(int size) {
            values = new int[size];
            for (int i = 0; i < size; i++)
            {
                values[i] = i;
            }
            //values[0] = 92;
            //values[1] = 57;
            //values[2] = 49;
            //values[3] = 68;
            //values[4] = 60;
            //values[5] = 43;
            //values[6] = 67;
            //values[7] = 54;
            //values[8] = 87;
            //values[9] = 72;
        }
    }
}
