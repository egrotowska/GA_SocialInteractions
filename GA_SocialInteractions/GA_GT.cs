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

        public static double feasibleRate;        // part of the main population (and parents poulation) that must be feasible

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

                Population parents = population.TournamentSelection();

                Population offspring = population.UniformCrossover(parents);//population.TwoPointsCrossover(parents);

                offspring.Mutation();

                parents.Sort();
                offspring.Sort();

                // Another way of choosing the new generation:
                //population.Clear();
                //population.AddRange(parents);
                //population.AddRange(offspring);
                //population.Sort();
                //population.RemoveRange(populationSize, population.Count - populationSize);

                //// TODO: number of cheaters should be less than a cheater rate? or some other value?
                //population.Clear();
                //for (int i = 0; i < populationSize; i++)
                //{
                //    //population.Add(parents.getIndividual(i));
                //    population.Add(offspring.getIndividual(i));
                //}

                population.Clear();
                int nonFeasibleAllowed = (int)(populationSize * (1.0 - feasibleRate));
                int nonFeasibleInPopulation = 0;

                for (int i = 0; i < offspring.Count; i++)
                {
                    if (!offspring.getIndividual(i).isFeasible)
                    {
                        if (nonFeasibleInPopulation == nonFeasibleAllowed)
                            continue;

                        else
                        {
                            population.Add(offspring.getIndividual(i));
                            nonFeasibleInPopulation++;
                        }
                    }

                    else
                    {
                        population.Add(offspring.getIndividual(i));
                    }
                }

                if (population.Count < populationSize)
                {
                    for (int i = 0; i < parents.Count; i++)
                    {
                        if (population.Count == populationSize)
                            break;

                        if (parents.getIndividual(i).isFeasible)
                            population.Add(parents.getIndividual(i));
                    }
                }

                population.GetBestFeasible().ShowValue();

                population.Evaluation();

            }

            population.Sort();
            population.Show();
            Console.ReadLine();
            return population.GetBestFeasible();
        }
    }
}
