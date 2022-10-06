using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public class DamagePowerUp : PowerUp
    {
        public int DamageIncrease  { get; set; }
        public DamagePowerUp()
        {

        }
        public DamagePowerUp(int damageIncrease)
        {
            DamageIncrease = damageIncrease;
        }
    }
}
