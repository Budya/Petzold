using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using _04Petzold.ListNamedBrushes;

namespace _09Petzold.ListColorsEvenElegantlier
{
    class ListColorsEvenElegantlier : Window
    {
        [STAThread]
        public static void Main()
        {
            Application App = new Application();
            App.Run(new ListColorsEvenElegantlier());
        }
        public ListColorsEvenElegantlier()
        {
            Title = "List Colors Even Elegantlier";

            // Create template DataTemplate for optionability
            DataTemplate  template = new DataTemplate(typeof(NamedBrush));

            // Creation FrameworkElementFactory for StackPanel
            FrameworkElementFactory factoryStack = 
                new FrameworkElementFactory(typeof(StackPanel));
            factoryStack.SetValue(StackPanel.OrientationProperty, 
                Orientation.Horizontal);

            // Set an object as root of visual tree DataTemplate
            template.VisualTree = factoryStack;

            // Creation FreameworkElemetnFactory object for Rectangle
            FrameworkElementFactory factoryRectangle = 
                new FrameworkElementFactory(typeof(Rectangle));
            factoryRectangle.SetValue(Rectangle.WidthProperty, 16.0);
            factoryRectangle.SetValue(Rectangle.HeightProperty, 16.0);
            factoryRectangle.SetValue(Rectangle.MarginProperty, new Thickness(2));
            factoryRectangle.SetValue(Rectangle.StrokeProperty, 
                SystemColors.WindowTextBrush);
            factoryRectangle.SetBinding(Rectangle.FillProperty, 
                new Binding("Brush"));

            // Connecting to StackPanel
            factoryStack.AppendChild(factoryRectangle);

            // Creation FrameworkElementFactory for TextBlock
            FrameworkElementFactory factoryTextBlock = 
                new FrameworkElementFactory(typeof(TextBlock));
            factoryTextBlock.SetValue(TextBlock.VerticalAlignmentProperty, 
                VerticalAlignment.Center);
            factoryTextBlock.SetValue(TextBlock.TextProperty,
                new Binding("Name"));

            // Connecting to StackPanel
            factoryStack.AppendChild(factoryTextBlock);

            // Creation ListBox object as window content
            ListBox lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            Content = lstbox;

            // Setting ItemTemplate property a already created Template before
            lstbox.ItemTemplate = template;

            // Setting ItemSource property an array of NamedBrush objects
            lstbox.ItemsSource = NamedBrush.All;

            // Bind SelectedValue to window Background property
            lstbox.SelectedValuePath = "Brush";
            lstbox.SetBinding(ListBox.SelectedValueProperty, "Background");
            lstbox.DataContext = this;

        }
    }
}
