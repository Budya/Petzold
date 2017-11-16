using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using _01Petzold.BetterEllipse;

namespace _01Petzold.EllipseWithChild
{
    class EncloseElementInEllipse : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new EncloseElementInEllipse());
        }

        public EncloseElementInEllipse()
        {
            Title = "Enclose Element in Ellipse";
            EllipseWithChild elips = new EllipseWithChild();
            elips.Fill = Brushes.ForestGreen;
            elips.Stroke = new Pen(Brushes.Magenta, 48);
            Content = elips;
            TextBlock text = new TextBlock();
            text.FontFamily = new FontFamily("Times New Roman");
            text.FontSize = 48;
            text.Text = "Text inside ellipse";

            elips.Child = text;

        }
    }
}
