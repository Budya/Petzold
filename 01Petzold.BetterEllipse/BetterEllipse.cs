using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using _01Petzold.BetterEllipse;


namespace _01Petzold.BetterEllipse
{
    public class BetterEllipse : FrameworkElement
    {
        // DependencyProperty
        public static readonly DependencyProperty FillProperty;
        public static readonly DependencyProperty StrokeProperty;

        // public interfaces
        public Brush Fill
        {
            set {SetValue(FillProperty, value);}
            get { return (Brush) GetValue(FillProperty); }
        }

        public Pen Stroke
        {
            set { SetValue(StrokeProperty, value); }
            get { return (Pen) GetValue(StrokeProperty); }
        }

        //Static constructor
        static BetterEllipse()
        {
            FillProperty =
            DependencyProperty.Register("Fill", typeof (Brush),
            typeof (BetterEllipse), new FrameworkPropertyMetadata(null,
            FrameworkPropertyMetadataOptions.AffectsParentMeasure));

            StrokeProperty =
            DependencyProperty.Register("Stroke", typeof (Pen),
            typeof (BetterEllipse), new FrameworkPropertyMetadata(null,
            FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        // Override of MeasureOverride
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            Size sizeDesired = base.MeasureOverride(sizeAvailable);
            if (Stroke != null)
                sizeDesired = new Size(Stroke.Thickness, Stroke.Thickness);
            return sizeDesired;
        }

        // Overriding of OnRender
        protected override void OnRender(DrawingContext dc)
        {
            Size size = RenderSize;

            // Adjust the size of the playback, 
            // taking into account the thickness of the pen
            if (Stroke != null)
            {
                size.Width = Math.Max(0, size.Width - Stroke.Thickness);
                size.Height = Math.Max(0, size.Height - Stroke.Thickness);
            }

            // Drawing an allipse
            dc.DrawEllipse(Fill, Stroke, 
                new Point(RenderSize.Width / 2, RenderSize.Height / 2),
                size.Width / 2, size.Height / 2);

            // For Text
            FormattedText formtxt = 
                new FormattedText("Hello, ellipse", CultureInfo.CurrentCulture, 
                    FlowDirection, new Typeface("Times New Roman Italic"),
                    24, Brushes.DarkBlue);
            Point ptText = new Point(
                (RenderSize.Width - formtxt.Width) / 2, 
                (RenderSize.Height - formtxt.Height) / 2);
            
            dc.DrawText(formtxt, ptText);
        }

    }
}
