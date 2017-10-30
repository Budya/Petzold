using System;
using System.Windows;


namespace Petzold.SimpleEllipse
{
    class RenderTheGraphic : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new RenderTheGraphic());
        }

        public RenderTheGraphic()
        {
            Title = "Render the Graphic";
            SimpleEllipse ellipse = new SimpleEllipse();
            Content = ellipse;
        }
    }
}
