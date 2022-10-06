using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public class Weapon : Item
    {
        public int Damage { get; set; }
        public enum Rarity
        {
            Common,
            Uncommon,
            Rare,
            Legendary
        }
    }
}
