using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MassHell_WPF.Composite
{
    internal class ParentUIElement : UI
    {
        List<UI> elements = new List<UI>();

        public ParentUIElement(FrameworkElement element, string name) : base(element, name)
        {
        }
        public void Add(UI element)
        {
            elements.Add(element);
        }
        public void Remove(UI element)
        {
            elements.Remove(element);
        }
        public FrameworkElement GetElement(string name)
        {
            return elements.Where(a => a.element.Name == name).FirstOrDefault().element;
        }
    }
}
