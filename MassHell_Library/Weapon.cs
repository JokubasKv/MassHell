using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Legendary
    }
    public class Weapon : Item
    {
        public Weapon(string name,int damage,string rarity)
        {
            Name = name;
            Damage = damage;
            Rarity = (Rarity) Enum.Parse(typeof(Rarity), rarity);
        }
        public Weapon(string name)
        {
            Name = name;
        }
        public int Damage { get; set; }
        public Rarity Rarity;

    }
}
