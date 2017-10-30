using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _01Petzold.DockAroundTheBlock
{
    class DockAroundTheBlock : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new DockAroundTheBlock());
        }

        public DockAroundTheBlock()
        {
            Title = "Dock Around The Block";

            DockPanel dock = new DockPanel();
            Content = dock;
            //dock.LastChildFill = false;

            for (int i = 0; i < 17; i++)
            {
                Button btn = new Button();
                btn.Content = "Button No. " + (i + 1);
               // btn.HorizontalAlignment = HorizontalAlignment.Center;
                dock.Children.Add(btn);
                btn.SetValue(DockPanel.DockProperty, (Dock) (i % 4));
            }
        }
    

    }
}
