using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.AdjustTheGradietn
{
    class AdjustTheGradient : Window
    {
        LinearGradientBrush brush;

        [STAThread]
        public static void Main(string[] args)
        {
            Application app  = new Application();
            app.Run(new AdjustTheGradient());
        }

        public AdjustTheGradient()
        {
            Title = "Adjust The Gradient";
            SizeChanged += WindowOnSizeChanged;

            brush = new LinearGradientBrush(Colors.Red, Colors.Blue, 0);
            brush.MappingMode = BrushMappingMode.Absolute;
            Background = brush;

        }

        void WindowOnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            double width = ActualWidth 
                - 2 * SystemParameters.ResizeFrameVerticalBorderWidth;
            double height = ActualHeight
                - 2*SystemParameters.ResizeFrameHorizontalBorderHeight
                - SystemParameters.CaptionHeight;

            Point ptCenter = new Point(width / 2, height / 2);
            Vector vectDiag = new Vector(width, -height);
            Vector vectPerp = new Vector(vectDiag.Y, -vectDiag.X);

            vectPerp.Normalize();
            vectPerp *= width * height / vectDiag.Length;
            brush.StartPoint = ptCenter + vectPerp;
            brush.EndPoint = ptCenter - vectPerp;

        }
        
    }
}