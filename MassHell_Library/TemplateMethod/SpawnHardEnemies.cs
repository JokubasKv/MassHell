using MassHell_Library.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.TemplateMethod
{
    public class SpawnHardEnemies : EnemyConveoyr
    {
        private Random pos = new Random();
        public sealed override bool needMages()
        {
            return true;
        }

        public sealed override bool needNinjas()
        {
            return false;
        }

        public sealed override bool needWarriors()
        {
            return false;
        }

        public sealed override void spawnMages(out Tile position, out Item returningItem)
        {
            int heightRandom = pos.Next(1, 720);
            int widthRandom = pos.Next(1, 1280);
            position = new Tile(widthRandom, heightRandom, 0);

            returningItem = new Item();

            EnemyFacotry factory = new MageFactory();
            returningItem = factory.CreateHard();
            returningItem.Name = "Mage";
        }

        public sealed override void spawnNinjas(out Tile position, out Item returningItem)
        {
            int heightRandom = pos.Next(1, 720);
            int widthRandom = pos.Next(1, 1280);
            position = new Tile(widthRandom, heightRandom, 0);

            returningItem = new Item();

            EnemyFacotry factory = new NinjaFactory();
            returningItem = factory.CreateHard();
            returningItem.Name = "Ninja";
        }

        public sealed override void spawnWarriors(out Tile position, out Item returningItem)
        {
            int heightRandom = pos.Next(1, 720);
            int widthRandom = pos.Next(1, 1280);
            position = new Tile(widthRandom, heightRandom, 0);

            returningItem = new Item();

            EnemyFacotry factory = new WarriorFactory();
            returningItem = factory.CreateHard();
            returningItem.Name = "Warrior";
        }
    }
}
