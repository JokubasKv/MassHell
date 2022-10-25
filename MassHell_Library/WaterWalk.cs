using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassHell_Library.Interfaces;

namespace MassHell_Library
{
    public class WaterWalk :PowerUp, IPowerUp
    {
        public int Duration { get; set; }
        public WaterWalk()
        {
            this.Duration = 30;
        }

        public int PowerupValue()
        {
            return Duration;
        }
    }
}
