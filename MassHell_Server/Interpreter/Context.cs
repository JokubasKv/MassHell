using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.Interpreter
{
    public class Context
    {
        public string _commandText { get; set; }

        public Context(string commandText)
        {
            this._commandText = commandText;
        }
    }
}
