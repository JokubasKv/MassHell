using System;
using System.Collections.Generic;
using System.Linq;
using MassHell_Library;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using MassHell_WPF;
using System.Diagnostics;
using System.Xml.Linq;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace MassHell_WPF
{
    /// <summary>
    /// The 'Component' abstract class
    /// </summary>
   /* public abstract class Showage
    {
        protected  FormObject formObject;

        public FormObject FormObject       
        {
            get { return formObject; }
            set { formObject = value; }
        }
        public abstract FormObject Create(string name, int x, int y, string content);
        public abstract FormObject Move(int x, int y);

    }

    class ConcreteShowage : Showage
    {
        public ConcreteShowage(FormObject form)
        {
            formObject = form;
        }

        public override FormObject Create(string name, int x, int y, string content)
        {
            throw new NotImplementedException();
        }

        public override FormObject Move(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// The 'Decorator' abstract class
    /// </summary>
    public abstract class BaseDecorator : Showage
    {
        protected Showage component;
        public  BaseDecorator(Showage component)
        {
            this.component = component;
        }
        public override FormObject Create(string name, int x, int y, string content)
        {

           return component.Create(name,x,y,content);
        }
        public override FormObject Move(int x, int y)
        {

            return component.Move(x,y);
            
        }
    }
    /// <summary>
    /// The 'ConcreteDecoratorB' class
    /// </summary>
    public class LabelDecorator : BaseDecorator
    {
        Label l = new Label();

        public LabelDecorator(Showage component) : base(component)
        {
        }

        public override FormObject Create(string name, int x, int y, string content)
        {
            l.Name = name;
            l.Width = 240;
            l.Height = 30;
            l.Content = content;
            //Canvas foundCanvas = UIHelper.FindChild<Canvas>(Application.Current.MainWindow, "MainPanel");
            //foundCanvas.Children.Add(l);

            Debug.WriteLine($"Label Decorator Name={formObject.name} X={formObject.XCoordinate} Y ={formObject.YCoordinate}");

            
            Canvas.SetLeft(l, x);
            Canvas.SetTop(l, y);
            return (new FormObject(name, x, y, 0));
        }
        public override FormObject Move(int x, int y)
        {
            Debug.WriteLine($"Label Decorator Name={formObject.name} X={formObject.XCoordinate} Y ={formObject.YCoordinate}");
            Canvas.SetLeft(l, x);
            Canvas.SetTop(l, y);
            return (new FormObject(l.Name, x, y, 0));
        }
    }

    public class UIHelper
    {
        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
    }*/


    /// <summary>
    /// The 'Component' abstract class
    /// </summary>
    public abstract class Showwage
    {
        private FormObject formObject;
        public FormObject FormObject
        {
            get { return formObject; }
            set { formObject = value; }
        }
        public abstract void Create(string content);
    }
    /// <summary>
    /// The 'ConcreteComponent' class
    /// </summary>
    public class ConreteShowwage : Showwage
    {
        // Constructor
        public ConreteShowwage(FormObject formObject)
        {
            this.FormObject = formObject;
        }
        public override void Create(string content)
        {
            //Debug.WriteLine($"ConreteShowwage {FormObject}");
        }
    }
    /// <summary>
    /// The 'Decorator' abstract class
    /// </summary>
    public abstract class Decorator : Showwage
    {
        protected Showwage component;
        // Constructor
        public Decorator(Showwage show)
        {
            this.component = show;
        }
        public override void Create(string content)
        {
            component.Create(content);
        }
    }
    /// <summary>
    /// The 'ConcreteDecorator' class
    /// </summary>
    public class LabelDecorator : Decorator
    {
        Label l = new Label();
        // Constructor
        public LabelDecorator(Showwage show)
            : base(show)
        {
        }
        public override void Create(string content)
        {
            l.Name = "label"+ base.component.FormObject.name;
            l.Width = 240;
            l.Height = 30;
            l.Content = content;
            //Canvas foundCanvas = UIHelper.FindChild<Canvas>(Application.Current.MainWindow, "MainPanel");
            //foundCanvas.Children.Add(l);

            //Debug.WriteLine($"Label Decorator Name= {base.component.FormObject.name} X={base.component.FormObject.XCoordinate} Y ={base.component.FormObject.YCoordinate}");


            Canvas.SetLeft(l, base.component.FormObject.XCoordinate);
            Canvas.SetTop(l, base.component.FormObject.YCoordinate);

            base.Create(content);
        }
    }
    public class ImageDecorator : Decorator
    {
        Image imag = new Image();
        // Constructor
        public ImageDecorator(Showwage show)
            : base(show)
        {
        }
        public override void Create(string content)
        {
            imag.Name = "label" + base.component.FormObject.name;
            imag.Source = new BitmapImage(new Uri(content, UriKind.Relative));
        //Canvas foundCanvas = UIHelper.FindChild<Canvas>(Application.Current.MainWindow, "MainPanel");
        //foundCanvas.Children.Add(l);

        //Debug.WriteLine($"Image Decorator Name= {base.component.FormObject.name} X={base.component.FormObject.XCoordinate} Y ={base.component.FormObject.YCoordinate}");


            Canvas.SetLeft(imag, base.component.FormObject.XCoordinate);
            Canvas.SetTop(imag, base.component.FormObject.YCoordinate);

            base.Create(content);
        }
    }
    public class RectangleDecorator : Decorator
    {
        Rectangle rect = new Rectangle();
        // Constructor
        public RectangleDecorator(Showwage show)
            : base(show)
        {
        }
        public override void Create(string content)
        {
            rect.Name = "label" + base.component.FormObject.name;
            rect.Width = 240;
            rect.Height = 30;
            //Canvas foundCanvas = UIHelper.FindChild<Canvas>(Application.Current.MainWindow, "MainPanel");
            //foundCanvas.Children.Add(l);

            //Debug.WriteLine($"Rectangle Decorator Name= {base.component.FormObject.name} X={base.component.FormObject.XCoordinate} Y ={base.component.FormObject.YCoordinate}");


            Canvas.SetLeft(rect, base.component.FormObject.XCoordinate);
            Canvas.SetTop(rect, base.component.FormObject.YCoordinate);

            base.Create(content);
        }
    }
}
