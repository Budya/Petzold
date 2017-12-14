using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _03Petzold.DiagonalizeThebuttons
{
    class DiagonalizeTheButtons : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new DiagonalizeTheButtons());
        }
        public DiagonalizeTheButtons()
        {
            Title = "Diagonalize The Buttons";
            DiagonalPanel pnl = new DiagonalPanel();
            Content = pnl;
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                Button btn = new Button();
                btn.Content = "Button Number " + (i + 1);
                btn.FontSize += rand.Next(20);
                pnl.Add(btn);
            }
           
        }
    }
}
