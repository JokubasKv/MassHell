using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.TemplateMethod
{
    public abstract class EnemyConveoyr
    {
        public virtual void createEnemy()
        {
            if (needMages())
            {
                spawnMages(out Tile position, out Item returningItem);
            }
            if (needNinjas())
            {
                spawnNinjas(out Tile position, out Item returningItem);
            }
            if (needWarriors())
            {
                spawnWarriors(out Tile position, out Item returningItem);
            }
        }

        public abstract bool needMages();
        public abstract bool needWarriors();
        public abstract bool needNinjas();

        public abstract void spawnMages(out Tile position, out Item returningItem);
        public abstract void spawnWarriors(out Tile position, out Item returningItem);
        public abstract void spawnNinjas(out Tile position, out Item returningItem);
    }
}
