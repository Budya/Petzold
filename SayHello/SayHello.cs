using System;
using System.Windows;
using System.Windows.Media;

namespace Petzold.SayHello
{
    class SayHello
    {
        [STAThread]
        public static void Main()
        {
            Window win = new Window();
            win.Title = "SayHello";
            win.Show(); // изменено с просто  SHOW
            win.WindowStyle = WindowStyle.ThreeDBorderWindow;
            win.ResizeMode = ResizeMode.CanResizeWithGrip;
            win.Background = new SolidColorBrush(Color.FromRgb(0, 255, 255));

            Application app = new Application();
            app.Run();
        }
    }
}