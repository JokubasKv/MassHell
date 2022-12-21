using System.Text;

namespace MassHell_Server.Mediator
{
    public class System : Recipient
    {
        public System(string name) : base(name)
        {
            this.name = "SYSTEM";
        }
        public override Message SendMessage(string message)
        {
            StringBuilder returningMessage = new StringBuilder();
            switch (message)
            {
                case "/help":
                    returningMessage.Append("This message shows how to start");
                    break;
                case "/online":
                    returningMessage.Append("Currently online players :");
                    foreach (Human p in Chat.connectedRecipients)
                    {
                        returningMessage.Append(p.name + ", ");
                    }
                    break;
                default:
                    returningMessage.Append("Incorrect command");
                    break;
            }
            return new Message("System", returningMessage.ToString(), DateTime.Now.ToShortTimeString());
        }
    }
}
