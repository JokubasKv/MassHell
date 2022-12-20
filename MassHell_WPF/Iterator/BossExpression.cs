using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_WPF.Iterator
{
    public class BossExpression : Expression
    {
        private string value;

        public BossExpression(string newValue)
        {
            this.value = newValue;
        }

        public override string getValue()
        {
            return value;
        }

        public override string execute()
        {
            return "BOSS";
        }
    }
}
