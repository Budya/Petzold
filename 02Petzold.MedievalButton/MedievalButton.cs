using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace _02Petzold.MedievalButton
{
    class MedievalButton : Control
    {
        // Two private fields
        private FormattedText formtext;
        private bool isMouseReallyOver;

        // Static, readonly fields
        public static readonly DependencyProperty TextProperty;
        public static readonly RoutedEvent KnockEvent;
        public static readonly RoutedEvent PreviewKnockEvent;

        //Static constructor
        static MedievalButton()
        {
            // Registering DepProp
            DependencyProperty.Register("Text", typeof (string),
                typeof (MedievalButton),
                    new FrameworkPropertyMetadata(" ",
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

            // Registering routedEvents
            KnockEvent =
            EventManager.RegisterRoutedEvent("Knock",
                RoutingStrategy.Bubble, typeof (RoutedEventHandler),
                typeof (MedievalButton));

            PreviewKnockEvent =
            EventManager.RegisterRoutedEvent("PreviewKnock",
                RoutingStrategy.Tunnel, typeof (RoutedEventHandler),
                typeof (MedievalButton));
        }
    }
}
