using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions
{
    class GA_GT
    {
        int numberOfEpochs;         // liczba epok
        int chromosomeLength;       // dlugosc chromosomu (liczba przedmiotow)
        int populationSize;         // liczebnosc populacji
        int gameModel;              // model z teorii gier

        Knapsack knapsack;
                                    // w paperze oznaczone:
        double weightGA;            // beta_GA
        double weightGT;            // beta_GT
        double cheatingDegree;      // tau
        double cheaterRate;         // alfa
        double crossoverRate;       // p_c
        double mutationRate;        // p_m

        Population population;

        public static Random random = new Random(); // www.dotnetperls.com/random

        private GA_GT() { }

        //methods shouldn't have more than 5 arguments... Game class needed but now i don't know how it should look like
        public GA_GT(int epochs, Knapsack knapsack, int N, int gm, double wga, double wgt, double chd, double chr, double cr, double mr)
        {
            this.numberOfEpochs = epochs;
            this.knapsack = knapsack;
            this.chromosomeLength = knapsack.NumberOfWeights;

            this.populationSize = N;
            this.gameModel = gm;
            this.weightGA = wga;
            this.weightGT = wgt;
            this.cheatingDegree = chd;
            this.cheaterRate = chr;
            this.crossoverRate = cr;
            this.mutationRate = mr;
        }

        public Individual RunGA_GT()
        {
            population = new Population();
            population.RandomPopulation(cheaterRate, chromosomeLength, populationSize);

            for (int epoch = 0; epoch < numberOfEpochs; epoch++)
            {
                population.Evaluation();

                Population parents = population.TournamentSelection();
                Population offspring = population.TwoPointsCrossover(parents);

                parents.Sort();
                offspring.Sort();

                population.Clear();

                for (int i = 0; i < populationSize / 2; i++)
                {
                    population.Add(parents.getIndividual(i));
                    population.Add(offspring.getIndividual(i));
                }
                population.Mutation();
            }

            population.Sort();
            return population.getIndividual(0);
        }
    }
}
