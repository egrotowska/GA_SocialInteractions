using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions {
    class Individual {

        Chromosome chromosome_;
        bool strategy_;
        double fitness_;
        bool isFeasible_;

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

        public bool isFeasible
        {
            get { return isFeasible_; }
            set { this.isFeasible_ = value; }
        }

        public Individual(Chromosome chromosome)
        {
            this.chromosome_ = chromosome;
        }

        public Individual(Chromosome chromosome, bool strategie, double fitness, bool isFeasible)
        {
            this.chromosome_ = chromosome;
            this.strategy_ = strategie;
            this.fitness_ = fitness;
            this.isFeasible = isFeasible;
        }

        private double fitnessCooperativeCooperative(Knapsack knapsack) 
        {
            if (this.isFeasible)
            {
                double sum = 0.0;
                for (int i = 0; i < this.chromosome.Count; i++)
                {
                    sum += this.chromosome[i] ? knapsack.GetValue(i) : 0.0;
                }

                return GA_GT.weightGA * sum / GA_GT.maxFitness 
                     + GA_GT.weightGT * GA_GT.gameModel.cooperatorCooperatorPayoff / GA_GT.maxPayoff;
            }
            else
            {
                double sum = 0.0;
                for (int i = 0; i < this.chromosome.Count; i++)
                {
                    sum += this.chromosome[i] ? knapsack.GetWeight(i) : 0.0;
                }

                return GA_GT.weightGA * (knapsack.constraint - sum) / GA_GT.maxFitness
                     + GA_GT.weightGT * GA_GT.gameModel.cooperatorCooperatorPayoff / GA_GT.maxPayoff;
            }
        }

        private double fitnessCooperativeDefector(Knapsack knapsack)
        {
            double sum = 0.0;
            if (this.isFeasible)
            {
                for (int i = 0; i < this.chromosome.Count; i++)
                {
                    sum += this.chromosome[i] ? knapsack.GetValue(i) : 0.0;
                }
                return GA_GT.weightGA * sum / GA_GT.maxFitness 
                    + GA_GT.weightGT * GA_GT.gameModel.cooperatorDefectorPayoff / GA_GT.maxPayoff;
            }
            else
            {
                for (int i = 0; i < this.chromosome.Count; i++)
                {
                    sum += this.chromosome[i] ? knapsack.GetWeight(i) : 0.0;
                }

                return GA_GT.weightGA * (knapsack.constraint - sum) / GA_GT.maxFitness
                     + GA_GT.weightGT * GA_GT.gameModel.cooperatorDefectorPayoff / GA_GT.maxPayoff;
            }
        }

        private double fitnessDefectorCooperative(Knapsack knapsack)
        {
            double deltaV = GA_GT.cheatingDegree / 100.0;
            double deltaW = GA_GT.cheatingDegree / 100.0;
            double sum = 0.0;

            if (this.isFeasible)
            {
                for (int i = 0; i < this.chromosome.Count; i++)
                {
                    sum += this.chromosome[i] ? knapsack.GetValue(i) + deltaV : 0.0;
                }
                return GA_GT.weightGA * sum / GA_GT.maxFitness 
                    + GA_GT.weightGT * GA_GT.gameModel.defectorCooperatorPayoff / GA_GT.maxPayoff;
            }
            else
            {
                for (int i = 0; i < this.chromosome.Count; i++)
                {
                    sum += this.chromosome[i] ? knapsack.GetWeight(i) - deltaW : 0.0;
                }

                return GA_GT.weightGA * (knapsack.constraint - sum) / GA_GT.maxFitness
                     + GA_GT.weightGT * GA_GT.gameModel.defectorCooperatorPayoff / GA_GT.maxPayoff;
            }
        }

        private double fitnessDefectorDefector(Knapsack knapsack)
        {
            double deltaV = GA_GT.cheatingDegree / 100.0;
            double deltaW = GA_GT.cheatingDegree / 100.0;
            double sum = 0.0;

            if (this.isFeasible)
            {
                for (int i = 0; i < this.chromosome.Count; i++)
                {
                    sum += this.chromosome[i] ? knapsack.GetValue(i) + deltaV : 0.0;
                }
                return GA_GT.weightGA * sum / GA_GT.maxFitness 
                    + GA_GT.weightGT * GA_GT.gameModel.defectorDefectorPayoff / GA_GT.maxPayoff;
            }
            else
            {
                for (int i = 0; i < this.chromosome.Count; i++)
                {
                    sum += this.chromosome[i] ? knapsack.GetWeight(i) - deltaW : 0.0;
                }

                return GA_GT.weightGA * (knapsack.constraint - sum) / GA_GT.maxFitness
                     + GA_GT.weightGT * GA_GT.gameModel.defectorDefectorPayoff / GA_GT.maxPayoff;
            }
        }

        public double FitnessValue(Individual secondInd, Knapsack knapsack)
        {
            bool strategy1 = this.strategy;
            bool strategy2 = secondInd.strategy;

            if (strategy1)
            {
                if (strategy2)
                {
                    return fitnessCooperativeCooperative(knapsack);
                }
                else
                {
                    return fitnessCooperativeDefector(knapsack);
                }
            }
            else
            {
                if (strategy2)
                {
                    return fitnessDefectorCooperative(knapsack);
                }
                else
                {
                    return fitnessDefectorDefector(knapsack);
                }
            }
        }

        public double FitnessValue(Individual secondInd, KnapsackList knapsackList)
        {
            double sum = 0.0;

            foreach (Knapsack knapsack in knapsackList.knapsackList)
            {
                sum += this.FitnessValue(secondInd, knapsack);
            }

            return sum;
        }

        public double FitnessValue(Knapsack knapsack)
        {
            double sum = 0.0;
            for (int i = 0; i < this.chromosome.Count; i++)
            {
                sum += this.chromosome[i] ? knapsack.GetValue(i) : 0.0;
            }

            if (this.isFeasible)
            {
                return sum / GA_GT.maxFitness;
            }
            else
            {
                return (knapsack.constraint - sum) / GA_GT.maxFitness;
            }
        }

        public double FitnessValue(KnapsackList knapsackList)
        {
            double sum = 0.0;

            foreach (Knapsack knapsack in knapsackList.knapsackList)
            {
                sum += this.FitnessValue(knapsack);
            }

            return sum;
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
            Console.WriteLine(strategy_ + " " + fitness_ + " " + isFeasible);
        }

        public void ShowFitness()
        {
            Console.WriteLine(fitness_);
        }

        public void Update(KnapsackList knapsackList)
        {
            isFeasible = chromosome.IsFeasible();
            fitness = FitnessValue(knapsackList);
        }

    }
}
