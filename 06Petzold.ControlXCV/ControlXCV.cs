using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using _05Petzold.CutCopyAndPaste;


namespace _06Petzold.ControlXCV
{
    class ControlXCV : CutCopyAndPaste
    {
        KeyGesture gestCut = new KeyGesture(Key.X, ModifierKeys.Control);
        KeyGesture gestCopy = new KeyGesture(Key.C, ModifierKeys.Control);
        KeyGesture gestPaste = new KeyGesture(Key.V, ModifierKeys.Control);
        KeyGesture gestDelete = new KeyGesture(Key.Delete);
        
        [STAThread]
        public new static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ControlXCV());
        }
        public ControlXCV()
        {
            Title = "Conttol X, C, and V";
            itemCut.InputGestureText = "Ctrl+X";
            itemCopy.InputGestureText = "Ctrl+C";
            itemPaste.InputGestureText = "Ctrl+V";
            itemDelete.InputGestureText = "Delete";
        }
        protected override void OnPreviewKeyDown(KeyEventArgs args)
        {
            base.OnPreviewKeyDown(args);
            args.Handled = true;

            if (gestCut.Matches(null, args))
                CutOnClick(this, args);
            else if (gestCopy.Matches(null, args))
                CopyOnClick(this, args);
            else if (gestPaste.Matches(null, args))
                PasteOnClick(this, args);
            else
                args.Handled = false;
        }
    }
}
