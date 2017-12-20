using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _11Petzold.SelectColorFromGrid
{
    class SelectColorFromGrid : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new SelectColorFromGrid());
        }
        public SelectColorFromGrid()
        {
            Title = "Select Color From Grid";
            SizeToContent = SizeToContent.WidthAndHeight;
            
            // Creation StackPanel object as window content
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;

            // Fake button for focus test
            Button btn = new Button();
            btn.Content = "Do-nothing button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);

            // Creation ColorGridBox object
            ColorGridBox clrgrid = new ColorGridBox();
            clrgrid.Margin = new Thickness(24);
            clrgrid.HorizontalAlignment = HorizontalAlignment.Center;
            clrgrid.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(clrgrid);

            // Binding property Background of window
            // to selected value of ColorGridBox
            clrgrid.SetBinding(ColorGridBox.SelectedValueProperty, "Background");
            clrgrid.DataContext = this;

            // Creation second fake button
            btn = new Button();
            btn.Content = "Do-nothing button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);
        }
    }
}
