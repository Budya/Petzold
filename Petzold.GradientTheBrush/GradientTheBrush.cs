using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

    namespace Petzold.GradienTheBrush
    {
        public class GradientTheBrush : Window
        {
            [STAThread]
            public static void Main(string[] args)
            {
                Application app = new Application();
                app.Run(new GradientTheBrush());
            }

            public GradientTheBrush()
            {
                Title = "Gradient The Brush";
                LinearGradientBrush brush =
//                    new LinearGradientBrush(Colors.Red, Colors.Blue,
//                                  new Point(0, 0), new Point(1, 1));
                new LinearGradientBrush(Colors.Red, Colors.Blue,
                    new Point(0, 0), new Point(0.25, 0.25));
                brush.SpreadMethod = GradientSpreadMethod.Reflect;

                Background = brush;
            }


        }
    }