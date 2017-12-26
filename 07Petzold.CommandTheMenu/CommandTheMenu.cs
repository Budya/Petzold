using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;

namespace _07Petzold.CommandTheMenu
{
    class CommandTheMenu : Window
    {
        private TextBlock text;

        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new CommandTheMenu());
        }

        public CommandTheMenu()
        {
            Title = "Command The Menu";

            // Creation DockPanel
            DockPanel dock = new DockPanel();
            Content = dock;

            // Creation menu in the top of window
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            // Creation TextBlock object, for other free space
            text = new TextBlock();
            text.Text = "Sample clipboard text";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.FontSize = 32; //24 p
            text.TextWrapping = TextWrapping.Wrap;
            dock.Children.Add(text);

            // Creation menu Edit
            MenuItem itemEdit = new MenuItem();
            itemEdit.Header = "_Edit";
            menu.Items.Add(itemEdit);

            // Creation commands of menu Edit
            MenuItem itemCut = new MenuItem();
            itemCut.Header = "Cu_t";
            itemCut.Command = ApplicationCommands.Cut;
            itemEdit.Items.Add(itemCut);

            MenuItem itemCopy = new MenuItem();
            itemCopy.Header = "_Copy";
            itemCopy.Command = ApplicationCommands.Copy;
            itemEdit.Items.Add(itemCopy);

            MenuItem itemPaste = new MenuItem();
            itemPaste.Header = "_Paste";
            itemPaste.Command = ApplicationCommands.Paste;
            itemEdit.Items.Add(itemPaste);

            MenuItem itemDelete = new MenuItem();
            itemDelete.Header = "_Delete";
            itemDelete.Command = ApplicationCommands.Delete;
            itemEdit.Items.Add(itemDelete);

            // Adding a bindings of commands in main window collection
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut,
                CutOnExecute, CutCanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy,
                CopyOnExecute, CutCanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste,
                PasteOnExecute, PasteCanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete,
                DeleteOnExecute, CutCanExecute));

            // Addon
            InputGestureCollection collGestures = new InputGestureCollection();
            collGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));

            RoutedUICommand commRestore =
            new RoutedUICommand("_Restore", "Restore", GetType(), collGestures);

            MenuItem itemRestore = new MenuItem();
            itemRestore.Header = "_Restore";
            itemRestore.Command = commRestore;
            itemEdit.Items.Add(itemRestore);

            CommandBindings.Add(new CommandBinding(commRestore, RestoreOnExecute));

        }

        private void RestoreOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            text.Text = "Sample clipboard text";
        }

        private void CutCanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = text.Text != null && text.Text.Length > 0;
        }

        private void PasteCanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = Clipboard.ContainsText();
        }

        private void CutOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            ApplicationCommands.Copy.Execute(null, this);
            ApplicationCommands.Delete.Execute(null, this);
        }

        private void CopyOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            Clipboard.SetText(text.Text);
        }

        private void PasteOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            text.Text = Clipboard.GetText();
        }

        private void DeleteOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            text.Text = null;
        }
    }
}
