using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace _06Petzold.ExploreDirectories
{
    class ExploreDirectories : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ExploreDirectories());

        }

        public ExploreDirectories()
        {
            Title = "Explore Directories";

            ScrollViewer scroll = new ScrollViewer();
            Content = scroll;

            WrapPanel wrap = new WrapPanel();
            scroll.Content = wrap;

            wrap.Children.Add(new FileSystemInfoButton());

        }
    }
}
