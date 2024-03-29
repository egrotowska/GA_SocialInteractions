﻿using System;
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

        public void Evaluation()
        {
            List<int> unused = new List<int>();

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

                double value1 = getIndividual(i).FitnessValue(getIndividual(randomIndex), GA_GT.knapsackList);
                double value2 = getIndividual(randomIndex).FitnessValue(getIndividual(i), GA_GT.knapsackList);

                population[i].fitness = value1;
                population[randomIndex].fitness = value2;
            }
        }

        public void RandomPopulation(double cheaterRate, int chromosomeSize, int populationSize)
        {
            int numberOfCheaters = (int)(populationSize * cheaterRate);

            for (int i = 0; i < populationSize; i++)
            {
                Chromosome chromosome = new Chromosome(chromosomeSize);

                Individual temp;
                if (i < numberOfCheaters)
                    temp = new Individual(chromosome, false, (double)0, true);   // cheater
                else
                    temp = new Individual(chromosome, true, (double)0, true);    // cooperator

                population.Add(temp);
            }

        }


        public Population TournamentSelection() 
        {
            Population parents = new Population();
            int nonFeasibleAllowed = (int)(Count * (1.0 - GA_GT.feasibleRate));
            int nonFeasibleInParents = 0;

            while (parents.Count != Count) {

                int x = GA_GT.random.Next() % Count;
                int y = GA_GT.random.Next() % Count;

                while (y == x)
                {
                    y = GA_GT.random.Next() % Count;
                }

                Individual chosen = null;
                if (getIndividual(x).fitness > getIndividual(y).fitness) 
                {
                    chosen = getIndividual(x);
                } 
                else 
                {
                    chosen = getIndividual(y);
                }

                if (!chosen.isFeasible)
                {
                    if (nonFeasibleInParents == nonFeasibleAllowed)
                    {
                        chosen = GetBestFeasible();
                    }
                    else
                    {
                        nonFeasibleInParents++;
                    }
                }

                parents.Add(chosen);
            }
            return parents;
        }

        public Individual GetBestFeasible()
        {
            this.Sort();

            for (int i = 0; i < Count; i++)
            {
                if (population[i].isFeasible)
                    return population[i];
            }

            throw new Exception("There is not a single feasible individual in the population");
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
            // we forgot about crossover probability
            if (GA_GT.random.NextDouble() < GA_GT.crossoverRate)
            {
                for (int i = index1; i <= index2; i++)
                {
                    bool temp = ind1.chromosome[i];
                    ind1.chromosome[i] = ind2.chromosome[i];
                    ind2.chromosome[i] = temp;
                }
            }
            return new Tuple<Individual, Individual>(ind1, ind2); 
        }

        protected Population crossoverHelper(Population parents, int random_gens1, int random_gens2, int[] permutation) {
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

        // TODO: Maybe random_gens should be randomize for each pair separately?
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

        public Population UniformCrossover(Population parents)
        {
            
            Population offspring = new Population();
            List<int> unused = new List<int>();

            for (int i = 0; i < parents.Count; i++)
            {
                unused.Add(i);
            }

            int chromosomeSize = getChromosomeSize();

            for (int i = 0; i < parents.Count; i++)
            {
                if (!unused.Contains(i))
                    continue;

                unused.Remove(i);

                int randomIndex = unused.ElementAt(GA_GT.random.Next() % unused.Count);
                unused.Remove(randomIndex);

                if (GA_GT.random.NextDouble() >= GA_GT.crossoverRate)
                {
                    offspring.Add(parents.getIndividual(i));
                    offspring.Add(parents.getIndividual(randomIndex));
                }

                else
                {
                    Chromosome ch1 = new Chromosome(chromosomeSize);
                    Chromosome ch2 = new Chromosome(chromosomeSize);

                    for (int j = 0; j < chromosomeSize; j++)
                    {
                        if (GA_GT.random.NextDouble() < 0.5)
                        {
                            ch1[j] = parents.getIndividual(i)[j];
                            ch2[j] = parents.getIndividual(randomIndex)[j];
                        }
                        else
                        {
                            ch1[j] = parents.getIndividual(randomIndex)[j];
                            ch2[j] = parents.getIndividual(i)[j];
                        }
                    }

                    Individual ind1 = new Individual(ch1);
                    ind1.strategy = parents.getIndividual(i).strategy;
                    ind1.Update(GA_GT.knapsackList);

                    Individual ind2 = new Individual(ch2);
                    ind2.strategy = parents.getIndividual(randomIndex).strategy;
                    ind2.Update(GA_GT.knapsackList);

                    offspring.Add(ind1);
                    offspring.Add(ind2);
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
                    if (GA_GT.random.NextDouble() < GA_GT.mutationRate)
                    {
                        population[i].MutateGene(j);
                    }
                }
                population[i].Update(GA_GT.knapsackList);
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
            Individual i = new Individual(individual);
            population.Add(i);
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
                Console.Write(i + ") ");  population[i].Show();
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

        public void RemoveRange(int index, int count)
        {
            this.population.RemoveRange(index, count);
        }

        public void AddRange(Population population)
        {
            this.population.AddRange(population.population);
        }

        public int NumberOfFeasible
        {
            get
            {
                int nof = 0;

                foreach (Individual i in population)
                {
                    if (i.isFeasible)
                        nof++;
                }

                return nof;
            }
        }
    }
}


