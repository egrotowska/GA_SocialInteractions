using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions {
    class Population {
        public List<Individual> population;

        public int Count 
        {
            get { return population.Count; }
        }

        public Population() 
        {
            this.population = new List<Individual>();
        }

        public void Evaluation() 
        {
            List<int> unused = new List<int>();

            for (int i = 0; i < Count; i++)
            {
                unused.Add(i);
            }

            for (int i = 0; i < Count; i++)
            {
                int randomIndex = unused.ElementAt(GA_GT.random.Next() % unused.Count);
                unused.RemoveAt(randomIndex);

                double value = getIndividual(i).FitnessValue(getIndividual(i).chromosome, getIndividual(i).strategie, getIndividual(randomIndex).strategie);
                Individual temp = new Individual(getIndividual(i).chromosome, getIndividual(i).strategie, value);
                population[i] = temp;
            }
        }

        public void RandomPopulation(double cheaterRate, int chromosomeSize, int populationSize)
        {
            int numberOfCheaters = (int)(populationSize * cheaterRate);

            for (int i = 0; i < populationSize; i++) {
                Chromosome chromosome = new Chromosome(chromosomeSize);

                Individual temp;
                if (i < numberOfCheaters) 
                    temp = new Individual(chromosome, false, (double) 0);   // tworzymy cheatera
                else
                    temp = new Individual(chromosome, true, (double) 0);    // tworzymy cooperatora

                population.Add(temp);
            }
        }


        public Population TournamentSelection() {
            Population parents = new Population();

            while (parents.Count != Count) {
                int x = GA_GT.random.Next() % Count;
                int y = GA_GT.random.Next() % Count;

                while (y == x)
                {
                    y = GA_GT.random.Next() % Count;
                }

                if (getIndividual(x).fitness > getIndividual(y).fitness) 
                {
                    parents.Add(getIndividual(x));
                } else 
                {
                    parents.Add(getIndividual(y));
                }
            }
            return parents;
        }

        public Population TwoPointsCrossover(Population parents) {
            Population offspring = new Population();
            List<int> used = new List<int>();

            int[] permutation = new int[parents.Count];

            for (int i = 0; i < parents.Count; i++) 
            {
                permutation[i] = i;
            }

            //random permutation to generate random pairs 
            for (int i = 0; i < parents.Count; i++) 
            {                                           
                int randomValue = GA_GT.random.Next() % parents.Count - 1; //it should be 0 to Count-1
                
                int temp = permutation[i];
                permutation[i] = permutation[randomValue];
                permutation[randomValue] = temp;
            }
            //random pairs of indexes are perm[0] and perm[count-1], perm[1] and perm[count-2] and so on
            //TODO: crossover
            return offspring;
        }
        
        public void Mutation() {
            // TODO: wszystko
        }

        public Individual getIndividual(int i)
        {
            return population[i];
        }

        public void Add(Individual individual) {
            population.Add(individual);
        }

        public void Clear() {
            population.Clear();
        }

        public void Sort() {
            population.Sort(Compare);
        }

        int Compare(Individual x, Individual y) {
            if (x.fitness > y.fitness)
                return 1;
            else if (x.fitness == y.fitness)
                return 0;
            else
                return -1;
        }
    }
}
