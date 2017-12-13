using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace _01Petzold.DuplicateUniformGrid
{
    class DuplicateUniformGrid : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new DuplicateUniformGrid());
        }
        public DuplicateUniformGrid()
        {
            Title = "Duplicate Uniform Grid";
            
            //ADD
            //SizeToContent = SizeToContent.WidthAndHeight;
            
            // Creation UniformGridAlmost object as window content
            UniformGridAlmost unigtid = new UniformGridAlmost();
            
            //ADD
            //unigtid.HorizontalAlignment = HorizontalAlignment.Center;
            //unigtid.VerticalAlignment = VerticalAlignment.Center;
            
            unigtid.Columns = 5;
            Content = unigtid;

            // Filling UniformGridAlmost by buttons with random size
            Random rand = new Random();
            for (int index = 0; index < 48; index++)
            {
                Button btn = new Button();
                btn.Name = "Button" + index;
                btn.Content = btn.Name;

                //ADD
                //btn.HorizontalAlignment = HorizontalAlignment.Center;
                //btn.VerticalAlignment = VerticalAlignment.Center;

                btn.FontSize += rand.Next(10);
                unigtid.Children.Add(btn);
            }
            AddHandler(Button.ClickEvent, 
                new RoutedEventHandler(ButtonOnClick));
        }

        private void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = args.Source as Button;
            MessageBox.Show(btn.Name + " has been clicked", Title);
        }
    }
}
