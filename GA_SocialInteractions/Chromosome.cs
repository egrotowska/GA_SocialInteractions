using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions {
    class Chromosome {

        bool[] gens;
        bool ifCheater;

        public int Count
        {
            get { return gens.Length; }
        }

        // random chromosome
        public Chromosome(int size)
        {
            this.gens = new bool[size];

            for (int j = 0; j < size; j++)
            {
                this.gens[j] = Convert.ToBoolean(GA_GT.random.Next() % 2);
            }
        }

        // feasible chromosome
        public Chromosome(int size, Knapsack knapsack)
        {
            this.gens = new bool[size];
            do
            {
                for (int j = 0; j < size; j++)
                {
                    this.gens[j] = Convert.ToBoolean(GA_GT.random.Next() % 2);
                }
            } while (!IsFeasible(knapsack));
        }

        public bool IsFeasible(Knapsack knapsack)
        {
            for (int m = 0; m < knapsack.NumberOfKnapsacks; m++)
            {
                int sum = 0;

                for (int i = 0; i < gens.Length; i++)
                {
                    sum += gens[i] ? knapsack.GetWeight(m, i) : 0;
                }

                if (sum > knapsack.GetConstraint(m))
                {
                    return false;
                }
            }

            return true;
        }

        public bool getGen(int i)
        {
            return gens[i];
        }

        // instead of getGen(int i) we could use the indexer:
        /*
        public bool this[int i]
        {
	        get { return gens[i]; }
        }
        */
        // and then:
        // Chromosome c = new ...
        // bool b = c[5];
        // I don't know if it's pretty so I leave the choice to you ;)

        public void MutateGene(int i)
        {
            gens[i] = !gens[i];
        }
    }
}
