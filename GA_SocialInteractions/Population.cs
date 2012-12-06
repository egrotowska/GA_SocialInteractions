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

        public Population(List<Individual> p)
        {
            this.population = p;
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
                if (!unused.Contains(i))
                    continue;

                unused.Remove(i);

                int randomIndex = unused.ElementAt(GA_GT.random.Next() % unused.Count);
                unused.Remove(randomIndex);

                double value = getIndividual(i).FitnessValue(getIndividual(i).chromosome);
              //  double value = getIndividual(i).FitnessValue(getIndividual(i).chromosome, getIndividual(i).strategy, getIndividual(randomIndex).strategy);

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
                    temp = new Individual(chromosome, false, (double) 0);   // cheater
                else
                    temp = new Individual(chromosome, true, (double) 0);    // cooperator

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

        private int[] generateRandomPairs(int numberOfIndividuals)
        {
            int[] permutation = new int[numberOfIndividuals];

            for (int i = 0; i < numberOfIndividuals; i++)
            {
                permutation[i] = i;
            }

            for (int i = 0; i < numberOfIndividuals; i++)
            {
                int randomValue = GA_GT.random.Next() % numberOfIndividuals;

                int temp = permutation[i];
                permutation[i] = permutation[randomValue];
                permutation[randomValue] = temp;
            }

            return permutation;
        }

        protected Tuple<Individual, Individual> crossover(Individual ind1, Individual ind2, int index1, int index2)
        {
            for (int i = index1; i <= index2; i++)
            {
                bool temp = ind1.chromosome[i];
                ind1.chromosome[i] = ind2.chromosome[i];
                ind2.chromosome[i] = temp;
            }
            return new Tuple<Individual, Individual>(ind1, ind2); 
        }

        protected Population crossoverHelper(Population parents, int random_gens1, int random_gens2, int[] permutation) {
            //some magic to have right order in offspring -> offsprings1 for permutations from 0 to parents.count/2, offsprings2 for rest
            List<Individual> offsprings1 = new List<Individual>();
            List<Individual> offsprings2 = new List<Individual>();
            List<int> used = new List<int>();

            if (random_gens2 < random_gens1)
            {
                int temp = random_gens1;
                random_gens1 = random_gens2;
                random_gens2 = temp;
            }

            for (int i = 0; i < parents.Count / 2; i++)
            {
                Tuple<Individual, Individual> children = crossover(parents.getIndividual(permutation[i]), parents.getIndividual(permutation[parents.Count - 1 - i]), random_gens1, random_gens2);
                offsprings1.Add(children.Item1);
                offsprings2.Add(children.Item2);
            }
            offsprings2.Reverse(0, offsprings2.Count);
            offsprings1.AddRange(offsprings2);
            Population offspring = new Population(offsprings1);
            return offspring;
        }

        public Population TwoPointsCrossover(Population parents)
        {
            int[] permutation = generateRandomPairs(parents.Count);
            int random_gens1 = GA_GT.random.Next() % parents.getChromosomeSize();
            int random_gens2 = GA_GT.random.Next() % parents.getChromosomeSize();

            while (random_gens1 == random_gens2)
            {
                random_gens2 = GA_GT.random.Next() % parents.getChromosomeSize();
            }
            return crossoverHelper(parents, random_gens1, random_gens2, permutation);
        }
        
        public void Mutation() 
        {
            int chromosomeSize = getChromosomeSize();

            for (int i = 0; i < population.Count; i++)
            {
                for (int j = 0; j < chromosomeSize; j++)
                {
                    if (GA_GT.random.NextDouble() < GA_GT.mutationRate) 
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

        public void Show()
        {
            for (int i = 0; i < population.Count; i++)
            {
                Console.Write(i + " ");  population[i].Show();
            }
            Console.WriteLine();
        }

        int Compare(Individual x, Individual y) {
            if (x.fitness > y.fitness)
                return -1;
            else if (x.fitness == y.fitness)
                return 0;
            else
                return 1;
        }
    }
}


