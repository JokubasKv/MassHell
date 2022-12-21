using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MassHell_Library;
using MassHell_Server;

namespace MassHell_Library.Interpreter
{
    public class SpawnMinigunExpression : IExpression
    {
        public SpawningSubSystem spawning = new SpawningSubSystem();
        private readonly Logger _logger = Logger.getInstance();

        public Dictionary<Tile, Item> Interpret(Context context, Tile pos, Item returningItem)
        {
            Dictionary<Tile, Item> result = new Dictionary<Tile, Item>();
            if (context._commandText == "/minigun")
            {
                spawning.SpawnMinigun(out pos, out returningItem);
                _logger.debug("Minigun spawned by command");
                returningItem.Name = "MINIGUN";
                result.Add(pos, returningItem);
            }
            return result;
        }
    }
}
