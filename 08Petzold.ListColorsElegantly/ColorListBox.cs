using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _08Petzold.ListColorsElegantly
{
    class ColorListBox : ListBox
    {
        public ColorListBox()
        {
            PropertyInfo[] props = typeof (Colors).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                ColorListBoxItem item = new ColorListBoxItem();
                item.Text = prop.Name;
                item.Color = (Color)prop.GetValue(null, null);
                Items.Add(item);
            }
            SelectedValuePath = "Color";
        }

        public Color SelectedColor
        {
            set { SelectedValue = value; }
            get
            {
                if (SelectedValue != null)
                {
                    return (Color) SelectedValue;
                }
                else
                {
                    return SystemColors.WindowColor;
                }
            }
        }
    }
}
