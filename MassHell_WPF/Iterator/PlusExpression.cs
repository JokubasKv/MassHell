using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_WPF.Iterator
{
    public class PlusExpression : Expression
    {
        Expression left, right;

        public PlusExpression(Expression left, Expression right)
        {
            this.left = left;
            this.right = right;
        }

        public PlusExpression()
        {
            left = new NullExpression();
            right = new NullExpression();
        }

        public override string execute()
        {
            return "SpawnEnemy";
        }
    }
}
