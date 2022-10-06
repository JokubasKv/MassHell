using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public class SpeedPowerUp : PowerUp
    {
        public float SpeedBoost{ get; set; }
        public SpeedPowerUp()
        {

        }
        public SpeedPowerUp(float speedBoost)
        {
            SpeedBoost = speedBoost;
        }
    }
}
