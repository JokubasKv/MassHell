using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassHell_Library.Interfaces;

namespace MassHell_Library
{
    public class SpeedPowerUp :PowerUp, IPowerUp
    {
        public float SpeedBoost{ get; set; }
        public SpeedPowerUp() 
        {

        }
        public SpeedPowerUp(float speedBoost,string name, int effectTime) : base(name, effectTime)
        {
            EffectTime = effectTime;
            Name = name;
            SpeedBoost = speedBoost;
        }

        public int PowerupValue()
        {
            return (int)SpeedBoost;
        }
    }
}
