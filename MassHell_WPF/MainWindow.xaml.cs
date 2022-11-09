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
using System.Collections.Generic;

namespace MassHell_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection? connection;
        int initialSpeed = 25;
        Player clientPlayer;

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

            //Tile tile = new Tile(Canvas.GetLeft(playerImage), Canvas.GetTop(playerImage), rotation);
            //Username Text field going to be replaced by Player class
            await connection.InvokeAsync("PlayerConnected",clientPlayer);
            while (await gametimer.WaitForNextTickAsync())
            {
                // With system.types it is not possible to serialize
                // string player = JsonSerializer.Serialize(Player1);
                if (playerImage == null)
                    break;
                //tile = new Tile(Canvas.GetLeft(playerImage), Canvas.GetTop(playerImage), rotation);

                if (clientPlayer.isMoving())
                    MovePlayer(clientPlayer,playerImage);
                await connection.InvokeAsync("UpdatePlayerPosition", clientPlayer);
                //rotation = tile.Rotation;

            }
        }

        public async void StartConnection()
        {
            await connection.StartAsync();

        }
        public void InitializeListeners()
        {
            // Later will be changed to use Player class?

            connection.On<Player>("PlayerConnected",(pplayer) =>
            {
                //connection.InvokeAsync("CreatePlayer", position, name);
                CreatePlayerImage(pplayer);
            });
            // connection.On<Tile,string>("CreatePlayer", (position,name) => CreatePlayerImage(position,name));
            connection.On<Player>("MoveOtherPlayer", (otherPlayer) => MoveOtherPlayer(otherPlayer));
            connection.On<List<Player>>("DrawOtherPlayers", (otherPlayers) => DrawOtherPlayers(otherPlayers));
            connection.On<Tile, Item>("DrawItem", (position, item) => DrawItem(position, item));
        }

        private void DrawOtherPlayers(List<Player> otherPlayers)
        {
            foreach (Player item in otherPlayers)
            {
                CreatePlayerImage(item);
            }
        }

        private void CreatePlayerImage(Player p)
        {
            if(MainPanel.FindName(p.Name) != null)
            {
                return;
            }
            //Create Image and add it to the panel
            var user = new Image();
            user.Name = p.Name;
            user.Height = 100;
            user.Width = 100;
            Uri resourceUri = new Uri("Images/Varn_token.png", UriKind.Relative);
            user.Source = new BitmapImage(resourceUri);
            Canvas.SetTop(user, p.XCoordinate);
            Canvas.SetLeft(user, p.YCoordinate);
            user.LayoutTransform = new RotateTransform(p.Rotation);  
            MainPanel.Children.Add(user);


        }

        // Changed to Player.cs later?
        private void MovePlayer(Player p,Image playerImage)
        {
            Canvas.SetLeft(playerImage,p.XCoordinate);
            Canvas.SetTop(playerImage, p.YCoordinate);
            double centerX = (Canvas.GetLeft(playerImage) + playerImage.Width / 2);
            double centerY = (Canvas.GetTop(playerImage) + playerImage.Height / 2);
            RotateTransform rotate = new RotateTransform(0, centerX, centerY);
            if (this.clientPlayer.goLeft && Canvas.GetLeft(playerImage) > 3)
            {
                Canvas.SetLeft(playerImage, Canvas.GetLeft(playerImage) - this.clientPlayer.Speed);
                rotate.Angle = 90;

                playerImage.LayoutTransform = rotate;

            }
            if (this.clientPlayer.goRight && (Canvas.GetLeft(playerImage) + playerImage.Width + 3) < App.Current.MainWindow.ActualWidth)
            {
                Canvas.SetLeft(playerImage, Canvas.GetLeft(playerImage) + this.clientPlayer.Speed);
                rotate.Angle = -90;

                playerImage.LayoutTransform = rotate;
            }
            if (this.clientPlayer.goUp && Canvas.GetTop(playerImage) > 15)
            {
                Canvas.SetTop(playerImage, Canvas.GetTop(playerImage) - this.clientPlayer.Speed);
                rotate.Angle = 180;

                playerImage.LayoutTransform = rotate;
            }
            if (this.clientPlayer.goDown && (Canvas.GetTop(playerImage) + playerImage.Width + 15) < App.Current.MainWindow.ActualHeight)
            {
                Canvas.SetTop(playerImage, Canvas.GetTop(playerImage) + this.clientPlayer.Speed);
                rotate.Angle = 0;

                playerImage.LayoutTransform = rotate;
            }
            playerImage.UpdateLayout();
            p.XCoordinate = Canvas.GetLeft(playerImage);
            p.YCoordinate = Canvas.GetTop(playerImage);
            p.Rotation = rotate.Angle;
        }

        public void DrawItem(Tile pos,Item item)
        {
            var user = new Image();
            user.Name = item.Name;
            user.Height = 100;
            user.Width = 100;
            Uri resourceUri;
            Console.WriteLine(item.GetType());
            if (item.Name == "InterestingName")
            {
                resourceUri = new Uri("Images/sword.png", UriKind.Relative);
            }
            else if (item.Name == "Mage")
            {
                resourceUri = new Uri("Images/Mage.png", UriKind.Relative);
            }
            else if (item.Name == "Ninja")
            {
                resourceUri = new Uri("Images/Ninja.png", UriKind.Relative);
            }
            else if (item.Name == "Warrior")
            {
                resourceUri = new Uri("Images/Warrior.png", UriKind.Relative);
            }
            else if (item.Name == "healthboost")
            {
                resourceUri = new Uri("Images/potion6.png", UriKind.Relative);
            }
            else if (item.Name == "damagepowerup")
            {
                resourceUri = new Uri("Images/potion.png", UriKind.Relative);
            }
            else
            {
                resourceUri = new Uri("Images/potion7.png", UriKind.Relative);
            }
            user.Source = new BitmapImage(resourceUri);
            Canvas.SetTop(user, pos.XCoordinate + 50);
            Canvas.SetLeft(user, pos.YCoordinate + 50);
            user.LayoutTransform = new RotateTransform(pos.Rotation);
            MainPanel.Children.Add(user);
        }

        private void RegisterUser_Click(object sender, RoutedEventArgs e)
        {
            //Create player that this client will control and maintain
            clientPlayer = new Player(0, UsernameBox.Text, 0, 0,0, initialSpeed, 0, 10, 1);
            CreatePlayerImage(clientPlayer);

            MainPanel.Visibility = Visibility.Visible;
            MainPanel.Focus();
            MainMenu.Visibility = Visibility.Collapsed;
            GameTimerEvent();
        }

        private void MoveOtherPlayer(Player otherPlayer)
        {
            Image otherPlayerImage = new Image();
            for (int i = 0; i < MainPanel.Children.Count; i++)
            {
                if (MainPanel.Children[i] is Image)
                {
                    Image temp = (Image)MainPanel.Children[i];
                    if (temp.Name == otherPlayer.Name)
                    {
                        otherPlayerImage = temp;
                    }
                }
            }
            Canvas.SetLeft(otherPlayerImage, otherPlayer.XCoordinate);
            Canvas.SetTop(otherPlayerImage, otherPlayer.YCoordinate);
            otherPlayerImage.LayoutTransform = new RotateTransform(otherPlayer.Rotation);
            otherPlayerImage.UpdateLayout();
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
                clientPlayer.MovementCommand("left", true);
            }
            if (e.Key == Key.D)
            {
                clientPlayer.MovementCommand("right", true);
            }
            if (e.Key == Key.W)
            {
                clientPlayer.MovementCommand("up", true);
            }
            if (e.Key == Key.S)
            {
                clientPlayer.MovementCommand("down", true);
            }
            //Images for some reason dont load
            if(e.Key == Key.P)
            {
                connection.InvokeAsync("SpawnItem");
            }
            if(e.Key == Key.I)
            {
                OpenInventory(clientPlayer.invOpen);
                clientPlayer.invOpen = !clientPlayer.invOpen;
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
                clientPlayer.MovementCommand("left", false);
            }
            if (e.Key == Key.D)
            {
                clientPlayer.MovementCommand("right", false);
            }
            if (e.Key == Key.W)
            {
                clientPlayer.MovementCommand("up", false);
            }
            if (e.Key == Key.S)
            {
                clientPlayer.MovementCommand("down", false);
            }
        }
    }
}
