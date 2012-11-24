using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions
{
    interface Game
    {

        double cooperatorCooperatorPayoff
        {
            get;
        }
        double cooperatorDefectorPayoff
        {
            get;
        }
        double defectorCooperatorPayoff
        {
            get;
        }
        double defectorDefectorPayoff
        {
            get;
        }

        void generatePayoffs();
    }
}
