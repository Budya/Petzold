using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.UriDialog
{
    class UriDialog : Window
    {
        TextBox txtBox;
        public UriDialog()
        {
            Title = "Enter a URI";
            ShowInTaskbar = false;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            txtBox = new TextBox();
            txtBox.Margin = new Thickness(48);
            Content = txtBox;

            txtBox.Focus();
        }

        public string Text
        {
            set 
            {
                txtBox.Text = value;
                txtBox.SelectionStart = txtBox.Text.Length;
            }
            get { return txtBox.Text; }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //base.OnKeyDown(e);

            if (e.Key==Key.Enter) Close();
        }
    }
}
