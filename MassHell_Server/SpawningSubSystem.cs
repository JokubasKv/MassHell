using MassHell_Library.AbstractFactory;
using MassHell_Library;
using Microsoft.AspNetCore.SignalR;
using System;


namespace MassHell_Server
{
    public class SpawningSubSystem
    {
        private Random pos = new Random();
        private Logger _logger = Logger.getInstance();
        public void SpawnItem(out Tile position, out Item returningItem)
        {
            // Will be changed according to map Tiles
            int heightRandom = pos.Next(1, 720);
            int widthRandom = pos.Next(1, 1280);
            position = new Tile(widthRandom, heightRandom, 0);
            float chance = pos.NextSingle();
            returningItem = new Item();
            if (chance > 0.5)
            {
                // Change not to be hardcoded
                returningItem = new Weapon("InterestingName", 50, "Legendary");
                _logger.debug("Weapon Created");


            }
            else
            {
                //returningItem = (Item)PowerUpFactory.getPowerUp("speedpowerup");
                //returningItem = new SpeedPowerUp(1.5f,"SPEED",20);
                Random random = new Random();
                int probbability = random.Next(0, 300);
                if (probbability >= 100 && probbability <= 200)
                {
                    // Creating powerups
                    Random random2 = new Random();
                    int probbability2 = random2.Next(0, 300);
                    if (probbability2 >= 100 && probbability2 <= 200)
                    {
                        returningItem = (Item)PowerUpFactory.getPowerUp("healthboost");
                        returningItem.Name = "healthboost";
                    }
                    else if (probbability2 > 200)
                    {
                        returningItem = (Item)PowerUpFactory.getPowerUp("damagepowerup");
                        returningItem.Name = "damagepowerup";
                    }
                    else
                    {
                        returningItem = (Item)PowerUpFactory.getPowerUp("speedpowerup");
                        returningItem.Name = "speedpowerup";
                    }
                }
            }

        }

        /// <summary>
        /// Spawning enemies based on chance
        /// </summary>
        /// <returns>Enemy that needs to spawn</returns>
        public void SpawnEnemy(out Tile position, out Item returningItem)
        {
            int heightRandom = pos.Next(1, 720);
            int widthRandom = pos.Next(1, 1280);
            position = new Tile(widthRandom, heightRandom, 0);

            Random random = new Random();
            int chance = random.Next(0, 300);
            returningItem = new Item();

            if (chance >= 100 && chance <= 200)
            {
                // Creating mages
                Random random2 = new Random();
                int chance2 = random.Next(0, 300);
                if (chance2 >= 100 && chance2 <= 200)
                {
                    EnemyFacotry factory = new MageFactory();
                    returningItem = factory.CreateEasy();
                }
                else if (chance2 > 200)
                {
                    EnemyFacotry factory = new MageFactory();
                    returningItem = factory.CreateMedium();
                }
                else
                {
                    EnemyFacotry factory = new MageFactory();
                    returningItem = factory.CreateHard();
                }
                returningItem.Name = "Mage";
            }
            else if (chance > 200)
            {
                // Creating warriors
                Random random2 = new Random();
                int chance2 = random2.Next(0, 300);
                if (chance2 >= 100 && chance2 <= 200)
                {
                    EnemyFacotry factory = new WarriorFactory();
                    returningItem = factory.CreateEasy();
                }
                else if (chance2 > 200)
                {
                    EnemyFacotry factory = new WarriorFactory();
                    returningItem = factory.CreateMedium();
                }
                else
                {
                    EnemyFacotry factory = new WarriorFactory();
                    returningItem = factory.CreateHard();
                }
                returningItem.Name = "Warrior";
            }
            else
            {
                // Creating ninjas
                Random random2 = new Random();
                int chance2 = random2.Next(0, 300);
                if (chance2 >= 100 && chance2 <= 200)
                {
                    EnemyFacotry factory = new NinjaFactory();
                    returningItem = factory.CreateEasy();
                }
                else if (chance2 > 200)
                {
                    EnemyFacotry factory = new NinjaFactory();
                    returningItem = factory.CreateMedium();
                }
                else
                {
                    EnemyFacotry factory = new NinjaFactory();
                    returningItem = factory.CreateHard();
                }
                returningItem.Name = "Ninja";
            }
            var test = returningItem.Name;

        }

        public void SpawnMinigun(out Tile position, out Item returningItem)
        {
            // Will be changed according to map Tiles
            int heightRandom = pos.Next(1, 720);
            int widthRandom = pos.Next(1, 1280);
            position = new Tile(widthRandom, heightRandom, 0);
            returningItem = new Item();

            returningItem = new Minigun(3, "MINIGUN", 50);
            _logger.debug("Minigun Created");

            returningItem.Name = "MINIGUN";

        }
    }
}
