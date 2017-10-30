using System;
using System.Windows;
using System.Windows.Input;

namespace Petzold.HandleAnEvent
{
    class HandleAnEvent
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();

            Window win = new Window();
            win.Title = "Hendle An Event";
            win.MouseDown += WindowOnMoustDown;

            app.Run(win);
        }

        static void WindowOnMoustDown(object sender, 
                                      MouseButtonEventArgs args)
        {
            Window win = sender as Window;
            string strMessage = 
            string.Format("Window clicked with {0} MouseButtonEventArgs at point ({1})", args.ChangedButton, args.GetPosition(win));
            MessageBox.Show(strMessage, win.Title);
        }

    }
    
}