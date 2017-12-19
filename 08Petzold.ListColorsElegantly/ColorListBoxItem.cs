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
    class ColorListBoxItem : ListBoxItem
    {
        private string str;
        private Rectangle rect;
        private TextBlock text;
        public ColorListBoxItem()
        {
            // Creation panel StackPanel for display Rectangle & TextBlock
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;

            // Creation Rectangle object for display a color sample
            rect = new Rectangle();
            rect.Width = 16;
            rect.Height = 16;
            rect.Margin = new Thickness(2);
            rect.Stroke = SystemColors.WindowTextBrush;
            stack.Children.Add(rect);

            // Creation TextBlock object for display color name
            text = new TextBlock();
            text.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(text);
        }

        // Property Text become a property of TextBlock object
        public string Text
        {
            set 
            { 
                str = value;
                string strSpaced = str[0].ToString();
                for(int i = 1; i < str.Length; i++)
                {
                    strSpaced += (char.IsUpper(str[i]) ? " " : "") +
                        str[i].ToString();
                }

                text.Text = strSpaced;
            }
            get { return str; }
        }

        // Property Color become a property Brush of Rectangle
        public Color Color
        {
            set { rect.Fill = new SolidColorBrush(value); }
            get 
            {
                SolidColorBrush brush = rect.Fill as SolidColorBrush;
                return brush == null ? Colors.Transparent : brush.Color;
            }
        }

        // Selected item marked bold font
        protected override void OnSelected(RoutedEventArgs args)
        {
            base.OnSelected(args);
            text.FontWeight = FontWeights.Bold;
        }

        protected override void OnUnselected(RoutedEventArgs args)
        {
            base.OnUnselected(args);
            text.FontWeight = FontWeights.Regular;
        }

        // Override ToString method
        public override string ToString()
        {
            return str;
        }
    }
}
