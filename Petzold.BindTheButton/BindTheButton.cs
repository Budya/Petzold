using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.BindTheButton
{
    class BindTheButton : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new BindTheButton());
        }

        public BindTheButton()
        {
            Title = "Bind The Button";

            ToggleButton btn = new ToggleButton();
            btn.Content = "Make_Topmost";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.SetBinding(ToggleButton.IsCheckedProperty, "Topmost");
            btn.DataContext = this;
            Content = btn;

            ToolTip tip = new ToolTip();
            tip.Content = "Toggle the button to make the window topmost" + 
                " on the desktop ";
            btn.ToolTip = tip;
        }
    }
}
