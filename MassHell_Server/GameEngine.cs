using Microsoft.AspNetCore.SignalR;
using MassHell_MapLibrary;
namespace MassHell_Server
{
    public class GameEngine : Hub
    {
        
        public async Task Message(string text)
        {
            await Clients.All.SendAsync("Receive", text);
        }
    }
}
