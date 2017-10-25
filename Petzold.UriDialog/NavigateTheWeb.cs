using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.UriDialog
{
    class NavigateTheWeb : Window
    {
        Frame frm;

        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new NavigateTheWeb());
        }

        public NavigateTheWeb()
        {
            Title = "Navigate the Web";
            
            frm = new Frame();
            Content = frm;

            Loaded += OnWindowLoaded;
        }

        void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            UriDialog dlg = new UriDialog();
            dlg.Owner = this;
            dlg.Text = "http://";
            dlg.ShowDialog();

            try
            {
                frm.Source = new Uri(dlg.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title);
            }

        }
    }
}
