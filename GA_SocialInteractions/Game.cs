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
        const double max = 2.0;

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

    public class ChickenGame : Game 
    {
        //values taken from wiki - there are incorrect values in the paper
        const double k = 0.0;
        const double s1 = 1.0;
        const double s2 = 1.0;
        const double c = 10.0;
        const double max = 1.0;

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

    public class MixedPolymorphism : Game
    {
        const double k = 1.0;
        const double s1 = 0.5;
        const double s2 = 0.5;
        const double c = 1.0;
        const double max = 1.5;

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

    public class FriendOrFoe : Game
    {
        const double k = 1.0;
        const double s1 = 1.0;
        const double s2 = 1.0;  //taken from wiki
        const double c = 1.0;
        const double max = 2.0;

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

    public class FacultativeDefection : Game
    {
        const double k = 1.0;
        const double s1 = 0.3;
        const double s2 = 0.3;
        const double c = 0.0;
        const double max = 1.3;

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

    public class BattleOfSexes : Game
    {
        const double k = 1.0;
        const double s1 = 1.0;
        const double s2 = -1.0;
        const double c = 0.3;
        const double max = 1.0;

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

    public class StagHunt : Game
    {
        //values taken from wiki - there are incorrect values in the paper
        const double k = 2.0;
        const double s1 = 2.0;
        const double s2 = -1.0;
        const double c = 1.0;
        const double max = 2.0; 

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
