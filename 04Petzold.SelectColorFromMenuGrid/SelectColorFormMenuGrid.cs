using _11Petzold.SelectColorFromGrid;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;


namespace _04Petzold.SelectColorFromMenuGrid
{
    class SelectColorFormMenuGrid : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new SelectColorFormMenuGrid());
        }
        public SelectColorFormMenuGrid()
        {
            Title = "Select Color From Menu Grid";

            // Creation DockPanel object
            DockPanel dock = new DockPanel();
            Content = dock;

            // Creation menu, in top of window
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            // Creation TextBlock for other free space
            TextBlock text = new TextBlock();
            text.Text = Title;
            text.FontSize = 32;
            text.TextAlignment = TextAlignment.Center;
            dock.Children.Add(text);

            // Filling comands in menu
            MenuItem itemColor = new MenuItem();
            itemColor.Header = "_Color";
            menu.Items.Add(itemColor);
            
            MenuItem itemForeground = new MenuItem();
            itemForeground.Header = "_Foreground";
            
            itemColor.Items.Add(itemForeground);

            // Creation ColorGridBox & binding it
            // to Foreground property of Window
            ColorGridBox clrbox = new ColorGridBox();
            clrbox.SetBinding(ColorGridBox.SelectedValueProperty, "Foreground");
            clrbox.DataContext = this;
            itemForeground.Items.Add(clrbox);
            MenuItem itemBackground = new MenuItem();
            itemBackground.Header = "_Background";
            itemColor.Items.Add(itemBackground);

            // Creation ColorGridBox & binding it 
            // to Background property of window
            clrbox = new ColorGridBox();
            clrbox.SetBinding(ColorGridBox.SelectedValueProperty, "Background");
            clrbox.DataContext = this;
            itemBackground.Items.Add(clrbox);
        }
        
        
    }
}
