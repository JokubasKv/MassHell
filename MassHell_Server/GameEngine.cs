using Microsoft.AspNetCore.SignalR;
using MassHell_Library;
using System;
using MassHell_Library.AbstractFactory;

namespace MassHell_Server
{
    public class GameEngine
    {
        private List<Player> connectedPlayers = new List<Player>();
        private readonly Logger _logger = Logger.getInstance();

        //Facade
        public NotifyingSubSystem notifs;
        public CommunicationSubSystem comms;
        public SpawningSubSystem spawning;

        public async void PlayerConnected(Player p)
        {
            //Add player to connected players
            connectedPlayers.Add(p);
            _logger.debug("New connected " + p.Name + " | " + connectedPlayers.Count);
            notifs.AppendPlayer(p);
            await notifs.ConnectPlayer(p);
            comms.AppendPlayer(p);
        }
        public async void UpdatePlayerPosition(Player p)
        {
            //Send every other client updated movement
            await notifs.UpdatePos(p);

        }
        // Add more functionality to differ the use of facade
        public void SpawnEnemy()
        {
            Tile pos;
            Item returningItem;
            spawning.SpawnEnemy(out pos,out returningItem);
            notifs.DrawItem(pos, returningItem);
        }
        // Add more functionality to differ the use of facade
        public void SpawnItem()
        {
            Tile pos;
            Item returningItem;
            spawning.SpawnItem(out pos, out returningItem);
            notifs.DrawItem(pos, returningItem);

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
