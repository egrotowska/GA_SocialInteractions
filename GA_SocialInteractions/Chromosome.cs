using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions {
    class Chromosome {

        bool[] genes;

        public int Count
        {
            get { return genes.Length; }
        }

        public Chromosome(int size)
        {
            this.genes = new bool[size];
            do
            {
                for (int j = 0; j < size; j++)
                {
                    this.genes[j] = Convert.ToBoolean(GA_GT.random.Next() % 2);
                }
            } while (!IsFeasible());
        }

        public bool IsFeasible()
        {
            for (int m = 0; m < GA_GT.knapsack.NumberOfKnapsacks; m++)
            {
                int sum = 0;

                for (int i = 0; i < genes.Length; i++)
                {
                    sum += genes[i] ? GA_GT.knapsack.GetWeight(m, i) : 0;
                }

                if (sum > GA_GT.knapsack.GetConstraint(m))
                {
                    return false;
                }
            }

            return true;
        }

        public bool this[int i]
        {
	        get { return genes[i]; }
        }
        
        public void MutateGene(int i)
        {
            genes[i] = !genes[i];
        }
    }
}
