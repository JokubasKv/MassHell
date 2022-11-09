using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public class WaterWalk :PowerUp, IPowerUp
    {
        public int Duration { get; set; }
        public WaterWalk(int duration)
        {
            this.Duration = duration;
        }

        public int PowerupValue()
        {
            return Duration;
        }
    }
}
