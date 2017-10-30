using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.DisplaySomeText
{
    public class DisplaySomeText : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new DisplaySomeText());

        }

        public DisplaySomeText()
        {
            Title = "Display Some Text";
            Content = DateTime.Now;
            //Content = "Conten can be simple text !";
            FontFamily = new FontFamily("Times New Roman");
            FontStyle = FontStyles.Oblique;
            FontWeight = FontWeights.Bold;
            FontSize = 48;

            Brush brush = new LinearGradientBrush(Colors.Black, Colors.White,
            new Point(0, 0), new Point(1, 1));
            Background = brush;
            Foreground = brush;

            //SizeToContent = SizeToContent.WidthAndHeight;

            BorderBrush = Brushes.SaddleBrown;
            BorderThickness = new Thickness(25);

        }
    }
}
