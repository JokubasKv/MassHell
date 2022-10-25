using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.Interfaces
{
    public interface IEnemyFactory
    {
        IWeapon GetWeapon();
        IArmor GetArmor();
    }
}
