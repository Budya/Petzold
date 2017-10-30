using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

namespace Petzold.StackTenButtons
{
    class StackTenButtons : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new StackTenButtons());
        }

        public StackTenButtons()
        {
            Title ="Stack Ten Buttons";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            StackPanel stack = new StackPanel();
            stack.Margin = new Thickness(5);
            //stack.Background = Brushes.Aquamarine;
            stack.HorizontalAlignment = HorizontalAlignment.Center;
            //stack.Orientation = Orientation.Horizontal;
            Content = stack;
            Random rand = new Random();

            for (int i = 0; i<10; i++)
            {
                Button btn = new Button();
                btn.Name = ((char) ('A' + i)).ToString();
                btn.FontSize += rand.Next(10);
                btn.HorizontalAlignment = HorizontalAlignment.Stretch;
                btn.Margin = new Thickness(5);
                btn.Content = "Button " + btn.Name + " says 'Klick me'";
                //btn.Click += ButtonOnClick;
                stack.Children.Add(btn);
            }
            stack.AddHandler(Button.ClickEvent, new RoutedEventHandler(
                ButtonOnClick));

        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            MessageBox.Show("Button " + btn.Name + " has been clicked",
                                                       "Button Click");
        }
    }
}
