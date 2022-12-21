using MassHell_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace MassHell_WPF
{
    public abstract class Visualizer
    {
        protected Visualizer Next;

        public void SetNext(Visualizer next)
        {
            Next = next;
        }

        public abstract void Draw(Tile pos, Item item);
    }

    public class ItemVisualizer : Visualizer
    {
        public override void Draw(Tile pos, Item item)
        {

            Uri resourceUri = null;
            switch (item.Name)
            {
                case "Sword":
                    resourceUri = new Uri("Images/Sword.png", UriKind.Relative);
                    break;
                case "MINIGUN":
                    resourceUri = new Uri("Images/Minigun.png", UriKind.Relative);
                    break;
                default:
                    if (Next == null) break;
                    Next.Draw(pos, item);
                    break;
            }
            if (resourceUri != null)
            {
                var user = new Image();
                user.Name = item.Name;
                user.Height = 100;
                user.Width = 100;
                user.Source = new BitmapImage(resourceUri);
                Canvas.SetTop(user, pos.XCoordinate + 50);
                Canvas.SetLeft(user, pos.YCoordinate + 50);
                user.LayoutTransform = new RotateTransform(pos.Rotation);


                Canvas foundCanvas = UIHelper.FindChild<Canvas>(UIHelper.FindChild<Canvas>(Application.Current.MainWindow, "FirstCanvas"), "MainPanel");
                foundCanvas.Children.Add(user);
            }
        }
    }

    public class EnemyVisualizer : Visualizer
    {
        public override void Draw(Tile pos, Item item)
        {
            Uri resourceUri = null;
            switch (item.Name)
            {
                case "Mage":
                    resourceUri = new Uri("Images/Mage.png", UriKind.Relative);
                    break;
                case "Ninja":
                    resourceUri = new Uri("Images/Ninja.png", UriKind.Relative);
                    break;
                case "Warrior":
                    resourceUri = new Uri("Images/Warrior.png", UriKind.Relative);
                    break;
                case "BOSS":
                    resourceUri = new Uri("Images/Boss.png", UriKind.Relative);
                    break;
                default:
                    if (Next == null) break;
                    Next.Draw(pos, item);
                    break;
            }
            if (resourceUri != null)
            {
                var user = new Image();
                user.Name = item.Name;
                user.Height = 100;
                user.Width = 100;
                user.Source = new BitmapImage(resourceUri);
                Canvas.SetTop(user, pos.XCoordinate + 50);
                Canvas.SetLeft(user, pos.YCoordinate + 50);
                user.LayoutTransform = new RotateTransform(pos.Rotation);


                Canvas foundCanvas = UIHelper.FindChild<Canvas>(UIHelper.FindChild<Canvas>(Application.Current.MainWindow, "FirstCanvas"), "MainPanel");
                foundCanvas.Children.Add(user);
            }
        }
    }

    public class PotionVisualizer : Visualizer
    {
        public override void Draw(Tile pos, Item item)
        {
            Uri resourceUri = null;
            switch (item.Name)
            {
                case "healthboost":
                    resourceUri = new Uri("Images/potion6.png", UriKind.Relative);
                    break;
                case "damagepowerup":
                    resourceUri = new Uri("Images/potion.png", UriKind.Relative);
                    break;
                default:
                    if (Next == null) break;
                    Next.Draw(pos, item);
                    break;
            }
            if (resourceUri != null)
            {
                var user = new Image();
                user.Name = item.Name;
                user.Height = 100;
                user.Width = 100;
                user.Source = new BitmapImage(resourceUri);
                Canvas.SetTop(user, pos.XCoordinate + 50);
                Canvas.SetLeft(user, pos.YCoordinate + 50);
                user.LayoutTransform = new RotateTransform(pos.Rotation);


                Canvas foundCanvas = UIHelper.FindChild<Canvas>(UIHelper.FindChild<Canvas>(Application.Current.MainWindow, "FirstCanvas"), "MainPanel");
                foundCanvas.Children.Add(user);
            }
        }
    }
}
