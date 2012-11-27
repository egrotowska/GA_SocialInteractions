using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions {
    class Population {
        private List<Individual> population;

        public int Count 
        {
            get { return population.Count; }
        }

        public Population() 
        {
            this.population = new List<Individual>();
        }

        public Population(Population p)
        {
            this.population = new List<Individual>(p.population);
        }

        public double Evaluation() 
        {
            List<int> unused = new List<int>();
            double newMax = GA_GT.maxFitness;

            for (int i = 0; i < Count; i++)
            {
                unused.Add(i);
            }

            for (int i = 0; i < Count; i++)
            {
                if (!unused.Contains(i))    // each individual will be paired only once
                    continue;

                unused.Remove(i);           // we won't pair individual with itself

                int randomIndex = unused.ElementAt(GA_GT.random.Next() % unused.Count);
                unused.Remove(randomIndex);

                double value = getIndividual(i).FitnessValue(getIndividual(i).chromosome, getIndividual(i).strategy, getIndividual(randomIndex).strategy);
                if (value > newMax)
                {
                    newMax = value;
                }
                population[i] = new Individual(getIndividual(i).chromosome, getIndividual(i).strategy, value);
            }

            return newMax;
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

        // method does nothing because swap doesn't work as it should
        public Population TwoPointsCrossover(Population parents) {
            Population offspring = new Population(parents);
            List<int> used = new List<int>();

            int[] permutation = new int[parents.Count];

            for (int i = 0; i < parents.Count; i++) 
            {
                permutation[i] = i;
            }

            //random permutation to generate random pairs 
            for (int i = 0; i < parents.Count; i++) 
            {                                           
                int randomValue = GA_GT.random.Next() % parents.Count;
                
                int temp = permutation[i];
                permutation[i] = permutation[randomValue];
                permutation[randomValue] = temp;
            }
            
            int random_gens1 = GA_GT.random.Next() % parents.getChromosomeSize();
            int random_gens2 = GA_GT.random.Next() % parents.getChromosomeSize();

            while (random_gens1 == random_gens2) {
                random_gens2 = GA_GT.random.Next() % parents.getChromosomeSize();
            }

            if (random_gens2 < random_gens1)
            {
                Swap<int>(random_gens1, random_gens2);
            }

            for (int i = 0; i < parents.Count / 2; i++)
            {
                for (int j = random_gens1; j < random_gens2; j++)
                {
                    Swap<bool>(offspring.getIndividual(permutation[i])[j], offspring.getIndividual(permutation[offspring.Count - 1 - i])[j]);
                }
            }

            return offspring;
        }
        
        public void Mutation() 
        {
            int chromosomeSize = getChromosomeSize();

            for (int i = 0; i < population.Count; i++)
            {
                for (int j = 0; j < chromosomeSize; j++)
                {
                    if (GA_GT.random.NextDouble() < 0.5)  // should be: < mutationRate, but we don't have the access to the variable
                        population[i].MutateGene(j);
                }
            }
        }

        public Individual getIndividual(int i)
        {
            if (population.Count < i + 1)
                throw new IndexOutOfRangeException("Population.getIndividual");

            return population[i];
        }

        public int getChromosomeSize()
        {
            if (population.Count < 1)
                throw new IndexOutOfRangeException("Population.getChromosomeSize");

            return population[0].chromosome.Count;
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

        // does nothing ?
        static void Swap<T>(T lhs, T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        public void Show()
        {
            for (int i = 0; i < population.Count; i++)
            {
                population[i].Show();
            }
            Console.WriteLine();
        }
    }
}


