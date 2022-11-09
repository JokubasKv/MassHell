using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_WPF
{
    internal interface IConnection
    {
        abstract Task Connect();
        abstract void Listen(string method,Action<dynamic> action);
        abstract void Listen(string method, Action<dynamic, dynamic> action);
        abstract void Listen(string method, Action<dynamic, dynamic, dynamic> action);

        abstract void Send(string method);

        abstract void Send(string method,object? arg1);
        abstract void Send(string method, object? arg1,object? arg2);
        abstract void Send(string method, object? arg1,object? arg2,object? arg3);

        abstract void Create(string server);

    }
}
