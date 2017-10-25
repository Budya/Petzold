using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.CommandTheButton
{
    class CommandTheButton : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new CommandTheButton());

        }

        public CommandTheButton()
        {
            Title = "Command The Button";
            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Command = ApplicationCommands.Paste;
            btn.Content = ApplicationCommands.Paste.Text;
            Content = btn;


            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste,
                PasteOnExecute, PasteCanExecute));
            
        }

        private void PasteCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsText();

        }

        private void PasteOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            Title = Clipboard.GetText();

        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Title = "Command the Button";
        }

    }
}
