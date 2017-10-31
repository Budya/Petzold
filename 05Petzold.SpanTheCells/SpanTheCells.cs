using System;
using System.Windows;
using System.Windows.Controls;

namespace _05Petzold.SpanTheCells
{
    class SpanTheCells : Window
    { 
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new SpanTheCells());
        }

        public SpanTheCells()
        {
            Title = "Span The Cells";
            SizeToContent = SizeToContent.WidthAndHeight;

            // Create Grid
            Grid grid = new Grid();
            grid.Margin = new Thickness(5);
            Content = grid;

            // Creating Rows
            for (int i = 0; i < 6; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowDef);
            }

            // Creating Columns
            for (int i = 0; i < 4; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                if(i==1)
                {
                    colDef.Width = new GridLength(100, GridUnitType.Star);
                }
                else
                {
                    colDef.Width = GridLength.Auto;
                }
                grid.ColumnDefinitions.Add(colDef);
            }

            //Creating Labels & TextBoxes
            string[] astrLabel = {
                                    "_First Name:", "_Last Name;",
                                    "_Social security number:",
                                    "_Credit card number:",
                                    "_Other personal stuff:"
                                 };
            for (int i = 0; i < astrLabel.Length; i++)
            {
                Label lbl = new Label();
                lbl.Content = astrLabel[i];
                lbl.VerticalContentAlignment = VerticalAlignment.Center;
                grid.Children.Add(lbl);
                Grid.SetRow(lbl, i);
                Grid.SetColumn(lbl, 0);

                TextBox textBox = new TextBox();
                textBox.Margin = new Thickness(5);
                grid.Children.Add(textBox);
                Grid.SetRow(textBox, i);
                Grid.SetColumn(textBox, 1);
                Grid.SetColumnSpan(textBox, 3); // Будет занимать 
                                                // 3 столбца начиная со второго
            }

            // Creating Buttons
            Button btn = new Button();
            btn.Content = "Submit";
            btn.Margin = new Thickness(5);
            btn.IsDefault = true;
            btn.Click += delegate { Close(); };
            grid.Children.Add(btn);
            Grid.SetRow(btn, 5);
            Grid.SetColumn(btn, 2);

            btn = new Button();
            btn.Content = "Cancel";
            btn.Margin = new Thickness(5);
            btn.IsCancel = true;
            btn.Click += delegate { Close(); };
            grid.Children.Add(btn);
            Grid.SetRow(btn, 5);
            Grid.SetColumn(btn, 3);

            // Set Focus to first textBox
            grid.Children[1].Focus();



        }
    }
}
