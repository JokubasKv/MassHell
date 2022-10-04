using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.AspNetCore.SignalR.Client;

namespace MassHell_WPF_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection? connection;
        public MainWindow()
        {

            InitializeComponent();
            Button btn = btn_Connect;
            btn.Click += Btn_Click;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
                        connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7200/gameengine")
                .Build();

            MessageBox.Show("Button's name is: " + sender.ToString());
            Connect();
            SendText("Hello there");
        }

        
        public async Task Connect()
        {
            await connection.StartAsync();
        }
        
        public async Task SendText(string text)
        {
            text = "Hello there";
            await connection.SendAsync("Message", text);
        }
    }
}
