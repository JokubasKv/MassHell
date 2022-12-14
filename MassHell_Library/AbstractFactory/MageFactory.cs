using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.AbstractFactory
{
    /// <summary>
    /// Mage factory class for creating different difficulty mages
    /// </summary>
    public class MageFactory : EnemyFacotry
    {
        /// <summary>
        /// Logger to print to console
        /// </summary>
        private readonly Logger _logger = Logger.getInstance();

        /// <summary>
        /// Override method to create easy mage
        /// </summary>
        /// <returns>Easy mage</returns>
        public override EasyEnemy CreateEasy()
        {
            var result = new EasyMage(100, 10, 1f);
            _logger.debug("Easy mage created! Damage-" + result.Damage + " Health-" + result.Health + " Speed-" + result.Speed);
            return result;
        }

        /// <summary>
        /// Override method to create medium mage
        /// </summary>
        /// <returns>Medium mage</returns>
        public override MediumEnemy CreateMedium()
        {
            var result = new MediumMage(150, 15, 1.5f);
            _logger.debug("Medium mage created! Damage-" + result.Damage + " Health-" + result.Health + " Speed-" + result.Speed);
            return result;
        }

        /// <summary>
        /// Override method to create hard mage
        /// </summary>
        /// <returns>Hard mage</returns>
        public override HardEnemy CreateHard()
        {
            var result = new HardMage(200, 20, 2f);
            _logger.debug("Hard mage created! Damage-" + result.Damage + " Health-" + result.Health + " Speed-" + result.Speed);
            return result;
        }
    }
}
