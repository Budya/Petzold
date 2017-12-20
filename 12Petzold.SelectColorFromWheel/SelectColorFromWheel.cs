using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using _04Petzold.CircleTheButtons;

namespace _12Petzold.SelectColorFromWheel
{
    class SelectColorFromWheel : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new SelectColorFromWheel());
        }
        public SelectColorFromWheel()
        {
            Title = "Select Color From Wheel";
            SizeToContent = SizeToContent.WidthAndHeight;

            // Creation StackPanel object as window content
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;

            // Creation fake button for focuse test
            Button btn = new Button();
            btn.Content = "Do-nothing button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);

            // Creation ColorWheel element
            ColorWheel clrwheel = new ColorWheel();
            clrwheel.Margin = new Thickness(24);
            clrwheel.HorizontalAlignment = HorizontalAlignment.Center;
            clrwheel.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(clrwheel);

            // Binding Background property of window to selected color
            clrwheel.SetBinding(ColorWheel.SelectedValueProperty, "Background");
            clrwheel.DataContext = this;

            // Creation another fake button
            btn = new Button();
            btn.Content = "Do-nothing button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);
        }
    }
}
