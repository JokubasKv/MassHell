namespace MassHell_Server.Mediator
{
    public class ChatProxy : AbstractChat
    {
        private Chat realChat = new Chat();
        private List<Recipient> recipients = Chat.connectedRecipients;
        public override Message AddMessage(string text, string name)
        {

            // POssible profanities filter  

            Recipient recipient = recipients.Find(o => o.name == name);
            // text unmute temporary testing
            if(text == "/unmute")
            {
                return realChat.AddMessage(text, name);

            }
            if (recipient != null && recipient.muted)
            {
                System rec = new System(recipient.name);
                return rec.SendMessage("/muted");
            }
            return realChat.AddMessage(text, name);
        }

        public override void AppendRecipient(Recipient player)
        {
            realChat.AppendRecipient(player);
        }

        public override void ClearChat()
        {
            realChat.ClearChat();
        }

        public override List<string> DisplayChat()
        {
            return realChat.DisplayChat();
        }

        public override void RemoveRecipient(Recipient player)
        {
            realChat.RemoveRecipient(player);
        }
    }
}
