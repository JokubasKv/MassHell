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
        int initialSpeed = 25;
        Player player;

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
            Image playerImage = new Image();
            for(int i=0;i<MainPanel.Children.Count;i++)
            {
                if (MainPanel.Children[i] is Image)
                {
                    Image temp = (Image)MainPanel.Children[i];
                    if (temp.Name == UsernameBox.Text)
                    {
                        playerImage = temp;
                    }
                }
            }

            Tile tile = new Tile(Canvas.GetLeft(playerImage), Canvas.GetTop(playerImage), rotation);
            //Username Text field going to be replaced by Player class
            await connection.InvokeAsync("PlayerConnected",tile,UsernameBox.Text);
            while (await gametimer.WaitForNextTickAsync())
            {
                // With system.types it is not possible to serialize
                // string player = JsonSerializer.Serialize(Player1);
                if (playerImage == null)
                    break;
                tile = new Tile(Canvas.GetLeft(playerImage), Canvas.GetTop(playerImage), rotation);

                if (player.isMoving())
                    tile = MovePlayer(tile,playerImage);
                await connection.InvokeAsync("UpdatePlayerPosition", tile,playerImage.Name);
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
            //Create Image and add it to the panel
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

            //Create player that this client will control and maintain
            player = new Player(0, name, position.XCoordinate, position.YCoordinate, initialSpeed, 0, 10, 1);
        }

        // Changed to Player.cs later?
        private Tile MovePlayer(Tile playerTile,Image playerImage)
        {
            Canvas.SetLeft(playerImage,playerTile.XCoordinate);
            Canvas.SetTop(playerImage, playerTile.YCoordinate);
            double centerX = (Canvas.GetLeft(playerImage) + playerImage.Width / 2);
            double centerY = (Canvas.GetTop(playerImage) + playerImage.Height / 2);
            RotateTransform rotate = new RotateTransform(0, centerX, centerY);
            if (player.goLeft && Canvas.GetLeft(playerImage) > 3)
            {
                Canvas.SetLeft(playerImage, Canvas.GetLeft(playerImage) - player.Speed);
                rotate.Angle = 90;

                playerImage.LayoutTransform = rotate;

            }
            if (player.goRight && (Canvas.GetLeft(playerImage) + playerImage.Width + 3) < App.Current.MainWindow.ActualWidth)
            {
                Canvas.SetLeft(playerImage, Canvas.GetLeft(playerImage) + player.Speed);
                rotate.Angle = -90;

                playerImage.LayoutTransform = rotate;
            }
            if (player.goUp && Canvas.GetTop(playerImage) > 15)
            {
                Canvas.SetTop(playerImage, Canvas.GetTop(playerImage) - player.Speed);
                rotate.Angle = 180;

                playerImage.LayoutTransform = rotate;
            }
            if (player.goDown && (Canvas.GetTop(playerImage) + playerImage.Width + 15) < App.Current.MainWindow.ActualHeight)
            {
                Canvas.SetTop(playerImage, Canvas.GetTop(playerImage) + player.Speed);
                rotate.Angle = 0;

                playerImage.LayoutTransform = rotate;
            }
            playerImage.UpdateLayout();
            playerTile.XCoordinate = Canvas.GetLeft(playerImage);
            playerTile.YCoordinate = Canvas.GetTop(playerImage);
            playerTile.Rotation = rotate.Angle;
            return playerTile;
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
                player.MovementCommand("left", true);
            }
            if (e.Key == Key.D)
            {
                player.MovementCommand("right", true);
            }
            if (e.Key == Key.W)
            {
                player.MovementCommand("up", true);
            }
            if (e.Key == Key.S)
            {
                player.MovementCommand("down", true);
            }
            //Images for some reason dont load
            if(e.Key == Key.P)
            {
                connection.InvokeAsync("SpawnItem");
            }
            if(e.Key == Key.I)
            {
                OpenInventory(player.invOpen);
                player.invOpen = !player.invOpen;
            }
            if (e.Key == Key.H)
            {
                connection.InvokeAsync("SpawnEnemy");
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
                player.MovementCommand("left", false);
            }
            if (e.Key == Key.D)
            {
                player.MovementCommand("right", false);
            }
            if (e.Key == Key.W)
            {
                player.MovementCommand("up", false);
            }
            if (e.Key == Key.S)
            {
                player.MovementCommand("down", false);
            }
        }
    }
}
