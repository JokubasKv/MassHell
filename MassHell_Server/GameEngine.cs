using Microsoft.AspNetCore.SignalR;
using MassHell_Library;
using System;
using MassHell_Library.AbstractFactory;

namespace MassHell_Server
{
    public class GameEngine : Hub
    {
        private static List<Player> connectedPlayers = new List<Player>();
        private readonly Logger _logger = Logger.getInstance();

        //Facade
        //public NotifyingSubSystem notifs = new NotifyingSubSystem();
        public CommunicationSubSystem comms = new CommunicationSubSystem();
        public SpawningSubSystem spawning = new SpawningSubSystem();

        public async void ConnectPlayer(Player p)
        {
            //Add player to connected players
            _logger.debug("New connected " + p.Name + " | " + connectedPlayers.Count);
            //If the player is not the first, draw every other connected player, so if somebody is late they can still see others.
            if (connectedPlayers.Count > 0)
            {
                _logger.debug("Telling " + p.Name + " to draw others");
                await Clients.Caller.SendAsync("DrawOtherPlayers", connectedPlayers);
            }
            connectedPlayers.Add(p);
            _logger.debug(connectedPlayers[0].ToString());



            //Tell all other players about new player
            await Clients.Others.SendAsync("PlayerConnected", p);
            comms.AppendPlayer(p);
        }
        public async Task UpdatePlayerPosition(Player p)
        {
            //Send every other client updated movement
            await Clients.Others.SendAsync("MoveOtherPlayer", p);

        }
        // Add more functionality to differ the use of facade
        public async Task SpawnEnemy()
        {
            Tile pos;
            Item returningItem;
            spawning.SpawnEnemy(out pos,out returningItem);
            await Clients.All.SendAsync("DrawItem", pos, returningItem);
        }
        // Add more functionality to differ the use of facade
        public async Task SpawnItem()
        {
            Tile pos;
            Item returningItem;
            spawning.SpawnItem(out pos, out returningItem);
            await Clients.All.SendAsync("DrawItem", pos, returningItem);

        }

        public async Task SpawnMinigun()
        {
            Tile pos;
            Item returningItem;
            spawning.SpawnMinigun(out pos, out returningItem);
            await Clients.All.SendAsync("DrawItem", pos, returningItem);

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
