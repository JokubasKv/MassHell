using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_MapLibrary
{
    internal class Tile
    {
        private int XCoordinate { get; set; }
        private int YCoordinate { get; set; }
        public Tile()
        {
        }
        public Tile(int xCoordinate, int yCoordinate)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }
    }

}
