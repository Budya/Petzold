using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace _02Petzold.MedievalButton
{
    class GetMedieval : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new GetMedieval());
        }
        public GetMedieval()
        {
            Title = "Get Medieval Button";

            MedievalButton btn = new MedievalButton();
            btn.Text = "Click this button";
            btn.FontSize = 24;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Padding = new Thickness(5,20,5,20);
            btn.Knock += ButtonOnKnock;
            Content = btn;
        }

        private void ButtonOnKnock(object sender, RoutedEventArgs args)
        {
            MedievalButton btn = args.Source as MedievalButton;
            MessageBox.Show("The button labeled \"" + btn.Text +
                            "\" has been knocked.", Title);
        }
    }
}
