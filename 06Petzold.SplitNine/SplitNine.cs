using System;
using System.Windows;
using System.Windows.Controls;


namespace _06Petzold.SplitNine
{
    class SplitNine : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new SplitNine());

        }

        public SplitNine()
        {
            Title = "Split Nine";

            Grid grid = new Grid();
            Content = grid;

            // Creating Rows & Columns
            for (int i = 0; i < 3; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.RowDefinitions.Add(new RowDefinition());

            }

            // Creating 9 buttons

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button btn = new Button();
                    btn.Content = "Row " + j + " and Column " + i;
                    btn.Margin = new Thickness(10);
                    grid.Children.Add(btn);
                    Grid.SetRow(btn, j);
                    Grid.SetColumn(btn, i);
                }
            }

            // Creating Splitter
            GridSplitter split = new GridSplitter();
            //split.Width = 6;
            split.HorizontalAlignment = HorizontalAlignment.Stretch;
            split.VerticalAlignment = VerticalAlignment.Top;
            split.ResizeDirection = GridResizeDirection.Columns;
            split.Height = 6;
            grid.Children.Add(split);
            Grid.SetRow(split, 1);
            Grid.SetColumn(split, 1);
            //Grid.SetRowSpan(split, 3);

        }
    }
}
