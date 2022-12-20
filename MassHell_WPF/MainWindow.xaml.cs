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
using System.Runtime;
using System.Runtime.InteropServices;
using MassHell_WPF.Iterator;
using Expression = MassHell_WPF.Iterator.Expression;
using System.Linq;
using System.Collections;

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


        // Realise check if disconnected. Needed for future
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
        private async void GameTimerEvent(string username)
        {
            double rotation = 0;
            Image playerImage = new Image();
            for (int i = 0; i < MainPanel.Children.Count; i++)
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

            Label label = new Label();
            label.Name = "McLabel";
            label.Width = 240;
            label.Height = 30;
            label.Content = username;

            MainPanel.Children.Add(label);

            //Tile tile = new Tile(Canvas.GetLeft(playerImage), Canvas.GetTop(playerImage), rotation);
            //Username Text field going to be replaced by Player class
            await connection.InvokeAsync("ConnectPlayer", clientPlayer);
            while (await gametimer.WaitForNextTickAsync())
            {
                // With system.types it is not possible to serialize
                // string player = JsonSerializer.Serialize(Player1);
                if (playerImage == null)
                    break;
                //tile = new Tile(Canvas.GetLeft(playerImage), Canvas.GetTop(playerImage), rotation);

                if (clientPlayer.isMoving())
                {
                    //MovePlayer(clientPlayer,playerImage);
                    //NewMovePlayer(clientPlayer);
                    NewerMovePlayer(clientPlayer);
                    MoveFormObject(new FormObject(label.Name, clientPlayer.XCoordinate, clientPlayer.YCoordinate, clientPlayer.Rotation));
                }
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

            connection.On<Player>("PlayerConnected", (pplayer) =>
            {
                //connection.InvokeAsync("CreatePlayer", position, name);
                CreatePlayerImage(pplayer);
            });
            // connection.On<Tile,string>("CreatePlayer", (position,name) => CreatePlayerImage(position,name));
            connection.On<Player>("MoveOtherPlayer", (otherPlayer) => MoveOtherPlayer(otherPlayer));
            connection.On<List<Player>>("DrawOtherPlayers", (otherPlayers) => DrawOtherPlayers(otherPlayers));
            connection.On<Tile, Item>("DrawItem", (position, item) => DrawItem(position, item));
            connection.On<List<string>>("GetMessages", (messages) => DisplayMessages(messages));
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
            if (MainPanel.FindName(p.Name) != null)
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
        private void MovePlayer(Player p, Image playerImage)
        {
            Canvas.SetLeft(playerImage, p.XCoordinate);
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

        private void NewMovePlayer(Player p)
        {
            RotateTransform rotate = new RotateTransform(0, p.XCoordinate, p.YCoordinate);
            if (this.clientPlayer.goLeft && p.XCoordinate > 3)
            {
                p.XCoordinate -= this.clientPlayer.Speed;
                rotate.Angle = 90;

            }
            if (this.clientPlayer.goRight && (p.XCoordinate) < App.Current.MainWindow.ActualWidth)
            {
                p.XCoordinate += this.clientPlayer.Speed;
                rotate.Angle = -90;
            }
            if (this.clientPlayer.goUp && p.YCoordinate > 15)
            {
                p.YCoordinate -= this.clientPlayer.Speed;
                rotate.Angle = 180;
            }
            if (this.clientPlayer.goDown && p.YCoordinate < App.Current.MainWindow.ActualHeight)
            {
                p.YCoordinate += this.clientPlayer.Speed;
                rotate.Angle = 0;
            }

            MoveFormObject(new FormObject(p.Name, p.XCoordinate, p.YCoordinate, p.Rotation));

            p.Rotation = rotate.Angle;
        }
        private void NewerMovePlayer(Player p)
        {
            FormObject temp = new FormObject("",0,0,0);

            if (p.goLeft && p.XCoordinate > 3)
            {
                temp = p.Move("left", clientPlayer.Speed);
            }
            if (p.goRight && (p.XCoordinate) < App.Current.MainWindow.ActualWidth)
            {
                temp = p.Move("right", clientPlayer.Speed);
            }
            if (p.goUp && p.YCoordinate > 15)
            {
                temp = p.Move("up", clientPlayer.Speed);
            }
            if (p.goDown && p.YCoordinate < App.Current.MainWindow.ActualHeight)
            {
                temp = p.Move("down", clientPlayer.Speed);
            }

            MoveFormObject(temp);

        }
        private void MoveFormObject(FormObject formObject)
        {
            UIElement obj= new UIElement();
            //var obj = (UIElement)MainPanel.FindName(formObject.name);
            for (int i = 0; i < MainPanel.Children.Count; i++)
            {
                if (MainPanel.Children[i] is Image)
                {
                    Image temp = (Image)MainPanel.Children[i];
                    if (temp.Name == formObject.name)
                    {
                        obj = temp;
                    }
                }
                if (MainPanel.Children[i] is Label)
                {
                    Label temp = (Label)MainPanel.Children[i];
                    if (temp.Name == formObject.name)
                    {
                        obj = temp;
                    }
                }
            }

            if (obj is Image)
            {
                Image temp = (Image)obj;
                Canvas.SetLeft(temp, formObject.XCoordinate);
                Canvas.SetTop(temp, formObject.YCoordinate);
                temp.LayoutTransform = new RotateTransform(formObject.Rotation);

                //Debug.WriteLine(obj);
            }
            if(obj is Label)
            {
                Label temp = (Label)obj;
                Canvas.SetLeft(obj, formObject.XCoordinate);
                Canvas.SetTop(temp, formObject.YCoordinate);

                //Debug.WriteLine(obj);
            }
        }

        public void DrawItem(Tile pos,Item item)
        {
            var user = new Image();
            user.Name = item.Name;
            user.Height = 100;
            user.Width = 100;
            Uri resourceUri;
            Console.WriteLine(item.GetType());
            if (item.Name == "Sword")
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
            else if (item.Name == "MINIGUN")
            {
                resourceUri = new Uri("Images/Minigun.png", UriKind.Relative);
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
            clientPlayer = new Player(0, UsernameBox.Text, 0, 0, 0, initialSpeed, 0, 10, 1);
            CreatePlayerImage(clientPlayer);

            MainPanel.Visibility = Visibility.Visible;
            MainPanel.Focus();
            MainMenu.Visibility = Visibility.Collapsed;
            GameTimerEvent(UsernameBox.Text);
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
            if (e.Key == Key.A)
            {
                clientPlayer.goLeft = true;
            }
            if (e.Key == Key.D)
            {
                clientPlayer.goRight = true;
            }
            if (e.Key == Key.W)
            {
                clientPlayer.goUp = true;
            }
            if (e.Key == Key.S)
            {
                clientPlayer.goDown = true;
            }
            //Images for some reason dont load
            if (e.Key == Key.P)
            {
                connection.InvokeAsync("SpawnItem");
            }
            if (e.Key == Key.I)
            {
                OpenInventory(clientPlayer.invOpen);
                clientPlayer.invOpen = !clientPlayer.invOpen;
            }
            if (e.Key == Key.H)
            {
                connection.InvokeAsync("SpawnEnemy");
            }
            if (e.Key == Key.U)
            {
               MoveFormObject(clientPlayer.UndoMove());
            }
            if (e.Key == Key.O)
            {
                var concrete = new ConreteShowwage(new FormObject("base", 0, 0, 0));
                Debug.WriteLine(concrete.FormObject.name);
                var label = new LabelDecorator(concrete);
                label.Create("Hello");

                var image = new ImageDecorator(label);
                image.Create("Stuff");
            }

            if (Keyboard.IsKeyDown(Key.T) && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                Expression e1 = new BossExpression("CTRL");
                Expression e2 = new BossExpression("T");

                Expression query = new PlusExpression(e1, e2);

                var result = query.execute();

                connection.InvokeAsync(result);
            }

            if (Keyboard.IsKeyDown(Key.V) && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                Expression e1 = new ItemExpression("CTRL");
                Expression e2 = new ItemExpression("V");

                Expression query = new PlusExpression(e1, e2);

                var result = query.execute();

                connection.InvokeAsync(result);
            }

            if (e.Key == Key.L)
            {
                var x = Key.L;
                Item p1 = new Item();
                p1.Name = "First item";

                // Perform a shallow copy of p1 and assign it to p2.
                Item p2 = p1.ShallowCopy();
                // Make a deep copy of p1 and assign it to p3.
                Item p3 = p1.DeepCopy();

                // Display values of p1, p2 and p3.
                Debug.WriteLine("Original values of p1, p2, p3:");
                Debug.WriteLine("   p1 instance values: ");
                DisplayValues(p1);
                Debug.WriteLine("   p2 instance values:");
                DisplayValues(p2);
                Debug.WriteLine("   p3 instance values:");
                DisplayValues(p3);

                // Change the value of p1 properties and display the values of p1,
                // p2 and p3.
                p1.Name = "A different name";
                Debug.WriteLine("\nValues of p1, p2 and p3 after changes to p1:");
                Debug.WriteLine("   p1 instance values: ");
                DisplayValues(p1);
                Debug.WriteLine("   p2 instance values (reference values have changed):");
                DisplayValues(p2);
                Debug.WriteLine("   p3 instance values (everything was kept the same):");
                DisplayValues(p3);
            }
            if (e.Key == Key.Enter)
            {
                clientPlayer.chatOpen = !clientPlayer.chatOpen;
                Canvas canvas = Chat;
                canvas.Visibility = Visibility.Visible;
                StackPanel stack = canvas.Children.OfType<StackPanel>().FirstOrDefault();
                if (clientPlayer.chatOpen == true && canvas != null)
                {
                    canvas.Opacity = 1.0;

                    Label label = stack.Children.OfType<Label>().FirstOrDefault();
                    TextBox enter = stack.Children.OfType<TextBox>().FirstOrDefault();
                    if (enter != null)
                    {
                        enter.Focus();
                        enter.Visibility = Visibility.Visible;
                    }
                    FocusManager.SetFocusedElement(Main, enter);
                }
            }

        }

        public static void DisplayValues(Item p)
    {
            GCHandle handle = GCHandle.Alloc(p, GCHandleType.WeakTrackResurrection);
            long address = GCHandle.ToIntPtr(handle).ToInt64();
            Debug.WriteLine("      Name: {0:s}, Hashcode: {1:d}, Memory: {1:d}",
            p.Name, p.GetHashCode());
    }
    public void OpenInventory(bool open)
        {
            if (open)
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
                clientPlayer.goLeft = false;
            }
            if (e.Key == Key.D)
            {
                clientPlayer.goRight = false;
            }
            if (e.Key == Key.W)
            {
                clientPlayer.goUp = false;
            }
            if (e.Key == Key.S)
            {
                clientPlayer.goDown = false;
            }
        }

        private async void TextBox_KeyDown(object sender, KeyEventArgs e)
        {

            if(e.Key == Key.Return)
            {
                clientPlayer.chatOpen = !clientPlayer.chatOpen;
                Canvas canvas = Chat;
                StackPanel stack = canvas.Children.OfType<StackPanel>().FirstOrDefault();
                canvas.Opacity = 0.6;
                TextBox enter = stack.Children.OfType<TextBox>().FirstOrDefault();
                if (enter != null)
                {
                    MainPanel.Focus();
                    enter.Visibility = Visibility.Hidden;
                    string message = enter.Text;
                    enter.Text = "";
                    //label.Content = "[" + clientPlayer.Name + " " + DateTime.Now.ToShortTimeString() + "] " + message;
                    await connection.InvokeAsync("SendMessage", clientPlayer, message);

                }
                MainPanel.Focus();
            }
        }
        private void DisplayMessages(List<string> messages)
        {
            StackPanel stack = Chat.Children.OfType<StackPanel>().FirstOrDefault();
            stack.Children.Clear();
            stack.Children.Add(EnterChat);
            foreach (var m in messages)
            {
                Label label = new Label();
                label.Content = m;
                stack.Children.Add(label);
            }
        }
    }
}