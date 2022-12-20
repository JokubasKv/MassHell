using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_WPF.Iterator
{
    public class NullExpression : Expression
    {
        Expression left, right;

        public NullExpression()
        {

        }

        public override string getValue()
        {
            return "NULL";
        }

        public override string execute()
        {
            return "NULL";
        }
    }
}
