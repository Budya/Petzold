using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
//using FilePath = System.IO.Path;

namespace _05Petzold.CutCopyAndPaste
{
    public class CutCopyAndPaste : Window
    {
        private TextBlock text;
        private double iconSize = 15.0;
        protected MenuItem itemCut, itemCopy, itemPaste, itemDelete;
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new CutCopyAndPaste());
        }

        public CutCopyAndPaste()
        {
            Title = "Cut, Copy, And Paste";
            
            //var directory = FilePath.GetDirectoryName(
            //    Assembly.GetExecutingAssembly().Location);

            //Console.WriteLine(directory.ToString());
            

            // Creation DockPanel
            DockPanel dock = new DockPanel();
            Content = dock;

            // Creation menu, in top of wondow
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            // Creation TextBlock for other free space
            text = new TextBlock();
            text.Text = "Sample clipboard text";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.FontSize = 32;
            text.TextWrapping = TextWrapping.Wrap;
            dock.Children.Add(text);

            // Creation menu Edit
            MenuItem itemEdit = new MenuItem();
            itemEdit.Header = "_Edit";
            itemEdit.SubmenuOpened += EditOnOpened;
            menu.Items.Add(itemEdit);

            // Creation commands of Edit menu
            itemCut = new MenuItem();
            itemCut.Header = "Cu_t";
            itemCut.InputGestureText = "Ctrl+X";
            itemCut.Click += CutOnClick;
            Image img = new Image();
            img.Source = new BitmapImage(
            new Uri("pack://application:,,/Images/cutIco.png"));
            Console.WriteLine(img.Source.ToString());
            Console.WriteLine(img.Width.ToString());
            Console.WriteLine(img.Height.ToString());
            img.Width = iconSize;
            img.Height = img.Width;
            itemCut.Icon = img;
            itemCut.VerticalContentAlignment = VerticalAlignment.Center;
            itemEdit.Items.Add(itemCut);

            itemCopy = new MenuItem();
            itemCopy.Header = "_Copy";
            itemCopy.Click += CopyOnClick;
            img = new Image();
            img.Source = new BitmapImage(
            new Uri("pack://application:,,,/Images/copyIco.png"));
            img.Width = iconSize;
            img.Height = img.Width;
            itemCopy.Icon = img;
            itemEdit.Items.Add(itemCopy);

            itemPaste = new MenuItem();
            itemPaste.Header = "_Paste";
            itemPaste.Click += PasteOnClick;
            img = new Image();
            img.Source = new BitmapImage(
            new Uri("pack://application:,,,/Images/pasteIco.png"));
            img.Width = iconSize;
            img.Height = img.Width;
            itemPaste.Icon = img;
            itemEdit.Items.Add(itemPaste);

            itemDelete = new MenuItem();
            itemDelete.Header = "_Delete";
            itemDelete.Click += DeleteOnClick;
            img = new Image();
            img.Source = new BitmapImage(
            new Uri("pack://application:,,,/Images/deleteIco.png"));
            img.Width = iconSize;
            img.Height = img.Width;
            itemDelete.Icon = img;
            itemEdit.Items.Add(itemDelete);
        }

        private void EditOnOpened(object sender, RoutedEventArgs args)
        {
            itemCut.IsEnabled =
            itemCopy.IsEnabled =
            itemDelete.IsEnabled = text.Text != null && text.Text.Length > 0;
            itemPaste.IsEnabled = Clipboard.ContainsText();
        }

        public void CutOnClick(object sender, RoutedEventArgs args)
        {
            CopyOnClick(sender, args);
            DeleteOnClick(sender, args);
        }
        public void CopyOnClick(object sender, RoutedEventArgs e)
        {
            if (text.Text != null && text.Text.Length > 0)
            Clipboard.SetText(text.Text);
        }

        public void PasteOnClick(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
            text.Text = Clipboard.GetText();
        }

        public void DeleteOnClick(object sender, RoutedEventArgs e)
        {
            text.Text = null;
        }
    }
}
