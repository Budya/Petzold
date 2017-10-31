using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace _08Petzold.ScrollCustomColors
{
    class ScrollCustomColors : Window
    {

        ScrollBar[] scrolls = new ScrollBar[3];
        TextBlock[] txtValues = new TextBlock[3];
        Panel pnlColor;

        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ScrollCustomColors());
        }

        public ScrollCustomColors()
        {
            Title = "Scroll Cusom Colors";

            Width = 500;
            Height = 300;

            // GridMain panel
            Grid gridMain = new Grid();
            Content = gridMain;

            // Seting Columns
            ColumnDefinition colDef = new ColumnDefinition();
            colDef.Width = new GridLength(200, GridUnitType.Pixel);
            gridMain.ColumnDefinitions.Add(colDef);

            colDef = new ColumnDefinition();
            colDef.Width = GridLength.Auto;
            gridMain.ColumnDefinitions.Add(colDef);

            colDef = new ColumnDefinition();
            colDef.Width = new GridLength(100, GridUnitType.Star);
            gridMain.ColumnDefinitions.Add(colDef);

            // Vertical Splitter
            GridSplitter split = new GridSplitter();
            split.ShowsPreview = true;
            split.HorizontalAlignment = HorizontalAlignment.Center;
            split.VerticalAlignment = VerticalAlignment.Stretch;
            split.Width = 6;
            gridMain.Children.Add(split);
            Grid.SetRow(split, 0);
            Grid.SetColumn(split, 1);

            // Shows color in left side of splitter
            pnlColor = new StackPanel();
            pnlColor.Background = new SolidColorBrush(SystemColors.WindowColor);
            gridMain.Children.Add(pnlColor);
            Grid.SetRow(pnlColor, 0);
            Grid.SetColumn(pnlColor, 2);

            // Second panel by left side of splitter
            Grid grid = new Grid();
            gridMain.Children.Add(grid);
            Grid.SetRow(grid, 0);
            Grid.SetColumn(grid, 0);

            //Three strings
            RowDefinition rowDef = new RowDefinition();
            rowDef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowDef);
            
            rowDef = new RowDefinition();
            rowDef.Height = new GridLength(100, GridUnitType.Star);
            grid.RowDefinitions.Add(rowDef);

            rowDef = new RowDefinition();
            rowDef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowDef);

            //Three columns
            for (int i = 0; i < 3; i++)
            {
                colDef = new ColumnDefinition();
                colDef.Width = new GridLength(33, GridUnitType.Star);
                grid.ColumnDefinitions.Add(colDef);
            }

            for (int i = 0; i < 3; i++)
            {
                Label lbl = new Label();
                lbl.Content = new string[] {"Red", "Green", "Blue"}[i];
                lbl.HorizontalAlignment = HorizontalAlignment.Center;
                grid.Children.Add(lbl);
                Grid.SetRow(lbl, 0);
                Grid.SetColumn(lbl, i);

                scrolls[i] = new ScrollBar();
                scrolls[i].Focusable = true;
                scrolls[i].Orientation = Orientation.Vertical;
                scrolls[i].Minimum = 0;
                scrolls[i].Maximum = 255;
                scrolls[i].SmallChange = 1;
                scrolls[i].LargeChange = 16;
                scrolls[i].ValueChanged += ScrollOnValueChanged;
                grid.Children.Add(scrolls[i]);
                Grid.SetRow(scrolls[i], 1);
                Grid.SetColumn(scrolls[i], i);

                txtValues[i] = new TextBlock();
                txtValues[i].TextAlignment = TextAlignment.Center;
                txtValues[i].HorizontalAlignment = HorizontalAlignment.Center;
                txtValues[i].Margin = new Thickness(5);
                grid.Children.Add(txtValues[i]);
                Grid.SetRow(txtValues[i], 2);
                Grid.SetColumn(txtValues[i], i);

            }

            // Initiation of Scroll
            Color clr = (pnlColor.Background as SolidColorBrush).Color;
            scrolls[0].Value = clr.R;
            scrolls[1].Value = clr.G;
            scrolls[2].Value = clr.B;

            // Set Focus
            scrolls[0].Focus();
        }

        private void ScrollOnValueChanged(object sender, RoutedEventArgs e)
        {
            ScrollBar scroll = sender as ScrollBar;
            Panel pnl = scroll.Parent as Panel;
            TextBlock txt = pnl.Children[1 +
                pnl.Children.IndexOf(scroll)] as TextBlock;

            txt.Text = String.Format("{0}\n0x{0:X2}", (int) scroll.Value);
            pnlColor.Background =
                new SolidColorBrush(
                    Color.FromRgb((byte) scrolls[0].Value,
                                  (byte) scrolls[1].Value,
                                  (byte) scrolls[2].Value));

        }
    }
}
