using System;
using System.Collections.Generic;
using System.Text;

namespace LazyGeneral
{
    class Army
    {
        public Army(int new_team, double new_power, int loc_x, int loc_y)
        {
            power = new_power;
            location[0] = loc_x;
            location[1] = loc_y;
            team = new_team;

        }
        private double power;
        public int[] location = new int[2];
        private int team;
        private double basic_dmg = 50;
        public int number;
        public bool fighting;

        public int Team
        {
            get { return team; }
        }

        public double Power
        {
            get { return power; }
            set { power = value; }
        }

        public double Basic_dmg
        {
            get { return basic_dmg; }
        }
    }
}
