using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MassHell_WPF.Composite
{
    public abstract class UI
    {
        public FrameworkElement element;
        public UI(FrameworkElement element, string name)
        {
            this.element = element;
            element.Name = name;
        }

        public string name { get; set; }

        public bool IsParent()
        {
            Panel elem = (Panel)element;
            if(element is Panel && elem.Children.Count > 0)
                return true;
            return false;
        }
    }
}
