using System;
using System.Collections.Generic;
using System.Text;

namespace LazyGeneral
{
    class Duel
    {
        public Duel(Army First, Army Second)
        {
            force1 = First;
            force2 = Second;
        }

        private int number;
        private Army force1;
        private Army force2;
        //private int location;

        public Army Force1
        {
            get { return force1; }
            set { force1 = value; }
        }

        public Army Force2
        {
            get { return force2; }
            set { force2 = value; }
        }

        public void Fight()
        {
            Army temp = new Army(force1.Team, force1.Power, 0, 0);
            temp.Power = force1.Power;
            force1.Power -= force2.Basic_dmg + force2.Power * 0.33;
            force2.Power -= temp.Basic_dmg + temp.Power * 0.33;
        }
    }
}
