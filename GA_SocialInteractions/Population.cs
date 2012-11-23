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

        public void Evaluation() {
            List<int> used = new List<int>();

            for (int i = 0; i < population.Count; i++) {
                if (used.Contains(i))
                    continue;

                used.Add(i);

                int z = 0;

                while (true) {
                    z = GA_GT.random.Next() % population.Count;

                    if (!used.Contains(z)) {
                        used.Add(z);
                        break;
                    }
                }
                double value = getIndividual(i).FitnessValue(getIndividual(i).chromosome, getIndividual(i).strategie, getIndividual(z).strategie);
                Individual temp = new Individual(getIndividual(i).chromosome, getIndividual(i).strategie, value);
                population[i] = temp;
            }
        }

        public void RandomPopulation(double cheaterRate, int chromosomeSize)
        {
            int i;
            int numberOfCheaters = (int)(population.Count * cheaterRate);

            for (i = 0; i < population.Count; i++) {
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

            while (parents.Count != population.Count) {
                int x = GA_GT.random.Next() % population.Count;
                int y = 0;

                while ((y = GA_GT.random.Next() % population.Count) == x)
                {
                }

                if (getIndividual(x).fitness > getIndividual(y).fitness) {
                    parents.Add(getIndividual(x));
                } else {
                    parents.Add(getIndividual(y));
                }
            }
            return parents;
        }

        public Population TwoPointsCrossover(Population parents) {
            Population offspring = new Population();
            List<int> used = new List<int>();

            for (int pair = 0; pair < parents.Count / 2; pair++) {
                int x, y;

                //wybierz rodzica x:
                while (true) {
                    x = GA_GT.random.Next() % parents.Count;

                    if (!used.Contains(x)) {
                        used.Add(x);
                        break;
                    }
                }

                //wybierz rodzica y:
                while (true) {
                    y = GA_GT.random.Next() % parents.Count;

                    if (!used.Contains(y)) {
                        used.Add(y);
                        break;
                    }
                }
                //TODO: wlasciwy crossover
            }
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
