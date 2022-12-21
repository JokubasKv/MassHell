using Microsoft.AspNetCore.SignalR;

namespace MassHell_Server.Mediator
{
    public class Human : Recipient
    {
        public Human(string name) : base(name)
        {

        }

        public override Message SendMessage(string message)
        {
            //Chat.AddMessage(message, name);
            return new Message(name, message, DateTime.Now.ToShortTimeString()); 
        }
    }
}
