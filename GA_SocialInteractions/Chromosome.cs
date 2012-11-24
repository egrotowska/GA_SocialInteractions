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
            get { return gens.Count(); }
        }

        public Chromosome(int size)
        {
            this.gens = new bool[size];
            do
            {
                for (int j = 0; j < size; j++)
                {
                    this.gens[j] = Convert.ToBoolean(GA_GT.random.Next() % 2);
                }
            } while (!IsFeasible());
        }

        public bool IsFeasible()
        {
            // TODO: wszystko
            return true;
        }
    }
}
