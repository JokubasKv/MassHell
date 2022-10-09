using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public class Tile
    {
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }
        public double Rotation { get; set; }
        public Tile()
        {
        }
        public Tile(double xCoordinate, double yCoordinate, double rotation)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Rotation = rotation;
        }
    }

}
