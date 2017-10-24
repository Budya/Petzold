using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Petzold.ShowMyFace
{
    class ShowMyFace : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ShowMyFace());
        }

        public ShowMyFace ()
        {
            Title = "Show My Face";
            Uri uri = new Uri("http://www.charlespetzold.com/PetzoldTattoo.jpg");
            BitmapImage bitmap = new BitmapImage(uri);
            Image img = new Image();
            img.Source = bitmap;
            img.Stretch = Stretch.Uniform;
            img.HorizontalAlignment = HorizontalAlignment.Right;
            img.VerticalAlignment = VerticalAlignment.Top;
            img.Margin = new Thickness(10);
            img.Opacity = 1; // 
            Background = new LinearGradientBrush(Colors.Red, Colors.Blue,
                                                 new Point(0, 0), new Point(1, 1));
            img.LayoutTransform = new RotateTransform(45); // Поворот изображения
            Content = img;
        }
    }
}
