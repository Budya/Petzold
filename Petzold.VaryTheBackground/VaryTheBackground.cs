using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.VaryTheBackground
{
    public class VaryTheBackground : Window
    {
        SolidColorBrush brush = Brushes.Black.Clone();  //new SolidColorBrush(Colors.Black);

        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new VaryTheBackground());
        }

        public VaryTheBackground()
        {
            Title = "Vary the Background";
            Width = 384;
            Height = 384;
            Background = brush;
        }

        protected override void OnMouseMove(MouseEventArgs args)
        {
            //base.OnMouseMove(args);

            double width = ActualWidth
                 - 2*SystemParameters.ResizeFrameVerticalBorderWidth;
            double height = ActualHeight
                - 2*SystemParameters.ResizeFrameHorizontalBorderHeight
                - SystemParameters.CaptionHeight;

            Point ptMouse = args.GetPosition(this);
            Point ptCenter = new Point(width / 2, height / 2);
            Vector vectMouse = ptMouse - ptCenter;
            double angle = Math.Atan2(vectMouse.Y, vectMouse.X);
            Vector vectEllipse = new Vector(width / 2 * Math.Cos(angle),
                                          height / 2 * Math.Sin(angle));

            Byte byLevel = (byte) (255*Math.Min(1, vectMouse.Length/
                                                   vectEllipse.Length));
            Color clr = brush.Color;
            clr.R = clr.G = clr.B = byLevel;
            brush.Color = clr;

        }
    }
}