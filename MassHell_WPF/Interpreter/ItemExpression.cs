using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_WPF.Iterator
{
    public class ItemExpression : Expression
    {
        private string value;

        public ItemExpression(string newValue)
        {
            this.value = newValue;
        }

        public override string getValue()
        {
            return value;
        }

        public override string execute()
        {
            return "ITEM";
        }
    }
}
