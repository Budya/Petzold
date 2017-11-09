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


namespace _02Petzold.SpaceButtons
{
    class SpaceButton : Button  
    {
        // Traditional private field and opened property

        string txt;

        public string Text
        {
            set 
            { 
                txt = value;
                Content = SpaceOutText(txt);
            }
            get { return txt; }
        }

        // DependencyProperty & opened property
        public static readonly DependencyProperty SpaceProperty;

        public int Space
        {
            set
            {
                SetValue(SpaceProperty, value);
            }
            get { return (int) GetValue(SpaceProperty); }
        }

        // Static constructor
        static SpaceButton()
        {
            //Set metadata
            FrameworkPropertyMetadata metadata = 
                new FrameworkPropertyMetadata();
            metadata.DefaultValue = 1;
            metadata.Inherits = true;
            metadata.PropertyChangedCallback += OnSpacePropertyChanged;

            // Registering DependencyProperty
            SpaceProperty = DependencyProperty.Register("Space", typeof (int),
                                                        typeof (SpaceButton),
                                                        metadata,
                                                        ValidateSpaceValue);
        }

        // Method callback for check value
        static bool ValidateSpaceValue(object  obj)
        {
            int i = (int) obj;
            return i >= 0;
        }

        // Method callback for Notify about property changing
        static  void OnSpacePropertyChanged(DependencyObject obj, 
            DependencyPropertyChangedEventArgs args)
        {
            SpaceButton btn = obj as SpaceButton;
            btn.Content = btn.SpaceOutText(btn.txt);
        }

        // Method for insert spaces in text
        string SpaceOutText(string str)
        {
            if (str == null) return null;
            
            StringBuilder build = new StringBuilder();
            foreach (char ch in str)
            {
                build.Append(ch + new string(' ', Space));
            }
            return build.ToString();
        }
    }
}
