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
            for (int i = 1; i < knapsackList_.Count; i++)
            {
                if (knapsackList_[i].values.Count() != numberOfValues && knapsackList_[i].weights.Count() != numberOfWeights) return false;
            }
            return true;
        }
    }
}
