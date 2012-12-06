using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions
{
    class Knapsack
    {
        int numberOfObjects;
        public int NumberOfObjects
        {
            get { return numberOfObjects; }
        }

        int[] weights_;
        public int[] weights
        {
            get { return weights_; }
            set { weights_ = value; }
        }

        int constraint_;
        public int constraint
        {
            get { return constraint_; }
            set { constraint_ = value; }
        }
        
        int[] values_;
        public int[] values
        {
            get { return values_; }
            set { values_ = value; }
        }

        public Knapsack(int[] weights, int[] values, int constraint)
        {
            this.weights_ = weights;
            this.numberOfObjects = weights.Length;
            this.constraint_ = constraint;

            this.values_ = values;

            if (values.Length != numberOfObjects)
                throw new ArgumentException("Knapsack constructor: values");
        }

        public int GetWeight(int i)
        {
            if (i + 1 > numberOfObjects)
                throw new ArgumentOutOfRangeException("Knapsack.GetWeight");

            return weights_[i];
        }

        public int GetValue(int i)
        {
            if(i + 1 > numberOfObjects)
                throw new ArgumentOutOfRangeException("Knapsack.GetValue");

            return values_[i];
        }

        public void Show() {
            Console.WriteLine("Constraint " + constraint_);
            for (int i = 0; i < numberOfObjects; i++) {
                Console.WriteLine("Object {0} : val {1} wgh {2}", i, values_[i], weights_[i]);
            }
        }
    }
}
