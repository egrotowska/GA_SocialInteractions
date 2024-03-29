﻿using System;
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

        public Chromosome(Chromosome c)
        {
            genes = new bool[c.Count];

            for (int i = 0; i < c.Count; i++)
            {
                genes[i] = c[i];
            }
        }

        public Chromosome(int size)
        {
            this.genes = new bool[size];

            List<int> zeros = new List<int>();

            for (int i = 0; i < size; i++)
            {
                zeros.Add(i);
            }

            while (zeros.Count > 0)
            {
                int index = zeros[GA_GT.random.Next() % zeros.Count];

                this.genes[index] = true;

                if (!IsFeasible())
                {
                    this.genes[index] = false;
                    return;
                }

                zeros.Remove(index);
            }
        }

        public Chromosome(int size, Knapsack knapsack)
        {
            this.genes = new bool[size];

            List<int> zeros = new List<int>();

            for (int i = 0; i < size; i++)
            {
                zeros.Add(i);
            }

            while (zeros.Count > 0)
            {
                int index = zeros[GA_GT.random.Next() % zeros.Count];

                this.genes[index] = true;

                if (!IsFeasible(knapsack))
                {
                    this.genes[index] = false;
                    return;
                }

                zeros.Remove(index);
            }
        }

        public Chromosome(bool[] genes)
        {
            this.genes = genes;
        }



        public bool IsFeasible(Knapsack knapsack)
        {
            int sum = 0;

            for (int i = 0; i < genes.Length; i++)
            {
                sum += genes[i] ? knapsack.GetWeight(i) : 0;
            }

            if (sum > knapsack.constraint)
            {
                return false;
            }
            return true;
        }

        public bool IsFeasible()
        {
            foreach (Knapsack knapsack in GA_GT.knapsackList.knapsackList)
            {
                int sum = 0;
                for (int i = 0; i < genes.Length; i++)
                {
                    sum += genes[i] ? knapsack.GetWeight(i) : 0;
                }

                if (sum > knapsack.constraint)
                {
                    return false;
                }
            }
            return true;

        }

        public bool this[int i]
        {
	        get { return genes[i]; }
            set { genes[i] = value; }
        }
        
        public void MutateGene(int i)
        {
            genes[i] = !genes[i];
        }
    }
}
