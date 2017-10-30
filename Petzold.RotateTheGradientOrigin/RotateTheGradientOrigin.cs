using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Petzold.RotateTheGradientOrigin
{
    class RotateTheGradientOrigin : Window
    {
        RadialGradientBrush brush;
        GradientBrush grBrush;
        double angle;

        [STAThread]
        public static void Main(string[] args)
        {
            Application app =  new Application();
            app.Run(new RotateTheGradientOrigin());
        }

        public RotateTheGradientOrigin()
        {
            Title = "Rotate The Gradient Origin";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = 384;
            Height = 384;

            brush = new RadialGradientBrush(Colors.White, Colors.Blue);
            brush.Center = brush.GradientOrigin = new Point(0.5, 0.5);
            brush.RadiusX = brush.RadiusY = 0.10;
            brush.SpreadMethod = GradientSpreadMethod.Repeat;
            Background = brush;

            grBrush = new LinearGradientBrush(Colors.Red, Colors.Blue,
                new Point(0, 0), new Point(1, 1));
            BorderThickness = new Thickness(50);
            BorderBrush = grBrush;

            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromMilliseconds(100);
            tmr.Tick += TimerOnTick;
            tmr.Start(); 
        }

        void TimerOnTick (object sender, EventArgs args)
        {
            
            Point pt = new Point(0.5 + 0.05*Math.Cos(angle),
                                 0.5 + 0.05*Math.Sin(angle));
            brush.GradientOrigin = pt;
            angle += Math.PI / 6;
        }

    

    }
}
