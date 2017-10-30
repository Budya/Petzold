using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _04Petzold.DesignAButton
{
    class DesignAButton : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new DesignAButton());
        }

        public DesignAButton()
        {
            Title = "Design A Button";

            // Создание объекта Button как содержимого окна
            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Click += ButtonOnClick;
            Content = btn;

            // Сздание объекста StackPanel как содержимого Button
            StackPanel stack = new StackPanel();
            btn.Content = stack;

            // Добавление объекта Polyline к StackPanel
            stack.Children.Add(ZigZag(10));

            // Добавление объекта Image
            Uri uri = new Uri("/book.ico",UriKind.Relative);
            BitmapImage bitmap = new BitmapImage(uri);
            Image img = new Image();
            img.Margin = new Thickness(0,10,0,0);
            img.Source = bitmap;
            img.Stretch = Stretch.None;
            stack.Children.Add(img);

            // Добавление объекта Label 
            Label lbl = new Label();
            lbl.Content = "_Read books";
            lbl.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Children.Add(lbl);

            // Добавление объекта Polyline
            stack.Children.Add(ZigZag(0));



        }

        Polyline ZigZag(int offset)
        {
            Polyline poly = new Polyline();
            poly.Stroke = SystemColors.ControlTextBrush;
            poly.Points = new PointCollection();
            for (int i = 0; i < 100; i+=10)
            {
                poly.Points.Add(new Point(i, (i + offset)% 20));
                

            }
            return poly;
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The button hax been clicked", Title);
        }
    }
}
