using MassHell_Library;
using System;

namespace MassHell_Server.Mediator
{
    public class Chat : AbstractChat
    {
        public static List<Message> Messages;
        private readonly Logger _logger = Logger.getInstance();
        public Chat()
        {
            Messages = new List<Message>();
        }
        public static List<Recipient> connectedRecipients = new List<Recipient>();
        public override List<string> DisplayChat()
        {
            List<string> messages = new List<string>();
            messages.AddRange(Chat.Messages.Select(m => m.ToString()));
            return messages;
        }
        public override void AppendRecipient(Recipient player)
        {
            connectedRecipients.Add(player);
            player.Chat = this;
            
        }
        public override void RemoveRecipient(Recipient player)
        {
            connectedRecipients.Remove(player);
        }

        public override Message AddMessage(string text, string name)
        {
            Recipient recipient = connectedRecipients.Find(o => o.name == name);
                if (text.StartsWith("/"))
                {
                System rec = new System(name);
                    rec.name = "SYSTEM";
                    Message mes = rec.SendMessage(text);
                return mes;
                }
                else
            {
                recipient = recipient as Human;
                Message mes = recipient.SendMessage(text);
                Messages.Add(mes);
                return mes;
            }


        }
        public override void ClearChat()
        {
            if (connectedRecipients.Count > 0)
            {
                _logger.debug("There is still players online. Can't clear chat");
                return;
            }
            ClearChatMessages();
        }
        private void ClearChatMessages()
        {
            Messages.Clear();
        }

    }
}
