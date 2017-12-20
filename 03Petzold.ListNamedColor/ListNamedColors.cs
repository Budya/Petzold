﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace _03Petzold.ListNamedColor
{
    class ListNamedColors : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ListNamedColors());
        }
        public ListNamedColors()
        {
            Title = "List Named Colors";

            // Creation ListBox as window content
            ListBox lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            // Filling list & setting properties
            lstbox.ItemsSource = NamedColor.All;
            lstbox.DisplayMemberPath = "Name";
            lstbox.SelectedValuePath = "Color";
        }

        private void ListBoxOnSelectionChanged(object sender,
            SelectionChangedEventArgs args)
        {
            ListBox lstbox = sender as ListBox;
            if (lstbox.SelectedValue != null)
            {
                Color clr = (Color)lstbox.SelectedValue;
                Background = new SolidColorBrush(clr);
            }
        }
    }
}