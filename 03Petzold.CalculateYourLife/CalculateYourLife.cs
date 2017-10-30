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

namespace _03Petzold.CalculateYourLife
{
    class CalculateYourLife : Window
    {
        TextBox txtBoxBegin, txtBoxEnd;
        Label lblLifeYears;
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new CalculateYourLife());
        }

        public CalculateYourLife()
        {
            Title = "Calculate Your Life";

            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;

            // Cоздание объекта Grid
            Grid grid = new Grid();
            Content = grid;

            //Defining Rows & Collumns
            for (int i = 0; i < 3; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowDef);
            }

            for (int i = 0; i < 2; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                colDef.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(colDef);
            }

            // Первый объект Label
            Label lblBeg = new Label();
            lblBeg.Content = "Begin Date: ";
            Grid.SetRow(lblBeg, 0);
            Grid.SetColumn(lblBeg, 0);

            // Первый объект TextBox
            txtBoxBegin = new TextBox();
            txtBoxBegin.Text = new DateTime(1980, 1, 1).ToShortTimeString();
            txtBoxBegin.TextChanged += TextBoxOnTextChanged;
            grid.Children.Add(txtBoxBegin);
            Grid.SetRow(txtBoxBegin, 0);
            Grid.SetColumn(txtBoxBegin, 1);

            // Второй объект Label
            Label lblEnd = new Label();
            lblEnd.Content = "End Date: ";
            grid.Children.Add(lblEnd);
            Grid.SetRow(lblEnd, 1);
            Grid.SetColumn(lblEnd, 0);

            // Второй объект TextBox
            txtBoxEnd = new TextBox();
            txtBoxEnd.TextChanged += TextBoxOnTextChanged;
            grid.Children.Add(txtBoxEnd);
            Grid.SetRow(txtBoxEnd, 1);
            Grid.SetColumn(txtBoxEnd, 1);

            // Третий объект Label
            Label lblLife = new Label();
            lblLife.Content = "Life in years: ";
            grid.Children.Add(lblLife);
            Grid.SetRow(lblLife, 2);
            Grid.SetColumn(lblLife, 1);

            // Объект Label для вычисленного результата
            lblLifeYears = new Label();
            grid.Children.Add(lblLifeYears);
            Grid.SetRow(lblLifeYears, 2);
            Grid.SetColumn(lblLifeYears, 1);

            // Задание внешних отступов
            Thickness thick = new Thickness(5);
            grid.Margin = thick;
            foreach (Control ctrl in grid.Children)
            {
                ctrl.Margin = thick;
            }

            // Передача фокуса и инициирование события
            txtBoxBegin.Focus();
            txtBoxEnd.Text = DateTime.Now.ToShortDateString();
            
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime dtBeg, dtEnd;
            if (DateTime.TryParse(txtBoxBegin.Text, out dtBeg) &&
                DateTime.TryParse(txtBoxEnd.Text, out dtEnd))
            {
                int iYears = dtEnd.Year - dtBeg.Year;
                int iMonths = dtEnd.Month - dtBeg.Month;
                int iDays = dtBeg.Day - dtEnd.Day;
                if(iDays < 0)
                {
                    iDays += DateTime.DaysInMonth(dtEnd.Year, 1 +
                                          (dtEnd.Month + 10)%12);
                    iMonths -= 1;
                }
                if(iMonths < 0)
                {
                    iMonths += 12;
                    iYears -= 1;
                }
                lblLifeYears.Content =
                    String.Format("{0} year{1}. {2} month{3}. {4} day{5}",
                                  iYears, iYears == 1 ? "" : "s",
                                  iMonths, iMonths == 1 ? "" : "s",
                                  iDays, iDays == 1 ? "" : "s");
            }
            else
            {
                lblLifeYears.Content = "";
            }
        }
    }
}
