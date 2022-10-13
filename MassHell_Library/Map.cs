using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public class Map
    {
        public List<Tile> tiles { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }


        public Map()
        {
            tiles = new List<Tile>();
        }
        public Map(int height, int width)
        {
            this.tiles = new List<Tile>();
            Height = height;
            Width = width;
        }
    }
}
