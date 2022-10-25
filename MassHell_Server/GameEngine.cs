using Microsoft.AspNetCore.SignalR;
using MassHell_Library;
using MassHell_Library.Interfaces;
using System;

namespace MassHell_Server
{
    public class GameEngine : Hub
    {
        Random pos = new Random();


        public async Task PlayerConnected(Tile position,string name)
        {
            Console.WriteLine("New connected");
            await Clients.Others.SendAsync("PlayerConnected", position,name);
        }
        public async Task CreatePlayer(Tile pos, string name)
        {
            Console.WriteLine("Player created");
            Console.WriteLine("Player Name : {0}",name);
            await Clients.Others.SendAsync("CreatePlayer", pos,name);
        }
        public async Task UpdatePlayerPosition(Tile player,string name)
        {
            
            await Clients.Others.SendAsync("MoveOtherPlayer",player,name);

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
                Console.WriteLine("PowerUp Created");

            }
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

        // Factory that creates enemys
        IEnemyFactory factory = null;
        public async Task SpawnEnemy()
        {
            int heightRandom = pos.Next(1, 720);
            int widthRandom = pos.Next(1, 1280);
            Tile position = new Tile(widthRandom, heightRandom, 0);
            float chance = pos.NextSingle();
            Item returningItem;

            if (chance > 0.5)
            {
                factory = new Warrior(5, 100, 1.5f);
                Console.WriteLine("New Warior equipment:");
                Console.WriteLine(factory.GetWeapon().Item());
                Console.WriteLine(factory.GetArmor().Item());
                Console.WriteLine("-----------------------");
                returningItem = factory as Item;
            }
            else
            {
                factory = new Mage(10, 150, 2.5f);
                Console.WriteLine("New Mage equipment: ");
                Console.WriteLine(factory.GetWeapon().Item());
                Console.WriteLine(factory.GetArmor().Item());
                Console.WriteLine("-----------------------");
                returningItem = factory as Item;
            }
            await Clients.All.SendAsync("DrawItem", position, returningItem);
        }
    }
}
