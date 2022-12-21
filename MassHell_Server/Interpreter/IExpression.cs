using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.Interpreter
{
    public interface IExpression
    {
        Dictionary<Tile, Item>Interpret(Context context, Tile position, Item returningItem);
    }
}
