using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public class Player : Tile
    {
        public int Id { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public int Mana { get; set; }
        public int Health { get; set; }
        public byte Lives { get; set; }
        public bool CanWalkOnWater { get; set; }
        public Item[] Inventory { get; set; }
        public Player()
        {
            Inventory = new Item[0];
        }
        public Player(int id, int speed, int damage,  int health, byte lives, int mana = 0, bool canWalkOnWater = false)
        {
            Id = id;
            Speed = speed;
            Damage = damage;
            Mana = mana;
            Health = health;
            Lives = lives;
            CanWalkOnWater = canWalkOnWater;
            Inventory = new Item[10];
        }
    }
}
