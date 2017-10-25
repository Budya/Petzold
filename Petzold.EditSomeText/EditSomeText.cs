using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.EditSomeText
{
    class EditSomeText : Window
    {
        private static string strFileName = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
                "Petzold.EditSomeText\\EditSomeText.txt");

        TextBox txtBox;

        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new EditSomeText());
        }

        public EditSomeText()
        {
            Title = "Edit some text";

            txtBox = new TextBox();
            txtBox.AcceptsReturn = true;
            txtBox.TextWrapping = TextWrapping.Wrap;
            txtBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtBox.KeyDown += TextBoxOnKeyDown;
            Content = txtBox;

            try
            {
                txtBox.Text = File.ReadAllText(strFileName);
            }
            catch 
            {
                
            }

            txtBox.CaretIndex = txtBox.Text.Length;
            txtBox.Focus();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //base.OnClosing(e);
            try
            {
                Directory.CreateDirectory(
                    Path.GetDirectoryName(strFileName));
                File.WriteAllText(strFileName, txtBox.Text);
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show("File could not be saved: " +
                                        ex.Message + "\nClose programm anyway?", Title,
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Exclamation);
            }
        }

        void TextBoxOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                txtBox.SelectedText = DateTime.Now.ToString();
                txtBox.CaretIndex = txtBox.SelectionStart + txtBox.SelectionLength;
            }
        }
    }
}
