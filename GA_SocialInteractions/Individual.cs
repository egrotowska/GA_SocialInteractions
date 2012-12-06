using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions {
    class Individual {

        Chromosome chromosome_;
        bool strategy_;
        double fitness_;

        public Chromosome chromosome
        {
            get { return chromosome_; }
            set { this.chromosome_ = value; }
        }

        public bool strategy
        {
            get { return strategy_; }
            set { this.strategy_ = value; }
        }

        public double fitness
        {
            get { return fitness_; }
            set { this.fitness_ = value; }
        }

        public Individual(Chromosome chromosome)
        {
            this.chromosome_ = chromosome;
        }

        public Individual(Chromosome chromosome, bool strategie, double fitness)
        {
            this.chromosome_ = chromosome;
            this.strategy_ = strategie;
            this.fitness_ = fitness;
        }

        private double fitnessCooperativeCooperative(Chromosome chromosome) 
        {
              if (chromosome.IsFeasible())
                    {
                        double sum = 0.0;
                        for (int i = 0; i < chromosome.Count; i++)
                        {
                            sum += chromosome[i] ? GA_GT.knapsack.GetValue(i) : 0.0;
                        }

                        return GA_GT.weightGA * sum / GA_GT.maxFitness + GA_GT.weightGT * GA_GT.gameModel.cooperatorCooperatorPayoff / GA_GT.maxPayoff;
                    }
                    else
                    {
                        double sum = 0.0;
                        for (int i = 0; i < chromosome.Count; i++)
                        {
                            sum += chromosome[i] ? GA_GT.knapsack.GetWeight(i) : 0.0;
                        }

                        return GA_GT.weightGA * (GA_GT.knapsack.constraint - sum) / GA_GT.maxFitness 
                             + GA_GT.weightGT * GA_GT.gameModel.cooperatorCooperatorPayoff / GA_GT.maxPayoff;
                    }
        }

        private double fitnessCooperativeDefector(Chromosome chromosome)
        {
            double sum = 0.0;
            if (chromosome.IsFeasible())
            {
                for (int i = 0; i < chromosome.Count; i++)
                {
                    sum += chromosome[i] ? GA_GT.knapsack.GetValue(i) : 0.0;
                }
                return GA_GT.weightGA * sum / GA_GT.maxFitness + GA_GT.weightGT * GA_GT.gameModel.cooperatorDefectorPayoff / GA_GT.maxPayoff;
            }
            else
            {
                for (int i = 0; i < chromosome.Count; i++)
                {
                    sum += chromosome[i] ? GA_GT.knapsack.GetWeight(i) : 0.0;
                }

                return GA_GT.weightGA * (GA_GT.knapsack.constraint - sum) / GA_GT.maxFitness
                     + GA_GT.weightGT * GA_GT.gameModel.cooperatorDefectorPayoff / GA_GT.maxPayoff;
            }
        }

        private double fitnessDefectorCooperative(Chromosome chromosome)
        {
            double deltaV = GA_GT.cheatingDegree / 100.0;
            double deltaW = GA_GT.cheatingDegree / 100.0;
            double sum = 0.0;
            
            if (chromosome.IsFeasible())
            {
                for (int i = 0; i < chromosome.Count; i++)
                {
                    sum += chromosome[i] ? GA_GT.knapsack.GetValue(i) + deltaV : 0.0;
                }
                return GA_GT.weightGA * sum / GA_GT.maxFitness + GA_GT.weightGT * GA_GT.gameModel.defectorCooperatorPayoff / GA_GT.maxPayoff;
            }
            else
            {
                for (int i = 0; i < chromosome.Count; i++)
                {
                    sum += chromosome[i] ? GA_GT.knapsack.GetWeight(i) - deltaW : 0.0;
                }

                return GA_GT.weightGA * (GA_GT.knapsack.constraint - sum) / GA_GT.maxFitness
                     + GA_GT.weightGT * GA_GT.gameModel.defectorCooperatorPayoff / GA_GT.maxPayoff;
            }
        }

        private double fitnessDefectorDefector(Chromosome chromosome)
        {
            double deltaV = GA_GT.cheatingDegree / 100.0;
            double deltaW = GA_GT.cheatingDegree / 100.0;
            double sum = 0.0;

            if (chromosome.IsFeasible())
            {
                for (int i = 0; i < chromosome.Count; i++)
                {
                    sum += chromosome[i] ? GA_GT.knapsack.GetValue(i) + deltaV : 0.0;
                }
                return GA_GT.weightGA * sum / GA_GT.maxFitness + GA_GT.weightGT * GA_GT.gameModel.defectorDefectorPayoff / GA_GT.maxPayoff;
            }
            else
            {
                for (int i = 0; i < chromosome.Count; i++)
                {
                    sum += chromosome[i] ? GA_GT.knapsack.GetWeight(i) - deltaW : 0.0;
                }

                return GA_GT.weightGA * (GA_GT.knapsack.constraint - sum) / GA_GT.maxFitness
                     + GA_GT.weightGT * GA_GT.gameModel.defectorDefectorPayoff / GA_GT.maxPayoff;
            }
        }

        //public double FitnessValue(Chromosome chromosome, bool strategy1, bool strategy2) 
        //{
        //    if (strategy1)
        //    {
        //        if (strategy2)
        //        {
        //            return fitnessCooperativeCooperative(chromosome);
        //        } 
        //        else
        //        {
        //            return fitnessCooperativeDefector(chromosome);
        //        }
        //    } 
        //    else
        //    {
        //        if (strategy2)
        //        {
        //            return fitnessDefectorCooperative(chromosome);
        //        }
        //        else
        //        {
        //            return fitnessDefectorDefector(chromosome);
        //        }
        //    }
        //}

        public double FitnessValue(Chromosome chromosome)
        {
            if (chromosome.IsFeasible())
            {
                double sum = 0.0;
                for (int i = 0; i < chromosome.Count; i++)
                {
                    sum += chromosome[i] ? GA_GT.knapsack.GetValue(i) : 0.0;
                }

                return sum;
            }
            else
            {
                //double sum = 0.0;
                //for (int i = 0; i < chromosome.Count; i++)
                //{
                //    sum += chromosome[i] ? GA_GT.knapsack.GetWeight(i) : 0.0;
                //}

                //return GA_GT.knapsack.constraint - sum;
                return 0.0;
            }
        }

        public bool this[int i]
        {
            get { return chromosome[i]; }
        }

        public void MutateGene(int i)
        {
            chromosome.MutateGene(i);
        }

        public void Show()
        {
            for (int i = 0; i < chromosome.Count; i++)
            {
                if (chromosome[i])
                    Console.Write("1 ");
                else
                    Console.Write("0 ");
            }
            Console.WriteLine(strategy_ + " " + fitness_);
        }
    }
}
