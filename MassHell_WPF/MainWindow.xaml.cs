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

namespace MassHell_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection? connection;
        bool goLeft,goRight, goUp, goDown;
        int speed = 25;

        PeriodicTimer gametimer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));

        public MainWindow()
        {
            InitializeComponent();
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7200/gameengine")
                .WithAutomaticReconnect()
                .Build();

            //var user = new Image();
            //user.Name = "Player";
            //user.Height = 150;
            //user.Width = 150;
            //Uri resourceUri = new Uri("Images/Varn_token.png", UriKind.Relative);
            //user.Source = new BitmapImage(resourceUri);
            //user.Margin = new Thickness(0, 0, 0, 0);
            //MapXML.Children.Add(user);

            MainPanel.Focus();
            StartConnection();
            InitializeListeners();
            GameTimerEvent();


        }

        // Later will be changed to use Player class?
        private async void GameTimerEvent()
        {
            while(await gametimer.WaitForNextTickAsync())
            {
                // With system.types it is not possible to serialize
                // string player = JsonSerializer.Serialize(Player1);
                double rotation = 0;
                Tile tile = new Tile((Canvas.GetLeft(Player1)), (Canvas.GetTop(Player1)), rotation);
                await connection.InvokeAsync("UpdatePlayerPosition", tile);

                if(goLeft|goRight|goUp|goDown)
                    MovePlayer(tile);
                tile = new Tile((Canvas.GetLeft(Player1)), (Canvas.GetTop(Player1)), rotation);
            }
        }

        public async void StartConnection()
        {
            await connection.StartAsync();
        }
        public void InitializeListeners()
        {
            // Later will be changed to use Player class?

            //connection.On<Tile>("UpdatePlayerPosition",player => MoveOtherPlayer(player));
            connection.On<Tile>("MoveOtherPlayer", (player) =>
            {
                Text.Text = "I moved through server";

                Canvas.SetLeft(Player11, player.XCoordinate);
                Canvas.SetTop(Player11, player.YCoordinate);
                Player11.UpdateLayout();
            });


        }
        // Changed to Player.cs later?
        private void MovePlayer(Tile player)
        {
            Canvas.SetLeft(Player1,player.XCoordinate);
            Canvas.SetTop(Player1, player.YCoordinate);
            double centerX = (Canvas.GetLeft(Player1) + Player1.Width / 2);
            double centerY = (Canvas.GetTop(Player1) + Player1.Height / 2);
            RotateTransform rotate = new RotateTransform(0, centerX, centerY);
            if (goLeft && Canvas.GetLeft(Player1) > 3)
            {
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) - speed);
                rotate.Angle = 90;

                Player1.LayoutTransform = rotate;

            }
            if (goRight && (Canvas.GetLeft(Player1) + Player1.Width + 3) < App.Current.MainWindow.ActualWidth)
            {
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) + speed);
                rotate.Angle = -90;

                Player1.LayoutTransform = rotate;
            }
            if (goUp && Canvas.GetTop(Player1) > 15)
            {
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) - speed);
                rotate.Angle = 180;

                Player1.LayoutTransform = rotate;
            }
            if (goDown && (Canvas.GetTop(Player1) + Player1.Width + 15) < App.Current.MainWindow.ActualHeight)
            {
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) + speed);
                rotate.Angle = 0;

                Player1.LayoutTransform = rotate;
            }
            Player1.UpdateLayout();
        }
        private void MoveOtherPlayer(Tile player)
        {
            Debug.WriteLine(player.XCoordinate);
            Text.Text = "I moved through server";
            Canvas.SetLeft(Player11, player.XCoordinate);
            Canvas.SetTop(Player11, player.YCoordinate);
            //double centerX = (Canvas.GetLeft(Player11) + Player11.Width / 2);
            //double centerY = (Canvas.GetTop(Player11) + Player11.Height / 2);
            //RotateTransform rotate = new RotateTransform(0, centerX, centerY);
            //if (goLeft && Canvas.GetLeft(Player11) > 3)
            //{
            //    Canvas.SetLeft(Player11, Canvas.GetLeft(Player11) - speed);
            //    rotate.Angle = 90;

            //    Player11.LayoutTransform = rotate;

            //}
            //if (goRight && (Canvas.GetLeft(Player11) + Player11.Width + 3) < App.Current.MainWindow.ActualWidth)
            //{
            //    Canvas.SetLeft(Player11, Canvas.GetLeft(Player11) + speed);
            //    rotate.Angle = -90;

            //    Player11.LayoutTransform = rotate;
            //}
            //if (goUp && Canvas.GetTop(Player11) > 15)
            //{
            //    Canvas.SetTop(Player11, Canvas.GetTop(Player11) - speed);
            //    rotate.Angle = 180;

            //    Player11.LayoutTransform = rotate;
            //}
            //if (goDown && (Canvas.GetTop(Player11) + Player11.Width + 15) < App.Current.MainWindow.ActualHeight)
            //{
            //    Canvas.SetTop(Player11, Canvas.GetTop(Player11) + speed);
            //    rotate.Angle = 0;

            //    Player11.LayoutTransform = rotate;
            //}
            Player11.UpdateLayout();
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
