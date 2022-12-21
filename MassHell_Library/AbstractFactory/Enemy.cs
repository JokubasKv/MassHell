using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassHell_Library.AbstractFactory.States;

namespace MassHell_Library.AbstractFactory
{
    internal class Enemy : Tile
    {
        public String Name { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public float Speed { get; set; }

        private EnemyHealthState _state = null;

        public Enemy(EnemyHealthState state)
        {
            this.TransitionTo(state);
        }

        // The Context allows changing the State object at runtime.
        public void TransitionTo(EnemyHealthState state)
        {
            Console.WriteLine($"Context: Transition to {state.GetType().Name}.");
            this._state = state;
            this._state.SetContext(this);
        }

        // The Context delegates part of its behavior to the current State
        // object.
        public void TakeDamage(int damage)
        {
            this._state.TakeDamage(damage);
        }

        public void Heal(int heal)
        {
            this._state.Heal(heal);
        }
        public FormObject Move(string direction, double distance)
        {
            return this._state.Move(direction, distance);
        }

    }
}
