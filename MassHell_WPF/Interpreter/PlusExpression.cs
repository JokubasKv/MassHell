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

        public override string getValue()
        {
            return left.getValue();
        }

        public override string execute()
        {
            var first = left.getValue();
            var second = right.getValue();
            if (first == "CTRL" && second == "T" && left.execute() == "BOSS" && right.execute() == "BOSS")
            {
                return "SpawnEnemy";
            }
            if (first == "CTRL" && second == "V" && left.execute() == "ITEM" && right.execute() == "ITEM")
            {
                return "SpawnMinigun";
            }
            return "";
        }
    }
}
