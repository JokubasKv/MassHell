using MassHell_Library;

namespace MassHell_Server.Mediator
{
    public abstract class AbstractChat
    {
        public abstract Message AddMessage(string text, string name);
        public abstract List<string> DisplayChat();
        public abstract void ClearChat();
        public abstract void AppendRecipient(Recipient player);
        public abstract void RemoveRecipient(Recipient player);

    }
}
