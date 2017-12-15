using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _06Petzold.ListColoredLables
{
    class ListColoredLables : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ListColoredLables());
        }
        public ListColoredLables()
        {
            Title = "List Colored Lables";

            // Creation ListBox object
            ListBox lstbox = new ListBox();
            lstbox.Height = 150;
            lstbox.Width = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            // Filling list by Label elements
            PropertyInfo[] props = typeof (Colors).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                Color clr = (Color)prop.GetValue(null, null);
                bool isBlack = .222*clr.R + .707*clr.G +
                               .071*clr.B > 128;
                Label lbl = new Label();
                lbl.Content = prop.Name;
                lbl.Background = new SolidColorBrush(clr);
                lbl.Foreground = isBlack ? Brushes.Black : Brushes.White;
                lbl.Margin = new Thickness(15,0,0,0);
                lbl.Tag = clr;
                lstbox.Items.Add(lbl);
            }

        }

        private void ListBoxOnSelectionChanged(object sender,
            SelectionChangedEventArgs args)
        {
            ListBox lstbox = sender as ListBox;
            Label lbl = lstbox.SelectedItem as Label;
            if(lbl != null)
            {
                Color clr = (Color)lbl.Tag;
                Background = new SolidColorBrush(clr);
            }
        }
    }
}
