using Microsoft.AspNetCore.SignalR;
using MassHell_Library;
using System;
using MassHell_Library.AbstractFactory;
using MassHell_Library.Interpreter;
using MassHell_Server.Interpreter;

namespace MassHell_Server
{
    public class GameEngine : Hub
    {
        private static List<Player> connectedPlayers = new List<Player>();
        private readonly Logger _logger = Logger.getInstance();

        //Facade
        //public NotifyingSubSystem notifs = new NotifyingSubSystem();
        public static CommunicationSubSystem comms = new CommunicationSubSystem();
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

            comms.AppendPlayer(p);

            List<string> messages = comms.DisplayChat();
            await Clients.Caller.SendAsync("GetMessages", messages.TakeLast(10));
            //Tell all other players about new player
            await Clients.Others.SendAsync("PlayerConnected", p);
        }
        public async Task SendMessage(Player sender,string message)
        {
            _logger.debug("Message sent from" + sender + ": " + message);
            comms.AddMessage(sender, message);

            Context context = new Context(message);

            Tile pos = new Tile();
            Item returningItem = new Item();

            var exp1 = new SpawnMinigunExpression();
            var result1 = exp1.Interpret(context, pos, returningItem);

            var exp2 = new SpawnBossExpression();
            var result2 =exp2.Interpret(context, pos, returningItem);


            if (result1.Count > 0)
            {
                await Clients.All.SendAsync("DrawItem", result1.Keys.First(), result1.Values.First());
            }
            else if (result2.Count > 0)
            {
                await Clients.All.SendAsync("DrawItem", result2.Keys.First(), result2.Values.First());
            }



            List<string> messages = comms.DisplayChat();
            await Clients.All.SendAsync("GetMessages", messages.TakeLast(10));
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
