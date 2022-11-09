using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public class FormObject : Tile
    {
        public string name;
        public FormObject(string name, double x, double y, double rotation) : base(x, y, rotation)
        {
            this.name = name;
        }
    }
}
