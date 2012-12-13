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
            this.isFeasible_ = isFeasible;
        }

        public Individual(Individual i)
        {
            this.chromosome_ = new Chromosome(i.chromosome);
            this.strategy_ = i.strategy;
            this.isFeasible_ = i.isFeasible;
            this.fitness_ = i.fitness;
        }

        private double fitnessCooperativeCooperative(KnapsackList knapsackList)
        {
            Knapsack knapsack = knapsackList[0];
            double sum = 0.0;

            for (int i = 0; i < this.chromosome.Count; i++)
            {
                sum += this.chromosome[i] ? knapsack.GetValue(i) : 0.0;
            }

            if (this.isFeasible)
            {
                return GA_GT.weightGA * sum / GA_GT.maxFitness
                     + GA_GT.weightGT * GA_GT.gameModel.cooperatorCooperatorPayoff / GA_GT.maxPayoff;
            }

            else
            {
                return GA_GT.weightGA * (sum - NonFeasibleKnapsacks(knapsackList) * knapsack.maxValue) / GA_GT.maxFitness
                     + GA_GT.weightGT * GA_GT.gameModel.cooperatorCooperatorPayoff / GA_GT.maxPayoff;
            }

        }

        private double fitnessCooperativeDefector(KnapsackList knapsackList)
        {
            Knapsack knapsack = knapsackList[0];
            double sum = 0.0;

            for (int i = 0; i < this.chromosome.Count; i++)
            {
                sum += this.chromosome[i] ? knapsack.GetValue(i) : 0.0;
            }

            if (this.isFeasible)
            {
                return GA_GT.weightGA * sum / GA_GT.maxFitness
                     + GA_GT.weightGT * GA_GT.gameModel.cooperatorDefectorPayoff / GA_GT.maxPayoff;
            }

            else
            {
                return GA_GT.weightGA * (sum - NonFeasibleKnapsacks(knapsackList) * knapsack.maxValue) / GA_GT.maxFitness
                     + GA_GT.weightGT * GA_GT.gameModel.cooperatorDefectorPayoff / GA_GT.maxPayoff;
            }
        }

        private double fitnessDefectorCooperative(KnapsackList knapsackList)
        {
            Knapsack knapsack = knapsackList[0];
            double deltaV = GA_GT.cheatingDegree / 100.0;
            //double deltaW = GA_GT.cheatingDegree / 100.0;
            double sum = 0.0;

            for (int i = 0; i < this.chromosome.Count; i++)
            {
                sum += this.chromosome[i] ? knapsack.GetValue(i) + deltaV : 0.0;
            }

            if (this.isFeasible)
            {
                return GA_GT.weightGA * sum / GA_GT.maxFitness
                     + GA_GT.weightGT * GA_GT.gameModel.defectorCooperatorPayoff / GA_GT.maxPayoff;
            }

            else
            {
                return GA_GT.weightGA * (sum - NonFeasibleKnapsacks(knapsackList) * knapsack.maxValue) / GA_GT.maxFitness
                     + GA_GT.weightGT * GA_GT.gameModel.defectorCooperatorPayoff / GA_GT.maxPayoff;
            }
        }

        private double fitnessDefectorDefector(KnapsackList knapsackList)
        {
            Knapsack knapsack = knapsackList[0];
            double deltaV = GA_GT.cheatingDegree / 100.0;
            //double deltaW = GA_GT.cheatingDegree / 100.0;
            double sum = 0.0;

            for (int i = 0; i < this.chromosome.Count; i++)
            {
                sum += this.chromosome[i] ? knapsack.GetValue(i) + deltaV : 0.0;
            }

            if (this.isFeasible)
            {
                return GA_GT.weightGA * sum / GA_GT.maxFitness
                     + GA_GT.weightGT * GA_GT.gameModel.defectorDefectorPayoff / GA_GT.maxPayoff;
            }

            else
            {
                return GA_GT.weightGA * (sum - NonFeasibleKnapsacks(knapsackList) * knapsack.maxValue) / GA_GT.maxFitness
                     + GA_GT.weightGT * GA_GT.gameModel.defectorDefectorPayoff / GA_GT.maxPayoff;
            }
        }

        public double FitnessValue(Individual secondInd, KnapsackList knapsackList)
        {
            bool strategy1 = this.strategy;
            bool strategy2 = secondInd.strategy;

            if (strategy1)
            {
                if (strategy2)
                {
                    return fitnessCooperativeCooperative(knapsackList);
                }
                else
                {
                    return fitnessCooperativeDefector(knapsackList);
                }
            }
            else
            {
                if (strategy2)
                {
                    return fitnessDefectorCooperative(knapsackList);
                }
                else
                {
                    return fitnessDefectorDefector(knapsackList);
                }
            }
        }


        public double FitnessValue(KnapsackList knapsackList)
        {
            double sum = 0.0;

            if (this.isFeasible)
            {
                for (int i = 0; i < this.chromosome.Count; i++)
                {
                    sum += this.chromosome[i] ? knapsackList[0].GetValue(i) : 0.0;
                }

                return sum / GA_GT.maxFitness;
            }

            else
            {
                double deltaV = GA_GT.cheatingDegree / 100.0;
                for (int i = 0; i < this.chromosome.Count; i++)
                {
                    sum += this.chromosome[i] ? knapsackList[0].GetValue(i) + deltaV : 0.0;
                }

                return (sum - NonFeasibleKnapsacks(knapsackList) * knapsackList[0].maxValue) / GA_GT.maxFitness;
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
            if (strategy_)
            {
                Console.WriteLine("cooperator " + fitness_ + " " + isFeasible);
            }
            else
            {
                Console.WriteLine("defector " + fitness_ + " " + isFeasible);
            }
        }

        public void ShowFitnessAndStrategy()
        {
            if (strategy_)
            {
                Console.WriteLine("cooperator " + fitness_);
            }
            else
            {
                Console.WriteLine("defector " + fitness_);
            }
        }

        public void ShowFitness()
        {
            Console.WriteLine(fitness_);
        }

        public void ShowValue()
        {
            double sum = 0.0;
            for (int i = 0; i < this.chromosome.Count; i++)
            {
                sum += this.chromosome[i] ? GA_GT.knapsackList[0].GetValue(i) : 0.0;
            }

            Console.WriteLine(sum);
        }

        public void Update(KnapsackList knapsackList)
        {
            isFeasible = chromosome.IsFeasible();
            fitness = FitnessValue(knapsackList);
        }

        public int NonFeasibleKnapsacks(KnapsackList knapsackList)
        {
            int counter = 0;

            foreach (Knapsack k in knapsackList.knapsackList)
            {
                if (!this.chromosome.IsFeasible(k))
                    counter++;
            }

            return counter;
        }
    }
}
