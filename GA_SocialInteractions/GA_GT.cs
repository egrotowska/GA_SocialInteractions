using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions
{
    class GA_GT
    {
        int numberOfEpochs;         // liczba epok
        int chromosomeLength;                      // dlugosc chromosomu (liczba przedmiotow)
        int numberOfKnapsacks;                      // liczba plecakow
        int populationSize;                      // liczebnosc populacji
        int gameModel;              // model z teorii gier
        int[][] weights;            // wagi przedmiotow
        int[] constraints;          // ograniczenia wagowe plecakow

                                    // w paperze oznaczone:
        double weightGA;            // beta_GA
        double weightGT;            // beta_GT
        double cheatingDegree;      // tau
        double cheaterRate;         // alfa
        double crossoverRate;       // p_c
        double mutationRate;        // p_m

        Population population;

        public static Random random = new Random();

        private GA_GT() { }

        public GA_GT(int epochs, int[][] weights, int[] constraints, int N, int gm, double wga, double wgt, double chd, double chr, double cr, double mr)
        {
            this.numberOfEpochs = epochs;

            this.weights = weights;
            this.numberOfKnapsacks = weights.Length;
            this.chromosomeLength = weights[0].Length;

            for (int i = 0; i < numberOfKnapsacks; i++)
            {
                if (weights[i].Length != chromosomeLength)
                    throw new Exception("Argument exception: weights");
            }

            this.constraints = constraints;

            if (constraints.Length != numberOfKnapsacks)
                throw new Exception("Argument exception: constraints");

            this.populationSize = N;
            this.gameModel = gm;
            this.weightGA  = wga;
            this.weightGT  = wgt;
            this.cheatingDegree = chd;
            this.cheaterRate    = chr;
            this.crossoverRate  = cr;
            this.mutationRate   = mr;
        }

        public Individual RunGA_GT()
        {
            population = new Population();

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
