using MassHell_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.PlayerItems
{
    public class Cloak : IArmor
    {
        public string Item()
        {
            return "Magic cloak";
        }
    }
}
