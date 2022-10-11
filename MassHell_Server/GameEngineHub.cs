using Microsoft.AspNetCore.SignalR;
using MassHell_Library;

namespace MassHell_Server
{
    public class GameEngineHub : Hub
    {
        public async Task UpdatePlayerPosition(Tile player)
        {
            Console.WriteLine(Context.UserIdentifier);
            await Clients.Others.SendAsync("MoveOtherPlayer",player);

        }
    }
}
