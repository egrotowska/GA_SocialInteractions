using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_SocialInteractions
{
    public interface Game
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
        double maxPayoff
        {
            get;
        }

        void generatePayoffs();
    }

    public class PrisonersDilemma : Game
    {
        const double k = 1.0;
        const double s1 = 0.4;
        const double s2 = 1.0;
        const double c = 0.2;
        const double max = 2.0; //?

        public double cooperatorCooperatorPayoff
        {
            get { return k; }
        }
        public double cooperatorDefectorPayoff
        {
            get { return k - s1; }
        }
        public double defectorCooperatorPayoff
        {
            get { return k + s2; }
        }
        public double defectorDefectorPayoff
        {
            get { return k - c; }
        }
        public double maxPayoff
        {
            get { return max; }
        }

        public void generatePayoffs()
        {
        }
    }
}
