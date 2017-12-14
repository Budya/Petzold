using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _04Petzold.CircleTheButtons
{
    class CircleTheButtons : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new CircleTheButtons());
        }
        public CircleTheButtons()
        {
            Title = "Circle The Buttons";
            RadialPanel pnl = new RadialPanel();
            pnl.Orientation = RadialPanelOrientation.ByWidth;
            pnl.ShowPieLines = true;
            Content = pnl;
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                Button btn = new Button();
                btn.Content = "Button Number " + (i + 1);
                btn.FontSize += rand.Next(10);
                pnl.Children.Add(btn);
            }
        }
    }
}
