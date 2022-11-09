using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using MassHell_Library;
using System.Windows;

namespace MassHell_WPF
{
    internal class SignalRConnectionAdapter : IConnection
    {
        private HubConnection? adaptee;
        Logger connectionLogger = Logger.getInstance();


        public async Task Connect()
        {
            if (adaptee == null)
            {
                connectionLogger.debug("Connection hasn't been created");
                return;
            }
            await adaptee.StartAsync();

        }

        public void Create(string server)
        {
            adaptee = new HubConnectionBuilder()
            .WithUrl(server)
            .WithAutomaticReconnect()
            .Build();
            if (adaptee == null)
            {
                connectionLogger.debug("Connection creation failed");
            }   
        }

        public void Listen(string method,Action<dynamic> action)
        {
            connectionLogger.debug(action.Target.ToString());
            if (adaptee == null)
            {
                connectionLogger.debug("Connection hasn't been created");
                return;
            }
            adaptee.On(method, action);
        }
        public void Listen(string method, Action<dynamic,dynamic,dynamic> action)
        {
            connectionLogger.debug(action.Target.ToString());

            if (adaptee == null)
            {
                connectionLogger.debug("Connection hasn't been created");
                return;
            }
            adaptee.On<dynamic, dynamic, dynamic>(method, action);
        }
        public void Listen(string method,Action<dynamic, dynamic> action)
        {
            connectionLogger.debug(action.Target.ToString());

            if (adaptee == null)
            {
                connectionLogger.debug("Connection hasn't been created");
                return;
            }
            adaptee.On(method, action);
        }
        public async void Send(string method)
        {
            if (adaptee == null)
            {
                connectionLogger.debug("Connection hasn't been created");
                return;
            }
            await adaptee.InvokeAsync(method);
        }
        public async void Send(string method, object? arg1)
        {
            if (adaptee == null)
            {
                connectionLogger.debug("Connection hasn't been created");
                return;
            }
            await adaptee.InvokeAsync(method, arg1);
        }
        public async void Send(string method, object? arg1, object? arg2)
        {
            if (adaptee == null)
            {
                connectionLogger.debug("Connection hasn't been created");
                return;
            }
            await adaptee.InvokeAsync(method, arg1, arg2);
        }
        public async void Send(string method,object? arg1, object? arg2, object? arg3)
        {
            if (adaptee == null)
            {
                connectionLogger.debug("Connection hasn't been created");
                return;
            }
            await adaptee.InvokeAsync(method, arg1, arg2, arg3);
        }
    }
}
