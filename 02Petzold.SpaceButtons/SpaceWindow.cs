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
    class SpaceWindow : Window
    {
        // DependencyProperty & Property

        public static readonly DependencyProperty SpaceProperty;
        public int Space
        {
            set
            {
                SetValue(SpaceProperty, value);
            }
            get 
            {
                return (int)GetValue(SpaceProperty);
            }
        }

        // Static constructor
        static SpaceWindow()
        {
            // Set metadata
            FrameworkPropertyMetadata metadata = 
                new FrameworkPropertyMetadata();
            metadata.Inherits = true;

            // Adding owner to SpaceProperty
            // and resetting (overriding) metadata
            SpaceProperty = SpaceButton.SpaceProperty.AddOwner(typeof (SpaceWindow));
            SpaceProperty.OverrideMetadata(typeof(SpaceWindow), metadata);

        }

    }
}
