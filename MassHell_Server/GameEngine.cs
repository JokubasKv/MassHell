using Microsoft.AspNetCore.SignalR;
using MassHell_Library;

namespace MassHell_Server
{
    public class GameEngine : Hub
    {
        public async Task PlayerConnected(Tile position,string name)
        {
            Console.WriteLine("New connected");
            await Clients.Others.SendAsync("PlayerConnected", position,name);
        }
        public async Task CreatePlayer(Tile pos, string name)
        {
            Console.WriteLine("Player created");
            await Clients.Others.SendAsync("CreatePlayer", pos,name);
        }
        public async Task UpdatePlayerPosition(Tile player,string name)
        {
            
            await Clients.Others.SendAsync("MoveOtherPlayer",player,name);

        }
    }
}
