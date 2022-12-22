using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MassHell_Library;

namespace MassHell_WPF.Composite
{
    internal class UILeaf : UI
    {
        private readonly Logger _logger = Logger.getInstance();

        public UILeaf(FrameworkElement element, string name) : base(element, name)
        {
        }

        public void Print()
        {
            _logger.debug(element.GetType().ToString() + name);
        }
    }
}
