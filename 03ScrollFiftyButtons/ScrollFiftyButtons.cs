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

namespace _03ScrollFiftyButtons
{
    class ScrollFiftyButtons : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ScrollFiftyButtons());
        }

        public ScrollFiftyButtons()
        {
            Title = "Scroll Fifty Buttons";
            SizeToContent = SizeToContent.Width;
            
            //ScrollViewer scroll = new ScrollViewer();
            //Content = scroll;
            
            Viewbox view = new Viewbox();
            Content = view;
            StackPanel stack = new StackPanel();
            
            stack.AddHandler(Button.ClickEvent,
                new RoutedEventHandler(ButtonOnClick));
            stack.Margin = new Thickness(5);
            view.Child = stack;
            //scroll.Content = stack;

            for (int i = 0; i<50; i++)
            {
                Button btn = new Button();
                btn.Name = "Button" + (i + 1);
                btn.Content = btn.Name + " says 'Click me'";
                btn.Margin = new Thickness(5);
                stack.Children.Add(btn);
            }

        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            if (btn != null)
                MessageBox.Show(btn.Name + " has been clicked",
                                "Button Click");
        }
    }
}
