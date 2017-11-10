using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace _03Petzold.Keystrokes
{
    class ExamineKeystrokes : Window
    {
        StackPanel stack;
        ScrollViewer scroll;

        string strHeader = "Event Key Sys-Key Text " +
                                   "Ctrl-Text Sys-Text Ime KeyStates " +
                                   "IsDown IsUp IsToggled IsRepeat";

        string strFormatKey = "{0,-10}{1,-20}{2,-10}{3,-10}{4,-15}{5,-8}{6,-7}{8,-10}";
        string strFormatText = "{0,-10}{1,-10}{2,-10}{3,-10}";

        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ExamineKeystrokes());

        }

        public ExamineKeystrokes()
        {
            Title = "Examine Keystrokes";
            FontFamily = new FontFamily("Courier New");

            Grid grid = new Grid();
            Content = grid;

            // One row in Auto mode
            // second take all other space
            RowDefinition rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);
            grid.RowDefinitions.Add(new RowDefinition());

            // Header output
            TextBlock textHeader = new TextBlock();
            textHeader.FontWeight = FontWeights.Bold;
            textHeader.Text = strHeader;
            grid.Children.Add(textHeader);

            // Creation StackPanel as child of ScrollViever
            // for output events

            scroll = new ScrollViewer();
            grid.Children.Add(scroll);
            Grid.SetRow(scroll, 1);
            stack = new StackPanel();
            scroll.Content = stack;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            DisplayKeyInfo(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            DisplayKeyInfo(e);
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            string str =
            String.Format(strFormatText, e.RoutedEvent.Name,
            e.Text, e.ControlText, e.SystemText);
            DisplayInfo(str);
        }

        void DisplayKeyInfo(KeyEventArgs args)
        {
            string str =
            String.Format(strFormatKey, args.RoutedEvent.Name, args.Key,
            args.SystemKey, args.ImeProcessedKey, args.KeyStates,
            args.IsDown, args.IsUp, args.IsToggled, args.IsRepeat);
            DisplayInfo(str);
        }
        void DisplayInfo(string str)
        {
            TextBlock text = new TextBlock();
            text.Text = str;
            stack.Children.Add(text);
            scroll.ScrollToBottom();
        }
    }
}
