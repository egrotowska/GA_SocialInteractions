using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions
{

    //http://ima.ac.uk/papers/beltra2009.pdf
    class GA_GT
    {
        int numberOfEpochs;         // liczba epok
        int d;                      // dlugosc chromosomu (liczba przedmiotow)
        int m;                      // liczba plecakow
        int N;                      // liczebnosc populacji
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

        List<Tuple<bool[], bool, double>> population;       // kazdy osobnik = <chromosom, strategia, wartosc funkcji celu>
        Random r;

        private GA_GT() { }

        public GA_GT(int epochs, int[][] weights, int[] constraints, int N, int gm, double wga, double wgt, double chd, double chr, double cr, double mr)
        {
            this.numberOfEpochs = epochs;

            this.weights = weights;
            this.m = weights.Length;
            this.d = weights[0].Length;

            for (int i = 1; i < m; i++)
            {
                if (weights[i].Length != d)
                    throw new Exception("Argument exception: weights");
            }

            this.constraints = constraints;

            if (constraints.Length != m)
                throw new Exception("Argument exception: constraints");

            this.N = N;
            this.gameModel = gm;
            this.weightGA  = wga;
            this.weightGT  = wgt;
            this.cheatingDegree = chd;
            this.cheaterRate    = chr;
            this.crossoverRate  = cr;
            this.mutationRate   = mr;
            this.r = new Random();
        }

        public Tuple<bool[], bool, double> RunGA_GT()
        {

            RandomPopulation();
            int epoch = 0;

            while (epoch++ <= numberOfEpochs)
            {

                Evaluation();

                List<Tuple<bool[], bool, double>> parents = TournamentSelection();

                List<Tuple<bool[], bool, double>> offspring = TwoPointsCrossover(parents);

                // wybrac nowa populacje mozna tak jak nizej, ale mozna tez jakos inaczej:

                // posortuj rodzicow i potomstwo
                parents.Sort(Compare);
                offspring.Sort(Compare);

                // wyczysc populacje i wrzuc do niej N/2 najlepszych rodzicow oraz N/2 najlepszych dzieci
                population.Clear();

                for (int i = 0; i < N / 2; i++)
                {
                    population.Add(parents[i]);
                    population.Add(offspring[i]);
                }

                Mutation();

            }

            population.Sort(Compare);

            return population[0];
        }

        int Compare(Tuple<bool[], bool, double> x, Tuple<bool[], bool, double> y)
        {
            if (x.Item3 > y.Item3)
                return 1;
            else if (x.Item3 == y.Item3)
                return 0;
            else
                return -1;
        }

        void RandomPopulation()
        {
            int i;
            int numberOfCheaters = (int)(N * cheaterRate);

            for (i = 0; i < N; i++)
            {
                bool[] b = new bool[d];

                do
                {
                    for (int j = 0; j < d; j++)
                    {
                        b[j] = Convert.ToBoolean(r.Next() % 2);
                    }

                } while (!IsFeasible(b));       // na poczatku wszystkie osobniki sa prawidlowe

                Tuple<bool[], bool, double> t;

                if(i < numberOfCheaters)
                    t = new Tuple<bool[], bool, double>(b, false, 0);   // tworzymy cheatera

                else
                    t = new Tuple<bool[], bool, double>(b, true, 0);    // tworzymy cooperatora

                population.Add(t);
            }

        }

        // strategy1 - strategia wlasna osobnika, strategy2 - strategia drugiego z osobnikow
        // true = cooperator, false = cheater
        double FitnessValue(bool[] chromosome, bool strategy1, bool strategy2)
        {
            // TODO: wszystko

            return 0;
        }

        bool IsFeasible(bool[] chromosome)
        {
            // TODO: wszystko

            return true;
        }

        void Evaluation()
        {
            List<int> used = new List<int>();

            for (int i = 0; i < N; i++)
            {
                if (used.Contains(i))
                    continue;

                used.Add(i);
             
                int z = 0;

                while (true)
                {
                    z = r.Next() % N;

                    if (!used.Contains(z))
                    {
                        used.Add(z);
                        break;
                    }
                }

                double value = FitnessValue(population[i].Item1, population[i].Item2, population[z].Item2);
                Tuple<bool[], bool, double> t = new Tuple<bool[], bool, double>(population[i].Item1, population[i].Item2, value);
                population[i] = t;
            }
        }

        List<Tuple<bool[], bool, double>> TournamentSelection()
        {
            List<Tuple<bool[], bool, double>> parents = new List<Tuple<bool[], bool, double>>();
            List<int> used = new List<int>();
 
            while (parents.Count != N)
            {
                int x = r.Next() % N;
                int y = 0;

                while((y = r.Next() % N) == x)
                {
                }

                if (population[x].Item3 > population[y].Item3)
                {
                    parents.Add(population[x]);
                }
                else
                {
                    parents.Add(population[y]);
                }
            }

            return parents;
        }

        List<Tuple<bool[], bool, double>> TwoPointsCrossover(List<Tuple<bool[], bool, double>> parents)
        {
            List<Tuple<bool[], bool, double>> offspring = new List<Tuple<bool[], bool, double>>();
            List<int> used = new List<int>();

            for (int pair = 0; pair < parents.Count / 2; pair++)
            {
                int x, y;

                //wybierz rodzica x:
                while (true)
                {
                    x = r.Next() % parents.Count;

                    if (!used.Contains(x))
                    {
                        used.Add(x);
                        break;
                    }
                }

                //wybierz rodzica y:
                while (true)
                {
                    y = r.Next() % parents.Count;

                    if (!used.Contains(y))
                    {
                        used.Add(y);
                        break;
                    }
                }

                //TODO: wlasciwy crossover
            }

            return offspring;
        }

        void Mutation()
        {
            // TODO: wszystko
        }
    }
}
