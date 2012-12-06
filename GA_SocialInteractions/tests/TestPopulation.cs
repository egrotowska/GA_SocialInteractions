using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions.tests
{
    class TestPopulation : Population
    {
        public TestPopulation()
        {
            Console.WriteLine("testcrossoverPopulation : {0} ", testCrossoverPopulation());
            Console.WriteLine("crossoverTestIndividual : {0} ", testCrossoverIndividuals());
        }

        public bool testCrossoverPopulation()
        {
            Population parents = new Population();

            Chromosome[] chr_tab = { new Chromosome(new bool[] {false, false, false}), new Chromosome(new bool[] { true, true, true })};
            Individual[] ind_tab = new Individual[4];

            for (int i = 0; i < 4; i++)
            {
                ind_tab[i] = new Individual(chr_tab[i % 2]);
                parents.Add(ind_tab[i]);
            }
            
            int[] permutation = new int[] { 0, 2, 1, 3};
            int random1 = 2;
            int random2 = 0;

            Population orphans = this.crossoverHelper(parents, random1, random2, permutation);
            for (int i = 0; i < 4; i++)
            {
                if (i < 2)
                {
                    for (int j = 0; j < orphans.getIndividual(0).chromosome.Count; j++)
                    {
                        if (orphans.getIndividual(i).chromosome[j] != false) return false;
                    }
                }
                else
                {
                    for (int j = 0; j < orphans.getIndividual(0).chromosome.Count; j++)
                    {
                        if (orphans.getIndividual(i).chromosome[j] != true) return false;
                    }
                }
            }
            return true;
        }

        public bool testCrossoverIndividuals()
        {
            bool[] gens1 = new bool[] { false, true, false };
            bool[] gens2 = new bool[] { true, false, true };

            bool[] temp1 = new bool[] {false, true, false};
            bool[] temp2 = new bool[] { true, false, true };
            Individual i1 = new Individual(new Chromosome(gens1));
            Individual i2 = new Individual(new Chromosome(gens2));

            int start = 0;
            int stop = 1;

            Tuple<Individual, Individual> inds = this.crossover(i1, i2, start, stop);

            for (int i = start; i <= stop; i++)
            {
                if (i1.chromosome[i] != temp2[i] || i2.chromosome[i] != temp1[i]) return false;
            }
            return true;
        }
    }
}
