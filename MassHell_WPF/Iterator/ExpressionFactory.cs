using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_WPF.Iterator
{
    public class ExpressionFactory
    {

        public static Expression getExpression(string token, Expression left, Expression right)
        {
            Expression exp = new NullExpression();
            if (token.Equals("BOSS"))
            {
                
            }
            else if (token.Equals("ITEM"))
            {
                
            }
            return exp;
        }
    }
}
