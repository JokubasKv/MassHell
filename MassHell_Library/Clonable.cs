using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MassHell_Library
{
    public abstract class Clonable
    {
        //Protoype implementation
        public virtual Item ShallowCopy()
        {
            return (Item)this.MemberwiseClone();
        }

        public virtual Item  DeepCopy()
        {
            Item clone = (Item)this.MemberwiseClone();
            return clone;
        }
    }
}
