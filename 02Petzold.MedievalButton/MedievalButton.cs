using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace _02Petzold.MedievalButton
{
    class MedievalButton : Control
    {
        // Two private fields
        private FormattedText formtxt;
        private bool isMouseReallyOver;

        private string text = " ";

        // Static, readonly fields
        public static readonly DependencyProperty TextProperty;
        public static readonly RoutedEvent KnockEvent;
        public static readonly RoutedEvent PreviewKnockEvent;

        //Static constructor
        static MedievalButton()
        {
            // Registering DepProp
            TextProperty =
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

        // Public interface (property) for dependencyProperty
        public string Text
        {
            set { SetValue(TextProperty, value == null ? " " : value); }
            get { return (string) GetValue(TextProperty); }
        }

        // Public interface for Routed Events
        public event RoutedEventHandler Knock
        {
            add{AddHandler(KnockEvent, value);}
            remove { RemoveHandler(KnockEvent, value);}
        }
        public  event RoutedEventHandler Previev
        {
            add{AddHandler(PreviewKnockEvent, value);}
            remove{RemoveHandler(PreviewKnockEvent, value);}
        }

        // Method runs when possible changing size of button
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            //return base.MeasureOverride(sizeAvailable);
            formtxt = new FormattedText(Text, CultureInfo.CurrentCulture, 
                FlowDirection, new Typeface(FontFamily, FontStyle, 
                    FontWeight, FontStretch), FontSize, Foreground );
            // Внутренние отступы учитываются при вычислении размера
            Size sizeDesired = new Size(Math.Max(48, formtxt.Width)+4,
                formtxt.Height + 4);
            sizeDesired.Width += Padding.Left + Padding.Right;
            sizeDesired.Height += Padding.Top + Padding.Bottom;
            return sizeDesired;
        }

        // Method OnRender calls for redraw button
        protected override void OnRender(DrawingContext dc)
        {
            //base.OnRender(dc);
            //Background color
            Brush brushBackground = SystemColors.ControlBrush;
            if(isMouseReallyOver && IsMouseCaptured)
            {
                brushBackground = SystemColors.ControlDarkBrush;
            }

            // Setting width of pen
            Pen pen = new Pen(Foreground, IsMouseOver ? 2 : 1);

            // Рисование прямоугольника с закругленными углами
            dc.DrawRoundedRectangle(brushBackground, pen,
                new Rect(new Point(0,0),RenderSize), 4,4 );

            // Set general color
            formtxt.SetForegroundBrush(
                IsEnabled ? Foreground : SystemColors.ControlDarkBrush);

            // Set point for begining text
            Point ptText = new Point(2, 2);
            switch (HorizontalContentAlignment)
            {
                case HorizontalAlignment.Left:
                    ptText.X += Padding.Left;
                    break;
                case HorizontalAlignment.Right:
                    ptText.X += RenderSize.Width - formtxt.Width -
                                Padding.Right;
                    break;
                case HorizontalAlignment.Center:
                case HorizontalAlignment.Stretch:
                    ptText.X += (RenderSize.Width - formtxt.Width -
                                 Padding.Left - Padding.Right)/2;
                    break;
            }
            switch (VerticalContentAlignment)
            {
                case VerticalAlignment.Top:
                    ptText.Y += Padding.Top;
                    break;
                case VerticalAlignment.Bottom:
                    ptText.Y +=
                    RenderSize.Height - formtxt.Height - Padding.Bottom;
                    break;
                case VerticalAlignment.Center:
                case VerticalAlignment.Stretch:
                    ptText.Y += (RenderSize.Height - formtxt.Height -
                                 Padding.Top - Padding.Bottom)/2;
                    break;
            }

            // Drawing Text
            dc.DrawText(formtxt, ptText);
        }

        // Mouse events affecting the appearance of the button
        protected override void OnMouseEnter(MouseEventArgs args)
        {
            base.OnMouseEnter(args);
            InvalidateVisual();
        }
        protected override void OnMouseLeave(MouseEventArgs args)
        {
            base.OnMouseLeave(args);
            InvalidateVisual();
        }
        protected override void OnMouseMove(MouseEventArgs args)
        {
            base.OnMouseMove(args);

            //Mouse direction check
            Point pt = args.GetPosition(this);
            bool isReallyOverNow = (pt.X >=0 && pt.X < ActualWidth &&
                                    pt.Y >= 0 && pt.Y < ActualHeight);
            if (isReallyOverNow != isMouseReallyOver)
            {
                isMouseReallyOver = isReallyOverNow;
                InvalidateVisual();
            }
        }

        protected override void OnMouseLeftButtonDown
            (MouseButtonEventArgs args)
        {
            base.OnMouseLeftButtonDown(args);
            CaptureMouse();
            InvalidateVisual();
            args.Handled = true;
        }

        // Событие, фактически инициирующее "Knock"
        protected override void OnMouseLeftButtonUp
            (MouseButtonEventArgs args)
        {
            base.OnMouseLeftButtonUp(args);
            if(IsMouseCaptured)
            {
                if(isMouseReallyOver)
                {
                    OnPreviewKnock();
                    OnKnock();
                }
                args.Handled = true;
                Mouse.Capture(null);
            }

        }

        // When MouseCapture is lost - button reDrowing
        protected override void OnLostMouseCapture
            (MouseEventArgs args)
        {
           base.OnLostMouseCapture(args);
           InvalidateVisual();
        }

        // Buttons "Space" & Enter calls
        // button operation
        protected override void OnKeyDown(KeyEventArgs args)
        {
            base.OnKeyDown(args);
            if (args.Key == Key.Space || args.Key == Key.Enter)
                args.Handled = true;
        }
        protected override void OnKeyUp(KeyEventArgs args)
        {
            base.OnKeyUp(args);
            OnPreviewKnock();
            OnKnock();
            args.Handled = true;
        }

        // Method OnKnock calls event "Knock"
        protected virtual void OnKnock()
        {
            RoutedEventArgs argsEvent = new RoutedEventArgs();
            argsEvent.RoutedEvent = MedievalButton.KnockEvent;
            argsEvent.Source = this;
            RaiseEvent(argsEvent);
        }

        protected virtual void OnPreviewKnock()
        {
            RoutedEventArgs argsEvent = new RoutedEventArgs();
            argsEvent.RoutedEvent = MedievalButton.PreviewKnockEvent;
            argsEvent.Source = this;
            RaiseEvent(argsEvent);
        }
    }
}
