using MassHell_Library;
using Microsoft.AspNetCore.SignalR;

namespace MassHell_Server
{

    // Does not connect to same server



    /// <summary>
    /// Used to seperate Object creation and notifying Clients
    /// </summary>
    //public class NotifyingSubSystem : Hub
    //{
    //    List<Player> connectedPlayers = new List<Player>();
    //    /// <summary>
    //    /// Logger to print to console
    //    /// </summary>
    //    private readonly Logger _logger = Logger.getInstance();

    //    //Don't know why Item is enemy
    //    public async Task DrawItem(Tile position, Item returningItem)
    //    {
    //        await Clients.All.SendAsync("DrawItem", position, returningItem);
    //    }
    //    public async Task ConnectPlayer(Player p)
    //    {

    //    }
    //    public async Task UpdatePos(Player p)
    //    {
    //    }
    //    public void AppendPlayer(Player player)
    //    {
    //        connectedPlayers.Add(player);
    //    }
    //    public void RemovePlayer(Player player)
    //    {
    //        connectedPlayers.Remove(player);
    //    }

    //}
}
