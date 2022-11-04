using Microsoft.AspNetCore.SignalR;
using MassHell_Library;
using System;
using MassHell_Library.AbstractFactory;

namespace MassHell_Server
{
    public class GameEngine : Hub
    {
        Random pos = new Random();

        static List<Player> connectedPlayers=new List<Player>();


        public async Task PlayerConnected(Player p)
        {
            //If the playeris not the first, draw every other connected player, so if somebody is late they can still see others.
            if (connectedPlayers.Count > 0) {
                Console.WriteLine("Telling "+ p.Name + " to draw others");
                await Clients.Caller.SendAsync("DrawOtherPlayers", connectedPlayers);
                InnerHolderSingleton.getInstance();
            }
            //Add player to connected players
            connectedPlayers.Add(p);
            Console.WriteLine("New connected " + p.Name + " | " + connectedPlayers.Count);

            //Tell all other players about new player
            await Clients.Others.SendAsync("PlayerConnected", p);
        }
       /* public async Task CreatePlayer(Player p)
        {
            Console.WriteLine("Player created");
            Console.WriteLine("Player Name : {0}", p.Name);
            await Clients.Others.SendAsync("CreatePlayer", p);
        }*/
        public async Task UpdatePlayerPosition(Player p)
        {
            //Send every other client updated movement
            await Clients.Others.SendAsync("MoveOtherPlayer",p);

        }
        public async Task SpawnItem()
        {
            int heightRandom =  pos.Next(1, 720);
            int widthRandom = pos.Next(1, 1280);
            Tile position = new Tile(widthRandom, heightRandom, 0);
            float chance = pos.NextSingle();
            Item returningItem;
            if (chance > 0.5)
            {
                // Change not to be hardcoded
                returningItem = new Weapon("InterestingName", 50, "Legendary");
                Console.WriteLine("Weapon Created");

            }
            else
            {
                returningItem = (Item)PowerUpFactory.getPowerUp("speedpowerup");
                //returningItem = new SpeedPowerUp(1.5f,"SPEED",20);

            }
            await Clients.All.SendAsync("DrawItem", position, returningItem);

        }

        /// <summary>
        /// Spawning enemies based on chance
        /// </summary>
        /// <returns>Enenmy that needs to spawn</returns>
        public async Task SpawnEnemy()
        {
            int heightRandom = pos.Next(1, 720);
            int widthRandom = pos.Next(1, 1280);
            Tile position = new Tile(widthRandom, heightRandom, 0);

            Random random = new Random();
            int chance = random.Next(0, 300);
            Item returningItem;

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
                else if(chance2 > 200)
                {
                    EnemyFacotry factory = new MageFactory();
                    returningItem = factory.CreateMedium();
                }
                else
                {
                    EnemyFacotry factory = new MageFactory();
                    returningItem = factory.CreateHard();
                }

            }
            else if(chance > 200)
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
            }
            returningItem.Name = "Enemy";
            await Clients.All.SendAsync("DrawItem", position, returningItem);
        }

        public Map CreateMap()
        {
            // Add logic to add rows of tiles with correct coords
            // y = 0 x = 0...1280 then y = 80 x=...1280
            Map map = new Map(720, 1280);
            for(int i =0;i<144;i++)
            {
                Tile empty = new Tile();
                map.tiles.Add(empty);
            }
            return map;

        }
    }
}
