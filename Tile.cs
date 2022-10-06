using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public class Tile
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
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
