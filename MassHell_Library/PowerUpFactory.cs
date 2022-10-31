using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    /// <summary>
    /// 
    /// </summary>
    public class PowerUpFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IPowerUp getPowerUp(string type)
        {
            IPowerUp objType = null;
            if (type.ToLower().Equals("healthboost"))
            {
                objType = new HealthBoost(10,10);
            }
            else if (type.ToLower().Equals("damagepowerup"))
            {
                objType = new DamagePowerUp(10);
            }
            else if (type.ToLower().Equals("waterwalk"))
            {
                objType = new WaterWalk();
            }
            else if (type.ToLower().Equals("speedpowerup"))
            {
                objType = new SpeedPowerUp(1.5f, "SPEED", 20);
            }
            else
            {
                throw new ArgumentNullException($"Incorrect power up type {type}");
            }
            return objType;
        }
    }
}
