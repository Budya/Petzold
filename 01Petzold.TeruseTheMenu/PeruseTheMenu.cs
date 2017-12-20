using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace _01Petzold.PeruseTheMenu
{
    class PeruseTheMenu : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new PeruseTheMenu());
        }
        public PeruseTheMenu()
        {
            Title = "Peruse the Menu";

            // Creatin DockPanel object
            DockPanel dock = new DockPanel();
            Content = dock;

            // Creation menu, docked in Left Top side of window
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            // Creation TextBlock object for all other free workspace
            TextBlock text = new TextBlock();
            text.Text = Title;

            text.FontSize = 32; // 24 points
            text.TextAlignment = TextAlignment.Center;
            dock.Children.Add(text);

            // Creation menu File
            MenuItem itemFile = new MenuItem();
            itemFile.Header = "_File";
            menu.Items.Add(itemFile);

            MenuItem itemNew = new MenuItem();
            itemNew.Header = "_New";
            itemNew.Click += UnimplementOnClick;
            itemFile.Items.Add(itemNew);
            
            MenuItem itemOpen = new MenuItem();
            itemOpen.Header = "_Open";
            itemOpen.Click += UnimplementOnClick;
            itemFile.Items.Add(itemOpen);

            MenuItem itemSave = new MenuItem();
            itemSave.Header = "_Save";
            itemSave.Click += UnimplementOnClick;
            itemFile.Items.Add(itemSave);

            itemFile.Items.Add(new Separator());
            
            MenuItem itemExit = new MenuItem();
            itemExit.Header = "E_xit";
            itemExit.Click += ExitOnClick;
            itemFile.Items.Add(itemExit);

            // Creation menu Window
            MenuItem itemWindow = new MenuItem();
            itemWindow.Header = "_Window";
            menu.Items.Add(itemWindow);

            MenuItem itemTaskbar = new MenuItem();
            itemTaskbar.Header = "_Show in Taskbar";
            itemTaskbar.IsCheckable = true;
            //itemTaskbar.IsChecked = ShowInTaskbar;
            //itemTaskbar.Click += TaskbarOnClick;

            itemTaskbar.SetBinding(MenuItem.IsCheckedProperty, "ShowInTaskbar");
            itemTaskbar.DataContext = this;
            
            itemWindow.Items.Add(itemTaskbar);

            MenuItem itemSize = new MenuItem();
            itemSize.Header = "Size to _Content";
            itemSize.IsCheckable = true;
            itemSize.IsChecked = SizeToContent == SizeToContent.WidthAndHeight;
            itemSize.Checked += SizeOnCheck;
            itemSize.Unchecked += SizeOnCheck;
            itemWindow.Items.Add(itemSize);

            MenuItem itemResize = new MenuItem();
            itemResize.Header = "_Resizable";
            itemResize.IsCheckable = true;
            itemResize.IsChecked = ResizeMode == ResizeMode.CanResize;
            itemResize.Click += ResizeOnClick;
            itemWindow.Items.Add(itemResize);

            MenuItem itemTopmost = new MenuItem();
            itemTopmost.Header = "_Topmost";
            itemTopmost.IsCheckable = true;
            itemTopmost.IsChecked = Topmost;
            itemTopmost.Checked += TopmostOnCheck;
            itemTopmost.Unchecked += TopmostOnCheck;
            itemWindow.Items.Add(itemTopmost);
        }

        private void UnimplementOnClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            string strItem = item.Header.ToString().Replace("_", "");
            MessageBox.Show("The " + strItem +
                            " option has not yet been implemented", Title);
        }

        private void ExitOnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TaskbarOnClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            ShowInTaskbar = item.IsChecked;
        }

        private void SizeOnCheck(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            SizeToContent = item.IsChecked
                                ? SizeToContent.WidthAndHeight
                                : SizeToContent.Manual;
        }

        private void ResizeOnClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            ResizeMode = item.IsChecked
                             ? ResizeMode.CanResize
                             : ResizeMode.NoResize;
        }

        private void TopmostOnCheck(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            Topmost = item.IsChecked;
        }
    }
}
