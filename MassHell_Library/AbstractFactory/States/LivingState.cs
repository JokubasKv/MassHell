using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.AbstractFactory.States
{
    internal class LivingState : EnemyHealthState
    {

        public override void TakeDamage(int damage)
        {
            _enemy.Health -= damage;
            //If dead go to dead state
            if (_enemy.Health <= 0)
            {
                this._enemy.TransitionTo(new DeadState());
            }
        }

        public override void Heal(int heal)
        {
            _enemy.Health += heal;
        }

        public override FormObject Move(string direction, double distance)
        {

            switch (direction)
            {
                case "left":
                    _enemy.XCoordinate -= distance;
                    _enemy.Rotation = 90;
                    break;
                case "right":
                    _enemy.XCoordinate += distance;
                    _enemy.Rotation = -90;
                    break;
                case "up":
                    _enemy.YCoordinate -= distance;
                    _enemy.Rotation = 180;
                    break;
                case "down":
                    _enemy.YCoordinate += distance;
                    _enemy.Rotation = 0;
                    break;
            }
            return new FormObject(_enemy.Name, _enemy.XCoordinate, _enemy.YCoordinate, _enemy.Rotation);

        }
    }
}
