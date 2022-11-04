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

        public string Name { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public int Mana { get; set; }
        public int Health { get; set; }
        public byte Lives { get; set; }
        public bool CanWalkOnWater { get; set; }
        public Item[] Inventory { get; set; }

        public bool goLeft { get; set; }
        public bool goRight { get; set; }
        public bool goUp { get; set; }
        public bool goDown { get; set; }

        public bool invOpen = true;


        Movement movement = new Movement();

        public Player()
        {
            Inventory = new Item[0];
        }
        public Player(int id, string name, double positionX, double positionY, int speed, int damage, int health, byte lives, int mana = 0, bool canWalkOnWater = false)
        {
            Id = id;
            Name = name;
            XCoordinate = positionX;
            YCoordinate = positionY;
            Speed = speed;
            Damage = damage;
            Mana = mana;
            Health = health;
            Lives = lives;
            CanWalkOnWater = canWalkOnWater;
            Inventory = new Item[10];
        }

        public void MovementCommand(string direction, bool moving)
        {
            // Create command operation and execute it
            Command command = new MovementCommand(movement, direction, moving, this);
            command.Execute();
            // Add command to undo list
        }

        public bool isMoving()
        {
            return goLeft | goRight | goUp | goDown;
        }
    }

}
