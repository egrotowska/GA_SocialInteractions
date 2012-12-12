using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions
{
    class GA_GT
    {
        public static int numberOfEpochs;         // liczba epok
        public static int chromosomeLength;       // dlugosc chromosomu (liczba przedmiotow)
        public static int populationSize;         // liczebnosc populacji
        public static Game gameModel;             // model z teorii gier

        public static KnapsackList knapsackList;
                                                  // w paperze oznaczone:
        public static double weightGA;            // beta_GA
        public static double weightGT;            // beta_GT
        public static double cheatingDegree;      // tau
        public static double cheaterRate;         // alfa
        public static double crossoverRate;       // p_c
        public static double mutationRate;        // p_m

        public static double maxFitness;          // f^max
        public static double maxPayoff;           // delta f^max

        Population population;

        public static Random random = new Random();

        public Individual RunGA_GT()
        {
            population = new Population();
            population.RandomPopulation(cheaterRate, chromosomeLength, populationSize);

            maxFitness = knapsackList.MaxFitness();     //maxFitness only calculated once (sum of the values of the objects)
            maxPayoff = gameModel.maxPayoff;

            population.Evaluation();

            for (int epoch = 0; epoch < numberOfEpochs; epoch++)
            {
                population.Sort();
                population.getIndividual(0).ShowFitness();
                
                population.Show();
                Console.ReadLine();

                Population parents = population.TournamentSelection();
                parents.Show();
                Console.ReadLine();
                Population offspring = population.UniformCrossover(parents);//population.TwoPointsCrossover(parents);
                offspring.Show();
                Console.ReadLine();

                parents.Sort();
                offspring.Sort();

                // Another way of choosing the new generation:
                //population.Clear();
                //population.AddRange(parents);
                //population.AddRange(offspring);
                //population.Sort();
                //population.RemoveRange(populationSize, population.Count - populationSize);

                // TODO: number of cheaters should be less than a cheater rate? or some other value?
                population.Clear();
                for (int i = 0; i < populationSize / 2; i++)
                {
                    population.Add(parents.getIndividual(i));
                    population.Add(offspring.getIndividual(i));
                }

                population.Mutation();
                population.Evaluation();

            }

            population.Sort();
            return population.getIndividual(0);
        }
    }
}
