using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Petzold.ShapeAnEllipse
{
    class ShapeAnEllipse : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ShapeAnEllipse());
        }

        public ShapeAnEllipse()
        {
            Title = "Shape an Allipse";
            Ellipse ellips = new Ellipse();
            ellips.Fill = Brushes.AliceBlue;
            ellips.StrokeThickness = 24;
            ellips.Stroke = new LinearGradientBrush(Colors.CadetBlue,
                            Colors.Chocolate, new Point(1, 0), new Point(0, 1));
            Content = ellips;
        }
        
        
    }
}
