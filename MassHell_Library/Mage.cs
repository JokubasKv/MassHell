using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassHell_Library.Interfaces;
using MassHell_Library.PlayerItems;

namespace MassHell_Library
{
    public class Mage : Item, IEnemyFactory
    {
        public int Damage { get; set; }
        public int Health { get; set; }
        public float Speed { get; set; }

        public Mage(int damage, int health, float speed)
        {
            Damage = damage;
            Health = health;
            Speed = speed;
        }

        public Mage()
        {

        }

        public IWeapon GetWeapon()
        {
            return new Wand();
        }

        public IArmor GetArmor()
        {
            return new Cloak();
        }
    }
}
