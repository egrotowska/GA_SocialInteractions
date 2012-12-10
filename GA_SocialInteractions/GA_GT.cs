﻿using System;
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
            Console.WriteLine("blabla {0} {1} {2} ", cheaterRate, chromosomeLength, populationSize);
            knapsackList.getKnapsack(0).Show();
            population = new Population();
            population.RandomPopulation(cheaterRate, chromosomeLength, populationSize, knapsackList.getKnapsack(0));

            maxFitness = 1.0;
            maxPayoff = gameModel.maxPayoff;

            maxFitness = population.Evaluation(knapsackList.getKnapsack(0));
            for (int epoch = 0; epoch < numberOfEpochs; epoch++)
            {

                population.Sort();
                for (int i = 0; i < population.Count; i++)
                {
                    population.getIndividual(i).ShowFitnessAndStrategy();
                }
                Console.ReadLine();

                Population parents = population.TournamentSelection();

                Population offspring = population.TwoPointsCrossover(population);

                parents.Sort();
                offspring.Sort();

                population.Clear();

                for (int i = 0; i < populationSize / 2; i++)
                {
                    population.Add(parents.getIndividual(i));
                    population.Add(offspring.getIndividual(i));
                }

                population.Mutation();
                maxFitness = population.Evaluation(knapsackList.getKnapsack(0));
            }

            population.Sort();
            return population.getIndividual(0);
        }
    }
}
