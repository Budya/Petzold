using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _04Petzold.EnterTheGrid
{
    class EnterTheGrid : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new EnterTheGrid());
        }

        public EnterTheGrid()
        {
            Title = "Enter The Grid";

            MinWidth = 300;
            SizeToContent = SizeToContent.WidthAndHeight;

            // Создание объекта StackPanel для содержимого окна
            StackPanel stak = new StackPanel();
            Content = stak;

            // Create Grid 
            Grid grid1 = new Grid();
            grid1.Margin = new Thickness(5);
            stak.Children.Add(grid1);

            // Create rows
            for (int i = 0; i < 5; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid1.RowDefinitions.Add(rowdef);
            }

            // Create Collumns
            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid1.ColumnDefinitions.Add(coldef);
            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            grid1.ColumnDefinitions.Add(coldef);

            // Creat labels & textBoxes
            string[] strLabels =
            {
                "_First name:", "_Last name:",
                "_Social security number:",
                "_Oter personal stuff:"
            };

            for (int i = 0; i < strLabels.Length; i++)
            {
                Label lbl = new Label();
                lbl.Content = strLabels[i];
                lbl.VerticalContentAlignment = VerticalAlignment.Center;
                grid1.Children.Add(lbl);

                Grid.SetRow(lbl, i);
                Grid.SetColumn(lbl, 0);
                TextBox txtbox = new TextBox();
                txtbox.Margin = new Thickness(5);
                grid1.Children.Add(txtbox);
                Grid.SetRow(txtbox, i);
                Grid.SetColumn(txtbox, 1);
            }

            // Creating second grid 
            Grid grid2 = new Grid();
            grid2.Margin = new Thickness(10);
            stak.Children.Add(grid2);


            //Для одной строки создавать определение не обязательно 
            // в определениях строк по умолчанию используется режим звездочка
            grid2.ColumnDefinitions.Add(new ColumnDefinition());
            grid2.ColumnDefinitions.Add(new ColumnDefinition());

            // Creating buttons
            Button btn = new Button();
            btn.Content = "Submit";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.IsDefault = true;
            btn.Click += delegate { Close(); };
            grid2.Children.Add(btn);

            btn = new Button();
            btn.Content = "Cancel";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.IsCancel = true;
            btn.Click += delegate { Close(); };
            grid2.Children.Add(btn);
            Grid.SetColumn(btn,1);

            // Set focus to first textBox
            (stak.Children[0] as Panel).Children[1].Focus();
        }
    }
}
