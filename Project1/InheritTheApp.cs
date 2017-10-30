using System;
using System.Windows;
using System.Windows.Input;



namespace Petzold.InheritTheApp
{
    class InheritTheApp : Application
    {
        [STAThread]
        public static void Main()
        {
            InheritTheApp app = new InheritTheApp();
            app.Run();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);

            Window win = new Window();
            win.Title = "Inherit The App";
            win.Show();

        }

        protected override void OnSessionEnding(SessionEndingCancelEventArgs args)
        {
            //base.OnSessionEnding(args);

            MessageBoxResult result =
                             MessageBox.Show("Do you want to save yout data ?",
                                              MainWindow.Title,
                                              MessageBoxButton.YesNoCancel,
                                              MessageBoxImage.Question, 
                                              MessageBoxResult.Yes);
            args.Cancel = (result == MessageBoxResult.Cancel);
        }
    
        
    }

}