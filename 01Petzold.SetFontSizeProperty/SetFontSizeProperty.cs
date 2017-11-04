using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _01Petzold.SetFontSizeProperty
{
    class SetFontSizeProperty : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new SetFontSizeProperty());

        }

        public SetFontSizeProperty()
        {
            Title = "Set FontSize Property";

            SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            FontSize = 16;
            double[] fntsizes = {8, 16, 32};

            // Creating Grid
            Grid grid = new Grid();
            Content = grid;

            // Set rows n colls
            for (int i = 0; i < 2; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                grid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < fntsizes.Length; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(col);
            }

            // Creating 6 buttons
            for (int i = 0; i < fntsizes.Length; i++)
            {
                Button btn = new Button();
                btn.Content = new TextBlock(
                    new Run("Set window FontSize to " + fntsizes[i]));
                btn.Tag = fntsizes[i];
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Click += WindowFontSizeOnClick;
                grid.Children.Add(btn);
                Grid.SetRow(btn, 0);
                Grid.SetColumn(btn, i);

                btn = new Button();
                btn.Content = new TextBlock(
                    new Run("Set button FontSize to " + fntsizes[i]));
                btn.Tag = fntsizes[i];
                btn.HorizontalAlignment = HorizontalAlignment.Stretch;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Click += ButtonFontSizeOnClick;
                grid.Children.Add(btn);
                Grid.SetRow(btn, 1);
                Grid.SetColumn(btn, i);
                
            }

        }

        private void ButtonFontSizeOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            btn.FontSize = (double)btn.Tag;
        }

        private void WindowFontSizeOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            FontSize = (double)btn.Tag;
        }
    }
}
