using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyGeneral
{
    class Battle
    {
        //=======================Конструктор=======================//
        public Battle()
        {

        }

        //=======================Необходимые параметры=======================//
        private Army[] forces = new Army[2];

        //=======================Свойства=======================//
        public Army[] Forces
        {
            get { return forces; }
        }

        //=======================Методы=======================//
        public void Fight()
        {
            Army temp = new Army(forces[0].Power, 0, 0, 0, 99);
            temp.Power = forces[0].Power;
            forces[0].Power -= forces[1].BasicPower + forces[1].Power * 0.33;
            forces[1].Power -= temp.BasicPower + temp.Power * 0.33;
        }
        public void Init(Army a1, Army a2)
        {
            forces[0] = a1;
            forces[1] = a2;
        }
    }
}
