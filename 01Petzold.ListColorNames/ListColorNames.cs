﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace _01Petzold.ListColorNames
{
    class ListColorNames : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ListColorNames());
        }
        public ListColorNames()
        {
            Title = "List Color Names";
            
            // Creation ListBox object
            ListBox lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            // Filling in the list by mons names
            PropertyInfo[] props = typeof (Colors).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                lstbox.Items.Add(prop.Name);
            }
        }

        private void ListBoxOnSelectionChanged(object sender,
            SelectionChangedEventArgs args)
        {
            ListBox lstbox = sender as ListBox;
            string str = lstbox.SelectedItem as string;
            if (str != null)
            {
                Color clr =
                (Color)typeof (Colors).GetProperty(str).GetValue(null, null);
                Background = new SolidColorBrush(clr);
            }

        }
    }
}
