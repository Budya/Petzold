using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Text;

namespace Petzold.RecordKeyStrokes
{
    class RecordKeyStrokes : Window
    {
        StringBuilder build = new StringBuilder("text");
        
        [STAThread]
        public static void Main(string[] args)
        {
            Application app  = new Application();
            app.Run(new RecordKeyStrokes());
        }
        
        public RecordKeyStrokes()
        {
            Title = "Record Key Strokes";
            Content = build;
            FontStyle = FontStyles.Normal;
            FontFamily = new FontFamily("Times New Roman");
            FontSize = 48;
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
          /*  base.OnTextInput(e);
            string str = Content as string;
            if (e.Text == "\b")
            {
                if (str.Length > 0)
                    str = str.Substring(0, str.Length - 1);
            }
            else
            {
                str += e.Text;
            }
            Content = str; */

            if (e.Text == "\b")
            {
                if (build.Length > 0) build.Remove(build.Length - 1, 1);
            }
            else
            {
                build.Append(e.Text);
            }
            Content = null;
            Content = build;

        }
    
    }
}
