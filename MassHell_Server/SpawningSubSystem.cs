using MassHell_Library.AbstractFactory;
using MassHell_Library;
using Microsoft.AspNetCore.SignalR;
using System;
using MassHell_Library.Iterator;

namespace MassHell_Server
{
    public class SpawningSubSystem
    {
        private Random pos = new Random();
        private Logger _logger = Logger.getInstance();

        private Aggregate<Weapon> weapons = new Aggregate<Weapon>();
        private int weaponsIteratorIndex = 0;

        private Aggregate<Item> items = new Aggregate<Item>();
        private int itemIteratorIndex = 0;

        private Aggregate<Item> enemies = new Aggregate<Item>();
        private int ememyIteratorIndex = 0;

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

                var v = Enum.GetValues(typeof(Rarity));
                var rarity = (Rarity)v.GetValue(pos.Next(v.Length));
                var name = "Sword";

                int damage;
                if (rarity == Rarity.Legendary)
                {
                    damage = pos.Next(40, 50);
                }
                else if (rarity == Rarity.Rare)
                {
                    damage = pos.Next(30, 40);
                }
                else if (rarity == Rarity.Uncommon)
                {
                    damage = pos.Next(20, 30);
                }
                else if (rarity == Rarity.Common)
                {
                    damage = pos.Next(10, 20);
                }
                else
                {
                    damage = 2;
                }

                Weapon weapon = new Weapon(name, damage, rarity.ToString());
                weapons[weaponsIteratorIndex] = weapon;
                weaponsIteratorIndex++;

                var iterator = weapons.Iterator;
                while (iterator.IsLeft())
                {
                    _logger.debug("Added weapon to iterator: " + iterator.Current.Name + " Weapon, " + iterator.Current.Damage + " Damage, " + iterator.Current.Rarity + " Rarity");

                    iterator.Next();
                }


                returningItem = weapon;


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
                    returningItem.Name = "healthboost";
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

                items[itemIteratorIndex] = returningItem;
                itemIteratorIndex++;

                var iterator = items.Iterator;
                while (iterator.IsLeft())
                {
                    _logger.debug("Added item to iterator: " + iterator.Current.Name + " item");

                    iterator.Next();
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

            enemies[ememyIteratorIndex] = returningItem;
            ememyIteratorIndex++;

            var iterator = enemies.Iterator;
            while (iterator.IsLeft())
            {
                _logger.debug("Added enemy to iterator: " + iterator.Current.Name + " enemy");

                iterator.Next();
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

            returningItem.Name = "MINIGUN";

        }
    }
}
