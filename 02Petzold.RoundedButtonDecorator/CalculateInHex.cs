using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;



namespace _02Petzold.RoundedButtonDecorator
{
    class CalculateInHex : Window
    {
        //Private fields
        private RoundedButton btnDisplay;
        private ulong numDisplay;
        ulong numFirst;
        private bool bNewNumber = true;
        private char chOperation = '=';

        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new CalculateInHex());
        }

        // Constructor
        public CalculateInHex()
        {
            Title = "Calculate In Hex";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            // Creating Grid obj as content
            Grid grid = new Grid();
            grid.Margin = new Thickness(4);
            Content = grid;

            // Creating 5 Columns
            for (int i = 0; i < 5; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(col);
            }

            // Creating 7 rows
            for (int i = 0; i < 7; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                grid.RowDefinitions.Add(row);
            }

            // Text for buttons
            string[] strButtons = {
                                    "0",
                                    "D", "E", "F", "+", "&",
                                    "A", "B", "C", "-", "|",
                                    "7", "8", "9", "*", "^",
                                    "4", "5", "6", "/", "<<",
                                    "1", "2", "3", "%", ">>",
                                    "0", "Back", "Equals" };
            int iRow = 0, iCol = 0;

            // Creating Buttons
            foreach (string str in strButtons)
            {
                //Creating RoundedButton
                RoundedButton btn = new RoundedButton();
                btn.Focusable = false;
                btn.Height = 32;
                btn.Margin = new Thickness(4);
                btn.Click += ButtonOnClick;

                // Creating TextBlock obj
                // For property Child of obj RoundedButton
                TextBlock txt = new TextBlock();
                txt.Text = str;
                btn.Child = txt;

                // Adding RoundedButton obj on Grid panel
                grid.Children.Add(btn);
                Grid.SetRow(btn, iRow);
                Grid.SetColumn(btn, iCol);

                // Special for Display Button
                if (iRow == 0 && iCol == 0)
                {
                    btnDisplay = btn;
                    btn.Margin = new Thickness(4,4,4,6);
                    Grid.SetColumnSpan(btn, 5);
                    iRow = 1;
                }
                // For Back & Equals
                else if (iRow == 6 && iCol > 0)
                {
                    Grid.SetColumnSpan(btn, 2);
                    iCol += 2;
                }
                // For all other buttons
                else
                {
                    btn.Width = 32;
                    if (0 == (iCol = (iCol + 1) % 5))
                    iRow++;
                }
            }
        }
        // Hendler For Click Event
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            // Getting button, whos have been clicked
            RoundedButton btn = args.Source as RoundedButton;
            if(btn == null)
                return;
            
            // Getting text of button & first char
            string strButton = (btn.Child as TextBlock).Text;
            char chButton = strButton[0];

            // For special cases
            if (strButton == "Equals")
                chButton = '=';
            if (btn == btnDisplay)
                numDisplay = 0;
            else if (strButton == "Back")
                numDisplay /= 16;

            // Hex digits
            else if (Char.IsLetterOrDigit(chButton))
            {
                if(bNewNumber)
                {
                    numFirst = numDisplay;
                    numDisplay = 0;
                    bNewNumber = false;
                }
                if (numDisplay <= ulong.MaxValue >> 4)
                    numDisplay = 16*numDisplay + (ulong) (chButton -
                        (Char.IsDigit(chButton) ? '0' : 'A' - 10)); 
                
            }

            // Working mode
            else
            {
                if(!bNewNumber)
                {
                    switch (chOperation)
                    {
                        case '=': break;
                        case '+': numDisplay = numFirst + numDisplay; break;
                        case '-': numDisplay = numFirst - numDisplay; break;
                        case '*': numDisplay = numFirst * numDisplay; break;
                        case '&': numDisplay = numFirst & numDisplay; break;
                        case '|': numDisplay = numFirst | numDisplay; break;
                        case '^': numDisplay = numFirst ^ numDisplay; break;
                        case '<': numDisplay = numFirst << (int)numDisplay; break;
                        case '>': numDisplay = numFirst >> (int)numDisplay; break;
                        case '/':
                            numDisplay =
                                numDisplay != 0
                                    ? numFirst/numDisplay
                                    : ulong.MaxValue;
                            break;
                        case '%':
                            numDisplay =
                                numDisplay != 0
                                    ? numFirst%numDisplay
                                    : ulong.MaxValue;
                            break;
                        default:numDisplay = 0; break;
                    }
                }
                bNewNumber = true;
                chOperation = chButton;
            }

            // Correcting input
            TextBlock text = new TextBlock();
            text.Text = String.Format("{0:X}", numDisplay);
            btnDisplay.Child = text;
        }
        protected override void OnTextInput(TextCompositionEventArgs args)
        {
            base.OnTextInput(args);
            if (args.Text.Length == 0) return;
            
            // Get pushed button
            char chKey = Char.ToUpper(args.Text[0]);

            // Browsing buttons Перебор кнопок
            foreach (UIElement child in (Content as Grid).Children)
            {
                RoundedButton btn = child as RoundedButton;
                string strButton = (btn.Child as TextBlock).Text;

                // logic to check for matching button
                if ((chKey == strButton[0] && btn != btnDisplay &&
                    strButton != "Equals" && strButton != "Back") ||
                    (chKey == '=' && strButton == "Equals")||
                    (chKey == '\r' && strButton == "Equals") ||
                    (chKey == '\b' && strButton == "Back") ||
                    (chKey == '\x1B' && btn == btnDisplay))
                {
                    // Imitation event Click for hendle pushing button
                    RoutedEventArgs argsClick = 
                    new RoutedEventArgs(RoundedButton.ClickEvent, btn);
                    btn.RaiseEvent(argsClick);

                    // Imitation button press
                    btn.IsPressed = true;

                    // Settin timmer for settin button in basic state
                    DispatcherTimer tmr = new DispatcherTimer();
                    tmr.Interval = TimeSpan.FromMilliseconds(100);
                    tmr.Tag = btn;
                    tmr.Tick += TimerOnTick;
                    tmr.Start();
                    args.Handled = true;
                }
            }
        }
        void TimerOnTick(Object sender, EventArgs args)
        {
            // Abort button push
            DispatcherTimer tmr = sender as DispatcherTimer;
            RoundedButton btn = tmr.Tag as RoundedButton;
            btn.IsPressed = false;

            // Stop timer and deleting event handler
            tmr.Stop();
            tmr.Tick -= TimerOnTick;
        }
    }
}
