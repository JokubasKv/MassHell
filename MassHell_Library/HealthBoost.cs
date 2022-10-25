using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassHell_Library.Interfaces;

namespace MassHell_Library
{
    public class HealthBoost :PowerUp, IPowerUp
    {
        public int HealthIncrease  { get; set; }
        public float HealthRestore { get; set; }
        public HealthBoost()
        {
            this.HealthIncrease = 5;
            this.HealthRestore = 5;
        }
        public HealthBoost(int healthIncrease, float healthRestore)
        {
            HealthIncrease = healthIncrease;
            HealthRestore = healthRestore;
        }

        public int PowerupValue()
        {
            return HealthIncrease;
        }
    }
}
