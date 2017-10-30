using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Petzold.ImageTheButton
{
    class ImageTheButton : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ImageTheButton());

        }

        public ImageTheButton()
        {
            Title = "Image The Button";
            Uri uri = new Uri("D://KRIK1.jpg", UriKind.Absolute);
            BitmapImage bitmap = new BitmapImage(uri);
            Image img = new Image();
            img.Source = bitmap;
                                                     
            img.Stretch = Stretch.None;

            Button btn = new Button();
            btn.Content = img;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            

            Content = btn;

        }
    }
}
