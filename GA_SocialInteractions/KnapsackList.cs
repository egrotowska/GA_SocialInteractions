using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions
{
    class KnapsackList
    {
        List<Knapsack> knapsackList_;
        public List<Knapsack> knapsackList {
            get { return knapsackList_; }
        }

        public KnapsackList(List<Knapsack> knapsacks)
        {
            knapsackList_ = knapsacks;
            if (!checkIfValidKnapsacks())
            {
                throw new ArgumentException("KnapsackList constructor: number of weights or values is invalid");
            }
        }

        public bool checkIfValidKnapsacks()
        {
            if (knapsackList_.DefaultIfEmpty() == null) return false;

            int numberOfValues = knapsackList_[0].values.Count();
            int numberOfWeights = knapsackList_[0].weights.Count();
            foreach (Knapsack knapsack in knapsackList_)
            {
                if (knapsack.values.Count() != numberOfValues && knapsack.weights.Count() != numberOfWeights) return false;
            }
            return true;
        }

        public void Show()
        {
            for (int i = 0; i < knapsackList_.Count; i++)
            {
                knapsackList_[i].Show();
            }
        }
    }
}
