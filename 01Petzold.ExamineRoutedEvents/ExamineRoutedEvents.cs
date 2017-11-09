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

namespace _01Petzold.ExamineRoutedEvents
{
    public class ExamineRoutedEvents : Application
    {
        static readonly FontFamily fontfam = 
            new FontFamily("Lucida Console");

        const string strFormat = "{0, -30}{1, -15}{2, -15}{3, -15}";
        StackPanel stackOutput;
        DateTime dtLast;

        [STAThread]
        public static void Main(string[] args)
        {
            ExamineRoutedEvents app = new ExamineRoutedEvents();
            app.Run();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Creation Window
            Window win  = new Window();
            win.Title = "Examine Routed Events";

            // Creation Grid obj and set Window as content
            Grid grid = new Grid();
            win.Content = grid;

            // Definition of 3 rows
            RowDefinition rowDef = new RowDefinition();
            rowDef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowDef);

            rowDef = new RowDefinition();
            rowDef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowDef);

            rowDef = new RowDefinition();
            rowDef.Height = new GridLength(100, GridUnitType.Star);
            grid.RowDefinitions.Add(rowDef);

            // Creating Button obj and add it to grid
            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Margin = new Thickness(24);
            btn.Padding = new Thickness(24);
            grid.Children.Add(btn);

            // Creating TextBlock obj and adding it to button as content
            TextBlock txtBlock = new TextBlock();
            txtBlock.FontSize = 24;
            txtBlock.Text = win.Title;
            btn.Content = txtBlock;

            // Creating Hedings who appear up on ScrollViever
            TextBlock textHeading = new TextBlock();
            textHeading.FontFamily = fontfam;
            textHeading.Inlines.Add(new Underline(new Run(
                String.Format(strFormat, "Routed Event", "sender", "Source",
                "OriginalSource"))));
            grid.Children.Add(textHeading);
            Grid.SetRow(textHeading, 1);

            // Creating ScrollViewer obj
            ScrollViewer scroll = new ScrollViewer();
            grid.Children.Add(scroll);
            Grid.SetRow(scroll, 2);

            // Creating StackPanell obj for show events
            stackOutput = new StackPanel();
            scroll.Content = stackOutput;

            // Adding event hendlers
            UIElement[] els = {win, grid, btn, txtBlock};
            foreach (UIElement el in els)
            {
                // Keyboard
                el.PreviewKeyDown += AllPuroposeEventHandler;
                el.PreviewKeyUp += AllPuroposeEventHandler;
                el.PreviewTextInput += AllPuroposeEventHandler;
                el.KeyDown += AllPuroposeEventHandler;
                el.KeyUp += AllPuroposeEventHandler;
                el.TextInput += AllPuroposeEventHandler;

                // Mouse
                el.MouseDown += AllPuroposeEventHandler;
                el.MouseUp += AllPuroposeEventHandler;
                el.PreviewMouseDown += AllPuroposeEventHandler;
                el.PreviewMouseUp += AllPuroposeEventHandler;
                
                // Stilus
                el.StylusDown += AllPuroposeEventHandler;
                el.StylusUp += AllPuroposeEventHandler;
                el.PreviewStylusDown += AllPuroposeEventHandler;
                el.PreviewStylusUp += AllPuroposeEventHandler;

                // Click
                el.AddHandler(Button.ClickEvent, 
                    new RoutedEventHandler(AllPuroposeEventHandler));

            }

            // Showing Window
            win.Show();





        }

        void AllPuroposeEventHandler(object sender, RoutedEventArgs e)
        {
            // Show empty string, if events split by space
            DateTime dtNow = DateTime.Now;
            if (dtNow - dtLast > TimeSpan.FromMilliseconds(100))
            {
                stackOutput.Children.Add(new TextBlock(new Run(" ")));
            }
            dtLast = dtNow;

            // Show info abt event
            TextBlock text = new TextBlock();
            text.FontFamily = fontfam;
            text.Text = String.Format(strFormat, 
                e.RoutedEvent.Name, 
                TypeWithoutNamespace(sender),
                TypeWithoutNamespace(e.Source),
                TypeWithoutNamespace(e.OriginalSource));
            stackOutput.Children.Add(text);
            (stackOutput.Parent as ScrollViewer).ScrollToBottom();

        }

        string TypeWithoutNamespace(object sender)
        {
            string[] astr = sender.GetType().ToString().Split('.');
            return astr[astr.Length - 1];
        }
    }
}
