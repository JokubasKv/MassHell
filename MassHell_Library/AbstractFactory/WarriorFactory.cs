using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.AbstractFactory
{
    /// <summary>
    /// Warrior factory class for creating different difficulty warriors
    /// </summary>
    public class WarriorFactory : EnemyFacotry
    {
        /// <summary>
        /// Logger to print to console
        /// </summary>
        private readonly Logger _logger = Logger.getInstance();

        /// <summary>
        /// Override method to create easy warrior
        /// </summary>
        /// <returns>Easy warrior</returns>
        public override EasyEnemy CreateEasy()
        {
            var result = new EasyWarrior(100, 5, 1f);
            _logger.debug("Easy warrior created! Damage-" + result.Damage + " Health-" + result.Health + " Speed-" + result.Speed);
            return new EasyWarrior(100, 5, 1f);
        }

        /// <summary>
        /// Override method to create medium warrior
        /// </summary>
        /// <returns>Medium warrior</returns>
        public override MediumEnemy CreateMedium()
        {
            var result = new MediumWarrior(120, 8, 1.2f);
            _logger.debug("Medium warrior created! Damage-" + result.Damage + " Health-" + result.Health + " Speed-" + result.Speed);
            return result;
        }

        /// <summary>
        /// Override method to create hard warrior
        /// </summary>
        /// <returns>Hard warrior</returns>
        public override HardEnemy CreateHard()
        {
            var result = new HardWarrior(150, 10, 1.5f);
            _logger.debug("Hard warrior created! Damage-" + result.Damage + " Health-" + result.Health + " Speed-" + result.Speed);
            return result;
        }
    }
}
