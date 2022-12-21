using Microsoft.AspNetCore.SignalR;
using MassHell_Library;
using System;
using MassHell_Library.AbstractFactory;
using MassHell_Server.Mediator;

namespace MassHell_Server
{
    public class GameEngine : Hub
    {
        private static List<Player> connectedPlayers = new List<Player>();
        private static Dictionary<string, string> connections = new Dictionary<string, string>();
        private readonly Logger _logger = Logger.getInstance();

        //Facade
        //public NotifyingSubSystem notifs = new NotifyingSubSystem();
        public static Chat comms = new Chat();
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
            if(!connections.ContainsKey(p.Name))
            {
                _logger.debug(p.Name);
                connections.Add(p.Name, Context.User.Identity.Name);
            }

            comms.AppendRecipient(new Human(p.Name));

            List<string> messages = comms.DisplayChat();

            await Clients.Caller.SendAsync("GetMessages", messages.TakeLast(10));
            //Tell all other players about new player
            await Clients.Others.SendAsync("PlayerConnected", p);

        }
        public async Task SendMessage(string sender,string message)
        {
            _logger.debug("Message sent from " + sender + ": " + message);
            //{
            //    Message returning = comms.CallSystemMessage(message);
            //    List<string> messages = comms.DisplayChat();
            //    messages.Add(returning.ToString());
            //    await Clients.Caller.SendAsync("GetMessages", messages.TakeLast(1));

            //}
            //else
            //{
            List<string> messages = comms.DisplayChat();
            int countBefore = messages.Count;
            Message current = comms.AddMessage(message,sender);
            messages = comms.DisplayChat();
            int countAfter = messages.Count;
            if(countBefore == countAfter)
            {
                messages.Add(current.ToString());
                await Clients.Caller.SendAsync("GetMessages", messages.TakeLast(1));
                messages.Remove(current.ToString());
            }
            else
            {
                await Clients.All.SendAsync("GetMessages", messages.TakeLast(1));
            }
            ///
            /// Can't figure out why it doesn't like to take connectionID as user identifier
            ///
            //    string userID = null; 
            //    connections.TryGetValue(sender.Name,out userID);
            //if (userID != null)
            //{
            //    _logger.debug("Banana:");
            //    await Clients.User(userID).SendAsync("GetMessages", messages.TakeLast(1));
            //    _logger.debug(Context.ConnectionId);
            //    await Clients.User(Context.ConnectionId).SendAsync("GetMessages", messages.TakeLast(1));

            //}
            // Make system say no user

            ////}

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
