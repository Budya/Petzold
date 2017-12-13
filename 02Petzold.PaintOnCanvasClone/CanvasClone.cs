using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace _02Petzold.PaintOnCanvasClone
{
    class CanvasClone : Panel
    {
        // Set two DepProps
        public static readonly DependencyProperty LeftProperty;
        public static readonly DependencyProperty TopProperty;

        static CanvasClone()
        {
            // Registering dependency properties as attached
            // default value is 0, with any change of position
            // parent position become false
            LeftProperty = DependencyProperty.RegisterAttached("Left",
                typeof (double), typeof (CanvasClone),
            new FrameworkPropertyMetadata(0.0,
                FrameworkPropertyMetadataOptions.AffectsParentArrange));
            TopProperty = DependencyProperty.RegisterAttached("Top",
                typeof(double), typeof(CanvasClone),
            new FrameworkPropertyMetadata(0.0,
                FrameworkPropertyMetadataOptions.AffectsParentArrange));
        }

        // Static methods for reading & writing attached properties
        public static void SetLeft(DependencyObject obj, double value)
        {
            obj.SetValue(LeftProperty, value);
        }
        public static double GetLeft(DependencyObject obj)
        {
            return (double)obj.GetValue(LeftProperty);
        }
        public static void SetTop(DependencyObject obj, double value)
        {
            obj.SetValue(TopProperty, value);
        }
        public static double  GetTop(DependencyObject obj)
        {
            return (double)obj.GetValue(TopProperty);
        }

        // Overriding MeassureOverride simple calls Measure
        // for child objects
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            foreach (UIElement child in InternalChildren)
            {
                child.Measure(new Size(Double.PositiveInfinity, 
                    Double.PositiveInfinity));
            }
            // Default return (0, 0)
            return base.MeasureOverride(sizeAvailable);
        }

        // Overriding ArrangeOverride positing child objects
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            foreach (UIElement child in InternalChildren)
            {
                child.Arrange(new Rect(
                    new Point(GetLeft(child), GetTop(child)),
                    child.DesiredSize));
            }
            return sizeFinal;
        }
    }
}
