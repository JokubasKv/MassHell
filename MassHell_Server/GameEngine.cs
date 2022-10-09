using Microsoft.AspNetCore.SignalR;
using MassHell_Library;

namespace MassHell_Server
{
    public class GameEngine : Hub
    {
        public async Task UpdatePlayerPosition(Tile player)
        {
            Console.WriteLine("Why  move");
            await Clients.Others.SendAsync("MoveOtherPlayer",player);

        }
    }
}
