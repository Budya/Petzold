using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _03Petzold.CheckTheColor
{
    class CheckTheColor : Window
    {
        private TextBlock text;
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new CheckTheColor());
        }
        public CheckTheColor()
        {
            Title = "Check the Color";
            
            // Creation DockPanel object
            DockPanel dock = new DockPanel();
            Content = dock;

            // Creation menu, in top of the window
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            // Creation TextBlock object for other free space
            text = new TextBlock();
            text.Text = Title;
            text.TextAlignment = TextAlignment.Center;
            text.FontSize = 32;
            text.Background = SystemColors.WindowBrush;
            text.Foreground = SystemColors.WindowTextBrush;
            dock.Children.Add(text);

            // Creation menu commands
            MenuItem itemColor = new MenuItem();
            itemColor.Header = "_Color";
            menu.Items.Add(itemColor);
            
            MenuItem itemForeground = new MenuItem();
            itemForeground.Header = "_Foregroung";
            itemForeground.SubmenuOpened += ForegrounOnOpened;
            itemColor.Items.Add(itemForeground);
            FillWithColors(itemForeground, ForegrounOnClick);
            
            MenuItem itemBackgroud  = new MenuItem();
            itemBackgroud.Header = "_Background";
            itemBackgroud.SubmenuOpened += BackgroundOnOpened;
            itemColor.Items.Add(itemBackgroud);
            FillWithColors(itemBackgroud, BackgroundOnClick);
        }

        void FillWithColors(MenuItem itemParent, RoutedEventHandler handler)
        {
            foreach (PropertyInfo prop in typeof(Colors).GetProperties())
            {
                Color clr = (Color) prop.GetValue(null, null);
                int iCount = 0;
                iCount += clr.R == 0 || clr.R == 255 ? 1 : 0;
                iCount += clr.G == 0 || clr.G == 255 ? 1 : 0;
                iCount += clr.B == 0 || clr.B == 255 ? 1 : 0;

                if (clr.A == 255 && iCount >1)
                {
                    MenuItem item = new MenuItem();
                    item.Header = "_" + prop.Name;
                    // ADDON
                    Rectangle rect = new Rectangle();
                    rect.Fill = new SolidColorBrush(clr);
                    rect.Width = 2*(rect.Height = 12);
                    item.Icon = rect;
                    //
                    item.Tag = clr;
                    item.Click += handler;
                    itemParent.Items.Add(item);
                }
            }
        }
        void ForegrounOnOpened(object sender, RoutedEventArgs args)
        {
            MenuItem itemParent = sender as MenuItem;
            foreach (MenuItem item in itemParent.Items)
            {
                item.IsChecked =
                    ((text.Foreground as SolidColorBrush).Color ==
                     (Color)item.Tag);
            }
        }
        void BackgroundOnOpened(object sender, RoutedEventArgs e)
        {
            MenuItem itemParent = sender as MenuItem;
            foreach (MenuItem item in itemParent.Items)
            {
                item.IsChecked =
                    ((text.Background as SolidColorBrush).Color ==
                     (Color)item.Tag);
            }
        }
        void ForegrounOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            Color clr = (Color)item.Tag;
            text.Foreground = new SolidColorBrush(clr);
        }
        void BackgroundOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            Color clr = (Color)item.Tag;
            text.Background = new SolidColorBrush(clr);
        }
    }
}
