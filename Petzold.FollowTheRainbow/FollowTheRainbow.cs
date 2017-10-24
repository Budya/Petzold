using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.FollowTheRainbow
{
    class FollowTheRainbow : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new FollowTheRainbow());
        }

        public FollowTheRainbow()
        {
            Title = "Follow The Rainbow";
            //LinearGradientBrush brush = new LinearGradientBrush();
            //brush.StartPoint = new Point(0, 0);
            //brush.EndPoint = new Point(1, 0);

            RadialGradientBrush brush = new RadialGradientBrush();
            brush.GradientOrigin = new Point(0.75, 0.75);

            Background = brush;

            brush.GradientStops.Add(new GradientStop(Colors.Red, 0));
            brush.GradientStops.Add(new GradientStop(Colors.Orange, 0.17));
            brush.GradientStops.Add(new GradientStop(Colors.Yellow, 0.33));
            brush.GradientStops.Add(new GradientStop(Colors.Green, 0.5));
            brush.GradientStops.Add(new GradientStop(Colors.Blue, 0.67));
            brush.GradientStops.Add(new GradientStop(Colors.Indigo, 0.84));
            brush.GradientStops.Add(new GradientStop(Colors.Violet, 1));
        }

    }
}