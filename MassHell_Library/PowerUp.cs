using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library
{
    public class PowerUp : Item
    {
        public int EffectTime { get; set; }
        public PowerUp(string name,int effectTime)
        {
            EffectTime = effectTime;
            Name = name;
        }
        public PowerUp()
        {

        }

    }
}
