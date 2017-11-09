using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _01Petzold.ExamineRoutedEvents
{
    public class ExamineRoutedEvents : Application
    {
        static readonly FontFamily fontfam = 
            new FontFamily("Lucida Console");

        const string strFormat = "{0, -30}{1, -15}{2, -15}{3, -15}";
        StackPanel stackOutput;
        DateTime dtLast;

        [STAThread]
        public static void Main(string[] args)
        {
            ExamineRoutedEvents app = new ExamineRoutedEvents();
            app.Run();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Creation Window
            Window win  = new Window();
            win.Title = "Examine Routed Events";


        }

    }
}
