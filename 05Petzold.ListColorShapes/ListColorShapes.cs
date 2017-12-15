using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _05Petzold.ListColorShapes
{
    class ListColorShapes : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ListColorShapes());
        }
        public ListColorShapes()
        {
            Title = "List Color Shapes";
            // Creation ListBox object as window content
            ListBox lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            //lstbox.SelectionChanged += ListBoxOnSelectionChanged;

            lstbox.SelectedValuePath = "Fill";
            lstbox.SetBinding(ListBox.SelectedValueProperty, "Background");
            lstbox.DataContext = this;

            Content = lstbox;

            // Filling ListBox by Ellipse objects
            PropertyInfo[] props = typeof (Brushes).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                Ellipse ellip = new Ellipse();
                ellip.Width = 100;
                ellip.Height = 25;
                ellip.Margin = new Thickness(10,5,0,5);
                ellip.Fill = prop.GetValue(null, null) as Brush;
                lstbox.Items.Add(ellip);
            }
        }

        private void ListBoxOnSelectionChanged(object sender,
            SelectionChangedEventArgs args)
        {
            ListBox lstbox = sender as ListBox;
            if (lstbox.SelectedIndex != -1)
                Background = (lstbox.SelectedItem as Shape).Fill;
        }
    }
}
