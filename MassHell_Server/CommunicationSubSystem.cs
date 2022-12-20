using MassHell_Library;

namespace MassHell_Server
{
    public class CommunicationSubSystem
    {
        private static Chat chat;
        List<Player> connectedPlayers = new List<Player>();

        //Used to add all players to current session of chat
        public CommunicationSubSystem()
        {
            chat = new Chat();
        }
        public List<string> DisplayChat()
        {
            List<string> messages = new List<string>();
                messages.AddRange(Chat.Messages.Select(m => m.ToString()));
            return messages;
        }
        public void AddMessage(Player player,string message)
        {
            chat.AddMessage(message, player.Name);
        }
        public void ClearChat()
        {
            chat.ClearChat(connectedPlayers.Count < 1);   
        }
        public void AppendPlayer(Player player)
        {
            connectedPlayers.Add(player);
        }
        public void RemovePlayer(Player player)
        {
            connectedPlayers.Remove(player);
        }

    }
}
