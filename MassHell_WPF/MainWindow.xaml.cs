using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.AspNetCore.SignalR.Client;
using MassHell_Library;
using System.Threading;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Linq;
using System.Numerics;

namespace MassHell_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection? connection;
        bool goLeft,goRight, goUp, goDown,invOpen = true;
        int speed = 25;

        PeriodicTimer gametimer = new PeriodicTimer(TimeSpan.FromMilliseconds(25));

        public MainWindow()
        {
            InitializeComponent();
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7200/gameengine")
                .WithAutomaticReconnect()
                .Build();
            MainPanel.Focus();
            StartConnection();
            InitializeListeners();

        }

        // Later will be changed to use Player class?
        private async void GameTimerEvent()
        {
            double rotation = 0;
            Image player = new Image();
            for(int i=0;i<MainPanel.Children.Count;i++)
            {
                if (MainPanel.Children[i] is Image)
                {
                    Image temp = (Image)MainPanel.Children[i];
                    if (temp.Name == UsernameBox.Text)
                    {
                        player = temp;
                    }
                }
            }
            Tile tile = new Tile(Canvas.GetLeft(player), Canvas.GetTop(player), rotation);
            //Username Text field going to be replaced by Player class
            await connection.InvokeAsync("PlayerConnected",tile,UsernameBox.Text);
            while (await gametimer.WaitForNextTickAsync())
            {
                // With system.types it is not possible to serialize
                // string player = JsonSerializer.Serialize(Player1);
                if (player == null)
                    break;
                tile = new Tile(Canvas.GetLeft(player), Canvas.GetTop(player), rotation);

                if (goLeft | goRight | goUp | goDown)
                    tile = MovePlayer(tile,player);
                await connection.InvokeAsync("UpdatePlayerPosition", tile,player.Name);
                rotation = tile.Rotation;

            }
        }

        public async void StartConnection()
        {
            await connection.StartAsync();

        }
        public void InitializeListeners()
        {
            // Later will be changed to use Player class?

            connection.On<Tile,string>("PlayerConnected",(position,name) =>
            {
                //connection.InvokeAsync("CreatePlayer", position, name);
                CreatePlayer(position, name);
            });
            // connection.On<Tile,string>("CreatePlayer", (position,name) => CreatePlayer(position,name));
            connection.On<Tile,string>("MoveOtherPlayer", (player,name) => MoveOtherPlayer(player,name));
            connection.On<Tile, Item>("DrawItem", (position, item) => DrawItem(position, item));
        }

        private void CreatePlayer(Tile position,string name)
        {
            if(MainPanel.FindName(name) != null)
            {
                return;
            }
            var user = new Image();
            user.Name = name;
            user.Height = 100;
            user.Width = 100;
            Uri resourceUri = new Uri("Images/Varn_token.png", UriKind.Relative);
            user.Source = new BitmapImage(resourceUri);
            Canvas.SetTop(user, position.XCoordinate);
            Canvas.SetLeft(user, position.YCoordinate);
            user.LayoutTransform = new RotateTransform(position.Rotation);  
            MainPanel.Children.Add(user);
        }

        // Changed to Player.cs later?
        private Tile MovePlayer(Tile player,Image Player)
        {
            Canvas.SetLeft(Player,player.XCoordinate);
            Canvas.SetTop(Player, player.YCoordinate);
            double centerX = (Canvas.GetLeft(Player) + Player.Width / 2);
            double centerY = (Canvas.GetTop(Player) + Player.Height / 2);
            RotateTransform rotate = new RotateTransform(0, centerX, centerY);
            if (goLeft && Canvas.GetLeft(Player) > 3)
            {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) - speed);
                rotate.Angle = 90;

                Player.LayoutTransform = rotate;

            }
            if (goRight && (Canvas.GetLeft(Player) + Player.Width + 3) < App.Current.MainWindow.ActualWidth)
            {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) + speed);
                rotate.Angle = -90;

                Player.LayoutTransform = rotate;
            }
            if (goUp && Canvas.GetTop(Player) > 15)
            {
                Canvas.SetTop(Player, Canvas.GetTop(Player) - speed);
                rotate.Angle = 180;

                Player.LayoutTransform = rotate;
            }
            if (goDown && (Canvas.GetTop(Player) + Player.Width + 15) < App.Current.MainWindow.ActualHeight)
            {
                Canvas.SetTop(Player, Canvas.GetTop(Player) + speed);
                rotate.Angle = 0;

                Player.LayoutTransform = rotate;
            }
            Player.UpdateLayout();
            player.XCoordinate = Canvas.GetLeft(Player);
            player.YCoordinate = Canvas.GetTop(Player);
            player.Rotation = rotate.Angle;
            return player;
        }

        public void DrawItem(Tile pos,Item item)
        {
            var user = new Image();
            user.Name = item.Name;
            user.Height = 100;
            user.Width = 100;
            Uri resourceUri;
            if (item.GetType() == typeof(Weapon))
            {
                resourceUri = new Uri("Images/sword.png", UriKind.Relative);
            }
            else
            {
                resourceUri = new Uri("Images/potion.png", UriKind.Relative);
            }
            user.Source = new BitmapImage(resourceUri);
            Canvas.SetTop(user, pos.XCoordinate + 50);
            Canvas.SetLeft(user, pos.YCoordinate + 50);
            user.LayoutTransform = new RotateTransform(pos.Rotation);
            MainPanel.Children.Add(user);
        }
        private void RegisterUser_Click(object sender, RoutedEventArgs e)
        {
            Tile tile = new Tile(0, 0, 0);
            CreatePlayer(tile,UsernameBox.Text);
            MainPanel.Visibility = Visibility.Visible;
            MainPanel.Focus();
            MainMenu.Visibility = Visibility.Collapsed;
            GameTimerEvent();
        }

        private void MoveOtherPlayer(Tile player,string name)
        {
            Image otherPlayer = new Image();
            for (int i = 0; i < MainPanel.Children.Count; i++)
            {
                if (MainPanel.Children[i] is Image)
                {
                    Image temp = (Image)MainPanel.Children[i];
                    if (temp.Name == name)
                    {
                        otherPlayer = temp;
                    }
                }
            }
            Canvas.SetLeft(otherPlayer, player.XCoordinate);
            Canvas.SetTop(otherPlayer, player.YCoordinate);
            otherPlayer.LayoutTransform = new RotateTransform(player.Rotation);
            otherPlayer.UpdateLayout();
        }
        /// <summary>
        /// Button control pressed down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPanel_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.A)
            {
                goLeft = true;
            }
            if (e.Key == Key.D)
            {
                goRight = true;
            }
            if (e.Key == Key.W)
            {
                goUp = true;
            }
            if (e.Key == Key.S)
            {
                goDown = true;
            }
            //Images for some reason dont load
            if(e.Key == Key.P)
            {
                connection.InvokeAsync("SpawnItem");
            }
            if(e.Key == Key.I)
            {
                OpenInventory(invOpen);
                invOpen = !invOpen;
            }
        }
        public void OpenInventory(bool open)
        {
            if(open)
            {
                Inventory.Visibility = Visibility.Visible;
                Inventory.Focus();
                return;
            }
            Inventory.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Button control when lifted up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPanel_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                goLeft = false;
            }
            if (e.Key == Key.D)
            {
                goRight = false;
            }
            if (e.Key == Key.W)
            {
                goUp = false;
            }
            if (e.Key == Key.S)
            {
                goDown = false;
            }
        }
    }
}
