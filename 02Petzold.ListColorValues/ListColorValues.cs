using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _02Petzold.ListColorValues
{
    class ListColorValues : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ListColorValues());
        }
        public ListColorValues()
        {
            Title = "List Color Values";
            // Creation ListBox object as window content
            ListBox lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            // Filling ListBox by Color objects
            PropertyInfo[] props = typeof (Colors).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                lstbox.Items.Add(prop.GetValue(null, null));
            }
        }

        private void ListBoxOnSelectionChanged(object sender,
            SelectionChangedEventArgs args)
        {
            ListBox lstbox = sender as ListBox;
            if(lstbox.SelectedIndex != -1)
            {
                Color clr = (Color)lstbox.SelectedItem;
                Background = new SolidColorBrush(clr);
            }
        }
    }
}
