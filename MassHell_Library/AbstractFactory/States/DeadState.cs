using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.AbstractFactory.States
{
    internal class DeadState : EnemyHealthState
    {
        public override void TakeDamage(int damage)
        {
            //Already dead dont do anything
            if (_enemy.Health <= 0)
            {
                this._enemy.TransitionTo(new DeadState());
            }
        }

        public override void Heal(int heal)
        {
            _enemy.Health += heal;
            if (_enemy.Health > 0)
            {
                this._enemy.TransitionTo(new LivingState());
            }
        }

        public override FormObject Move(string direction, double distance)
        {
            //Dead so dont move
            return new FormObject(_enemy.Name, _enemy.XCoordinate, _enemy.YCoordinate, _enemy.Rotation);
        }

    }
}
