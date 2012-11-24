using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions
{
    interface Game
    {
        double cooperatorCooperatorPayoff;
        double cooperatorDefectorPayoff;
        double defectorCooperatorPayoff;
        double defectorDefectorPayoff;

        public double cooperatorCooperatorPayoff
        {
            get;
        }
        public double cooperatorDefectorPayoff
        {
            get;
        }
        public double defectorCooperatorPayoff
        {
            get;
        }
        public double defectorDefectorPayoff
        {
            get;
        }

        public void generatePayoffs();
    }
}
