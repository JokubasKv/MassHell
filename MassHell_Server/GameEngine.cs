using Microsoft.AspNetCore.SignalR;
using MassHell_Library;

namespace MassHell_Server
{
    public class GameEngine : Hub
    {
        
        public async Task Message(string text)
        {
            Console.WriteLine("Button pressed");
            await Clients.All.SendAsync("Receive", text);
        }
        public async Task UpdatePlayerPosition()
        {
            Console.WriteLine("Why no move");
            await Clients.All.SendAsync("MovePlayer");

        }
    }
}
