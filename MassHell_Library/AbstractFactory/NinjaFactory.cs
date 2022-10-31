using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.AbstractFactory
{
    /// <summary>
    /// Ninja factory class for creating different difficulty ninjas
    /// </summary>
    public class NinjaFactory : EnemyFacotry
    {
        /// <summary>
        /// Override method to create easy ninja
        /// </summary>
        /// <returns>Easy ninja</returns>
        public override EasyEnemy CreateEasy()
        {
            var result = new EasyNinja(50, 15, 2.0f);
            Console.WriteLine("Easy ninja created! Damage-" + result.Damage + " Health-" + result.Health + " Speed-" + result.Speed);
            return result;
        }

        /// <summary>
        /// Override method to create meium ninja
        /// </summary>
        /// <returns>Medium ninja</returns>
        public override MediumEnemy CreateMedium()
        {
            var result = new MediumNinja(60, 25, 2.5f);
            Console.WriteLine("Medium ninja created! Damage-" + result.Damage + " Health-" + result.Health + " Speed-" + result.Speed);
            return result;
        }

        /// <summary>
        /// Override method to create hard ninja
        /// </summary>
        /// <returns>Hard ninja</returns>
        public override HardEnemy CreateHard()
        {
            var result = new HardNinja(70, 40, 3.0f);
            Console.WriteLine("Hard ninja created! Damage-" + result.Damage + " Health-" + result.Health + " Speed-" + result.Speed);
            return result;
        }
    }
}
