using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.AbstractFactory
{
    /// <summary>
    /// Enemy factory abstract class/interface
    /// </summary>
    public abstract class EnemyFacotry
    {
        /// <summary>
        /// Method that creates easy difficulty enemy
        /// </summary>
        /// <returns>Easy enemy</returns>
        public abstract EasyEnemy CreateEasy();

        /// <summary>
        /// Method that creates medium difficulty enemy
        /// </summary>
        /// <returns>Medium enemy</returns>
        public abstract MediumEnemy CreateMedium();

        /// <summary>
        /// Method that creates hard difficulty enemy
        /// </summary>
        /// <returns>Hard enemy</returns>
        public abstract HardEnemy CreateHard();
    }
}
