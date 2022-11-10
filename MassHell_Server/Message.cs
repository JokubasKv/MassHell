namespace MassHell_Server
{
    public class Message
    {
        public string sender { get; set; }
        public string message { get; set; }
        public TimeSpan time { get; set; }
        public Message(string sender, string message, TimeSpan time)
        {
            this.sender = sender;
            this.message = message;
            this.time = time;
        }
    }
}
