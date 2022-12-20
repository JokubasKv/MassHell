using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    /// <summary>
    /// Powerup factory
    /// </summary>
    public class PowerUpFactory
    {
        /// <summary>
        /// Gets powerup by given name
        /// </summary>
        /// <param name="type">power up name</param>
        /// <returns>requested power up</returns>
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
                objType = new WaterWalk(30);
            }
            else if (type.ToLower().Equals("speedpowerup"))
            {
                objType = new SpeedPowerUp(1.5f, "SPEED", 20);
            }
            else if (type.ToLower().Equals("minigun"))
            {
                objType = new Minigun(3f, "MINIGUN", 50);
            }
            else
            {
                throw new ArgumentNullException($"Incorrect power up type {type}");
            }
            Console.WriteLine(objType.GetType().Name + " power up created");
            return objType;
        }
    }
}
