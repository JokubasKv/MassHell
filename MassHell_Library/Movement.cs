using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public abstract class Command
    {
        public abstract void Execute();
    }
    /// <summary>
    /// The 'ConcreteCommand' class
    /// </summary>
    public class MovementCommand : Command
    {
        string direction;
        bool moving;
        Player player;
        Movement calculator;
        // Constructor
        public MovementCommand(Movement calculator,
            string direction, bool moving, Player player)
        {
            this.calculator = calculator;
            this.direction = direction;
            this.moving = moving;
            this.player = player;
        }
        // Execute new command
        public override void Execute()
        {
            calculator.Operation(direction, moving, player);
        }
    }
    /// <summary>
    /// The 'Receiver' class
    /// </summary>
    public class Movement
    {
        public void Operation(string @operator, bool moving, Player player)
        {
            switch (@operator)
            {
                case "left": player.goLeft = moving; break;
                case "right": player.goRight = moving; break;
                case "up": player.goUp = moving; break;
                case "down": player.goDown = moving; break;
            }
        }
    }
}
