using MassHell_Library;
using System;

namespace MassHell_Server
{
    public class Chat
    {
        public List<Message> Messages { get; private set; }
        private readonly Logger _logger = Logger.getInstance();

        public void AddMessage(string text,string name)
        {
            Messages.Add(new Message(name, text, DateTime.Now.TimeOfDay));
        }
        public void ClearChat(bool listEmpty)
        {
            if (listEmpty)
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
