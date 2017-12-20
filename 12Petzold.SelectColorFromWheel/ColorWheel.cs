using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using _04Petzold.CircleTheButtons;

namespace _12Petzold.SelectColorFromWheel
{
    class ColorWheel : ListBox
    {
        public ColorWheel()
        {
            // Defining template of ItemsPanel
            FrameworkElementFactory factoryRadialPanel = 
                new FrameworkElementFactory(typeof(RadialPanel));
            ItemsPanel = new ItemsPanelTemplate(factoryRadialPanel);
            
            // Creation DataTemplate object for options
            DataTemplate template = new DataTemplate(typeof(Brush));
            ItemTemplate = template;
            
            // Creation FrameworkElementFactory based on Rectangle
            FrameworkElementFactory elRectangle = 
                new FrameworkElementFactory(typeof(Rectangle));
            elRectangle.SetValue(Rectangle.WidthProperty, 4.0);
            elRectangle.SetValue(Rectangle.HeightProperty, 12.0);
            elRectangle.SetValue(Rectangle.MarginProperty, 
                new Thickness(1,8,1,8));
            elRectangle.SetBinding(Rectangle.FillProperty, new Binding(""));

            // Setting elRectangle as VisualTree node
            template.VisualTree = elRectangle;

            // Filling list ListBox
            PropertyInfo[] prps = typeof (Brushes).GetProperties();
            foreach (PropertyInfo prop in prps)
            {
                Items.Add((Brush) prop.GetValue(null, null));
            }
        }
    }
}
