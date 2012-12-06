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

        public static Knapsack knapsack;
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
            maxFitness = 1.0;
            maxPayoff = gameModel.maxPayoff;
            maxFitness = 1.0;

            Console.WriteLine("Random population:");

        //    population.Show();
            maxFitness = population.Evaluation();
            for (int epoch = 0; epoch < numberOfEpochs; epoch++)
            {

                population.Sort();
                population.getIndividual(0).Show();
               // Console.ReadLine();

               // Console.WriteLine("Population after evaluation:");
                //population.Show();
               // Console.ReadLine();

                Population parents = population.TournamentSelection();
              //  Console.WriteLine("parents before");
               //parents.Show();
                Population offspring = population.TwoPointsCrossover(parents);
               // Console.WriteLine("parents after");
                //parents.Show();
               // Console.WriteLine("offspring");
               // offspring.Show();


                //Console.ReadLine();

                parents.Sort();
                offspring.Sort();

                population.Clear();

                // TODO: number of cheaters should be less than a cheater rate? or some other value?
                for (int i = 0; i < populationSize / 2; i++)
                {
                    population.Add(parents.getIndividual(i));
                    population.Add(offspring.getIndividual(i));
                }

              //  Console.WriteLine("before mutation");
               // population.Show();

                population.Mutation();
                maxFitness = population.Evaluation();
             //   Console.WriteLine("after mutation");
                //population.Show();

               // Console.ReadLine();
            }
          //  population.Show();

            population.Sort();
            return population.getIndividual(0);
        }
    }
}
