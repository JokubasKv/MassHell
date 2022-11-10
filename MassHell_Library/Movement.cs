using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public abstract class Command
    {
        public abstract void Move();
        public abstract void Unmove();
    }

    public class MovementCommand : Command
    {
        string direction;
        double distance;
        Tile entity;
        Movement movement;
        // Constructor
        public MovementCommand(Movement calculator,
            string direction, double distance, Tile entity)
        {
            this.movement = calculator;
            this.direction = direction;
            this.distance = distance;
            this.entity = entity;
        }
        // Execute new command
        public override void Move()
        {
            movement.Operation(direction, distance, entity);
        }
        public override void Unmove()
        {
            movement.Operation(Undo(direction), distance, entity);
        }

        private string Undo(string direction)
        {
            switch (direction)
            {
                case "left": return "right";
                case "right": return "left";
                case "up": return "down";
                case "down": return "up";
                default:
                    throw new
             ArgumentException("@operator");
            }
        }
    }

    public class Movement
    {
        public void Operation(string direction, double distance, Tile player)
        {
            switch (direction)
            {
                case "left":
                    player.XCoordinate -= distance;
                    player.Rotation = 90;
                    break;
                case "right":
                    player.XCoordinate += distance;
                    player.Rotation = -90;
                    break;
                case "up":
                    player.YCoordinate -= distance;
                    player.Rotation = 180;
                    break;
                case "down":
                    player.YCoordinate += distance;
                    player.Rotation = 0;
                    break;
            }
        }
    }
}
