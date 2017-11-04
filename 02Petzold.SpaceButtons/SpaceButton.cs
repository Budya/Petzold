using System;
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
                Content = SpaseOutText(txt);
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
                                                        typeof (SpaceButton), metadata,
                                                        ValidateSpaceValue);
        }

        // Method callback for check value
        static bool ValidateSpaceValue(object  obj)
        {
            int i = (int) obj;
            return i >= 0;
        }
    }
}
