using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MassHell_Library
{
    public class Item : Clonable
    {
        public string Name { get; set; }


        public override Item ShallowCopy()
        {
            return (Item)this.MemberwiseClone();
        }
        public override Item DeepCopy()
        {
            Item clone = (Item)this.MemberwiseClone();
            clone.Name = string.Copy(Name);
            return clone;
        }

    }



}
