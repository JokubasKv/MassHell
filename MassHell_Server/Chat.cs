using MassHell_Library;
using System;

namespace MassHell_Server
{
    public class Chat
    {
        public static List<Message> Messages;
        private readonly Logger _logger = Logger.getInstance();
        public Chat()
        {
            Messages = new List<Message>();
        }
        public void AddMessage(string text,string name)
        {
            Messages.Add(new Message(name, text, DateTime.Now.ToShortTimeString()));
        }
        public void ClearChat(bool listEmpty)
        {
            if (!listEmpty)
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
