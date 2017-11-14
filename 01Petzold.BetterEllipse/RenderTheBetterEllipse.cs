using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace _01Petzold.BetterEllipse
{
    class RenderTheBetterEllipse : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new RenderTheBetterEllipse());
        }
        public RenderTheBetterEllipse()
        {
            Title = "Render The Better Ellipse";

            BetterEllipse elips = new BetterEllipse();
            elips.Fill = Brushes.AliceBlue;
            elips.Stroke = new Pen(
                new LinearGradientBrush(Colors.CadetBlue, Colors.Chocolate, 
                    new Point(1, 0), new Point(0, 1)), 24);
            
            Content = elips;

        }
    }
}
