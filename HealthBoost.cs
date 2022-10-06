using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public class HealthBoost : PowerUp
    {
        public int HealthIncrease  { get; set; }
        public float HealthRestore { get; set; }
        public HealthBoost()
        {

        }
        public HealthBoost(int healthIncrease, float healthRestore)
        {
            HealthIncrease = healthIncrease;
            HealthRestore = healthRestore;
        }
    }
}
