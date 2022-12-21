using MassHell_Library.Interpreter;
using MassHell_Library;

namespace MassHell_Server.Interpreter
{
    public class SpawnBossExpression : IExpression
    {
        public SpawningSubSystem spawning = new SpawningSubSystem();
        private readonly Logger _logger = Logger.getInstance();
        public Dictionary<Tile, Item> Interpret(Context context, Tile pos, Item returningItem)
        {
            Dictionary<Tile, Item> result = new Dictionary<Tile, Item>();
            if (context._commandText == "/boss")
            {
                spawning.SpawnEnemy(out pos, out returningItem);
                _logger.debug("Boss spawned by command");
                returningItem.Name = "BOSS";
                result.Add(pos, returningItem);
            }
            return result;
        }
    }
}
