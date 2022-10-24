using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public class DamagePowerUp : PowerUp, IPowerUp
    {
        public int DamageIncrease  { get; set; }
        public DamagePowerUp()
        {
            this.DamageIncrease = 10;
        }
        public DamagePowerUp(int damageIncrease)
        {
            DamageIncrease = damageIncrease;
        }

        public int PowerupValue()
        {
            return DamageIncrease;
        }
    }
}
