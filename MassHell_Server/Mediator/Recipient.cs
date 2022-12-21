using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Server.Mediator
{
    public abstract class Recipient
    {
        public Recipient(string name)
        {
            this.name = name;
        }
        public string name { get; set; }

        Chat chat;
        public Chat Chat
        {
            set { chat = value; }
            get { return chat; }
        }
        public abstract Message SendMessage(string message);
    }
}
