using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.AbstractFactory
{
    /// <summary>
    /// Easy mage class
    /// </summary>
    public class EasyMage : EasyEnemy
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public float Speed { get; set; }

        public EasyMage(int health, int damage, float speed)
        {
            Health = health;
            Damage = damage;
            Speed = speed;
        }
    }
}
