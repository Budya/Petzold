using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace Petzold.ClickTheButtons
{
    class ClickTheButton : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ClickTheButton());
        }

        public ClickTheButton()
        {
            Title = "Click the Button";

            Button btn = new Button();
            btn.Content = "_Click me, please!";
            btn.Click += ButtonOnClick;
            //btn.Margin = new Thickness(96);
            //btn.HorizontalContentAlignment = HorizontalAlignment.Left;
            //btn.VerticalContentAlignment = VerticalAlignment.Bottom;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            //btn.Padding = new Thickness(96);
            Content = btn;
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The button has been clicked and all is well", Title);
        }
    }
}
