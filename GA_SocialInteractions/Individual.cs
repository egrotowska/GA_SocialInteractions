using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions {
    class Individual {
        public Chromosome chromosome
        {
            get { return chromosome; }
            set { this.chromosome = value; }
        }
        public bool strategie
        {
            get { return strategie; }
            set { this.strategie = value; }
        }
        public double fitness
        {
            get { return fitness; }
            set { this.fitness = value; }
        }

        public Individual(Chromosome chromosome, bool strategie, double fitness)
        {
            this.chromosome = chromosome;
            this.strategie = strategie;
            this.fitness = fitness;
        }


        // strategy1 - strategia wlasna osobnika, strategy2 - strategia drugiego z osobnikow
        // true = cooperator, false = cheater
        public double FitnessValue(Chromosome chromosome, bool strategy1, bool strategy2) {
            // TODO: wszystko
            return 0;
        }
    }
}
