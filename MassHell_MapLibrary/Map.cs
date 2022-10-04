using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_MapLibrary
{
    public class Map
    {
        List<Tile> tiles { get; set; }


        public Map()
        {
            tiles = new List<Tile>();
        }

    }
}
