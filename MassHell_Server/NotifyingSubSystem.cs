using MassHell_Library;
using Microsoft.AspNetCore.SignalR;

namespace MassHell_Server
{
    /// <summary>
    /// Used to seperate Object creation and notifying Clients
    /// </summary>
    public class NotifyingSubSystem : Hub
    {
        List<Player> connectedPlayers = new List<Player>();
        /// <summary>
        /// Logger to print to console
        /// </summary>
        private readonly Logger _logger = Logger.getInstance();

        //Don't know why Item is enemy
        public async Task DrawItem(Tile position, Item returningItem)
        {
            await Clients.All.SendAsync("DrawItem", position, returningItem);
        }
        public async Task ConnectPlayer(Player p)
        {
            //If the player is not the first, draw every other connected player, so if somebody is late they can still see others.
            if (connectedPlayers.Count > 0)
            {
                _logger.debug("Telling " + p.Name + " to draw others");
                await Clients.Caller.SendAsync("DrawOtherPlayers", connectedPlayers);
            }


            //Tell all other players about new player
            await Clients.Others.SendAsync("PlayerConnected", p);
        }
        public async Task UpdatePos(Player p)
        {
            await Clients.Others.SendAsync("MoveOtherPlayer", p);
        }
        public void AppendPlayer(Player player)
        {
            connectedPlayers.Add(player);
        }
        public void RemovePlayer(Player player)
        {
            connectedPlayers.Remove(player);
        }

    }
}
