using System;
using System.Windows;
using System.Windows.Input;

namespace Petzold.InherinAppAndWindow
{
    class InherinAppAndWindow
    {
        [STAThread]
        public static void Main()
        {
            MyApplication app = new MyApplication();
            app.Run();
        }
    }


}