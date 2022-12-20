using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public class Minigun : PowerUp, IPowerUp
    {

        public float SpeedBoost { get; set; }
        public Minigun()
        {

        }
        public Minigun(float speedBoost, string name, int effectTime) : base(name, effectTime)
        {
            EffectTime = effectTime;
            Name = name;
            SpeedBoost = speedBoost;
        }
        public int PowerupValue()
        {
            return 9001;
        }
    }
}
