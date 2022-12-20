namespace MassHell_Server
{
    public class Message
    {
        public string sender { get; set; }
        public string message { get; set; }
        public string time { get; set; }
        public Message(string sender, string message, string time)
        {
            this.sender = sender;
            this.message = message;
            this.time = time;
        }
        public override string ToString()
        {
            return "[" + time + " " + sender + "] " + message;
        }
    }
}
