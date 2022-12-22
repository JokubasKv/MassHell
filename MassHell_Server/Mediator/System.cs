using System.Text;

namespace MassHell_Server.Mediator
{
    public class System : Recipient
    {
        public System(string name) : base(name)
        {
            this.name = name;
        }
        public override Message SendMessage(string message)
        {
            StringBuilder returningMessage = new StringBuilder();
            Recipient recipient = Chat.connectedRecipients.Find(o => o.name == name);

            switch (message)
            {
                case "/help":
                    returningMessage.Append("This message shows how to start");
                    break;
                case "/online":
                    foreach (Human p in Chat.connectedRecipients)
                    {
                        returningMessage.Append(p.name + ", ");
                    }
                    break;
                case "/muted":
                    returningMessage.Append("You are currently muted");
                    break;
                case "/mute":
                    recipient.muted = true;
                    returningMessage.Append("You are muted");
                    break;
                case "/unmute":
                    recipient.muted = false;
                    returningMessage.Append("You are unmuted");
                    break;
                default:
                    returningMessage.Append("Incorrect command");
                    break;
            }
            return new Message("System", returningMessage.ToString(), DateTime.Now.ToShortTimeString());
        }
    }
}
