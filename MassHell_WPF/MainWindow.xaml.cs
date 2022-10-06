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
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;
using MassHell_Library;
using System.Text.Unicode;
using System.Windows.Xps.Serialization;
using System.ComponentModel;

namespace MassHell_WPF
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
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7200/gameengine")
                .Build();

            //var user = new Image();
            //user.Name = "Player";
            //user.Height = 150;
            //user.Width = 150;
            //Uri resourceUri = new Uri("Images/Varn_token.png", UriKind.Relative);
            //user.Source = new BitmapImage(resourceUri);
            //user.Margin = new Thickness(0, 0, 0, 0);
            //MapXML.Children.Add(user);
        }
        public async void StartConnection()
        {
            await connection.StartAsync();
        }
        public void Write()
        {
            string text = "Calling message";
            connection.InvokeAsync<string>("Message", text);
        }
        public async void InitializeListeners()
        {
            connection.On<Player>("UpdatePlayerPosition", Player => MovePlayer());

        }
        //public void Print(string message)
        //{
        //    List<string> messages = new List<string>();
        //    messages.Add(message);
        //    TextList.ItemsSource = messages;
        //}
        private void MovePlayer()
        {
            Down_Left.Content = "We not here";
            Thickness current = Player.Margin;
            Player.Margin =new Thickness(current.Left+5, current.Top-5, current.Right-5, current.Bottom+5);
            //Player.UpdateLayout();
        }
        private void Connect_Click_1(object sender, RoutedEventArgs e)
        {
            StartConnection();
            InitializeListeners();
            Connect.Visibility = Visibility.Collapsed;
            //connection.InvokeAsync<Player>("MovePlayer");
            //MovePlayer();
        }

        private void MapXML_KeyDown(object sender, KeyEventArgs e)
        {
            // Won't work
            //if(e.Key == Key.Up)
            //{


            //}
            //else
            //{
            //    StartConnection();
            //}
        }

        private void Down_Left_Click(object sender, RoutedEventArgs e)
        {
            //MovePlayer();
            connection.InvokeAsync<Player>("UpdatePlayerPosition",Player);
        }
    }
}
