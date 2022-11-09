using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public class Logger
    {
        private static readonly Logger instance = new Logger();
        private Logger() { }

        public void debug(string text)
        {
            Console.WriteLine(text);
        }

        public static Logger getInstance()
        {
            //Console.WriteLine(instance.GetHashCode());
            return instance;
        }
    }
}
