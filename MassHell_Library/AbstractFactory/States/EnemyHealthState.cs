using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.AbstractFactory.States
{
    abstract class EnemyHealthState
    {

        protected Enemy _enemy;

        public void SetContext(Enemy context)
        {
            _enemy = context;
        }

        public abstract void TakeDamage(int damage);

        public abstract void Heal(int heal);

        public abstract FormObject Move(string direction, double distance);

    }
}
