using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace _02Petzold.CheckTheWindowStyle
{
    class CheckTheWindowStyle : Window
    {
        private MenuItem itemChecked;
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new CheckTheWindowStyle());
        }
        public CheckTheWindowStyle()
        {
            Title = "Check The Window Style";

            // Creation DockPanel object
            DockPanel dock = new DockPanel();
            Content = dock;

            // Creation menu, in top of window
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            // Creation TextBlock, for other free space
            TextBlock text = new TextBlock();
            text.Text = Title;
            text.FontSize = 32;
            text.TextAlignment = TextAlignment.Center;
            dock.Children.Add(text);

            // Creation MenuItem objects for Window changing
            MenuItem itemStyle = new MenuItem();
            itemStyle.Header = "_Style";
            menu.Items.Add(itemStyle);

           
        }

    }
}
