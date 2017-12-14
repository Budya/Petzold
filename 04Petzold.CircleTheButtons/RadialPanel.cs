using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _04Petzold.CircleTheButtons
{
    public class RadialPanel : Panel
    {
        // Dependency property
        public static readonly DependencyProperty OrientationProperty;

        // Private fields
        private bool showPieLines;
        private double angleEach; // angle for each child object
        private Size sizeLargest; // size of Largest child object
        private double radius; // radius of circle
        private double outerEdgeFromCenter;
        private double inneEdgeFromCenter;


        // Static constructor for creation
        // dependecy property
        static RadialPanel()
        {
            OrientationProperty =
                DependencyProperty.Register("Orientation",
                typeof (RadialPanelOrientation), typeof (RadialPanel),
                new FrameworkPropertyMetadata(RadialPanelOrientation.ByWidth,
                FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        // Property Orientation
        public RadialPanelOrientation Orientation
        {
            set{SetValue(OrientationProperty, value);}
            get { return (RadialPanelOrientation) GetValue(OrientationProperty); }
        }

        // Property wPieLines
        public bool ShowPieLines
        {
            set
            {
                if (value != showPieLines)
                    InvalidateVisual();
                showPieLines = value;
            }
            get { return showPieLines; }
        }
        // Overriding MeasureOverride
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            if (InternalChildren.Count == 0)
                return new Size(0, 0);
            angleEach = 360.0 / InternalChildren.Count;
            sizeLargest = new Size(0, 0);
            foreach (UIElement child in InternalChildren)
            {
                // Call Measure for each child object ...
                child.Measure(new Size(Double.PositiveInfinity, 
                    Double.PositiveInfinity));
                // ... with following check of DesiredSize property
                sizeLargest.Width = Math.Max(sizeLargest.Width,
                    child.DesiredSize.Width);
                sizeLargest.Height = Math.Max(sizeLargest.Height,
                    child.DesiredSize.Height);
            }
            if (Orientation == RadialPanelOrientation.ByWidth)
            {
                //Calculate distance from center to edge of element
                inneEdgeFromCenter = sizeLargest.Width/2/
                    Math.Tan(Math.PI*angleEach/360);
                outerEdgeFromCenter = inneEdgeFromCenter +
                                      sizeLargest.Height;

                // Calculating radius of circle with size
                // of biggest child object
                radius = Math.Sqrt(Math.Pow(sizeLargest.Width/2, 2) +
                                   Math.Pow(outerEdgeFromCenter, 2));
            }
            else
            {
                // Calculate distance from center to edge of element
                inneEdgeFromCenter = sizeLargest.Height/2/
                    Math.Tan(Math.PI*angleEach/360);
                outerEdgeFromCenter = inneEdgeFromCenter +
                                      sizeLargest.Width;
                // Calculating radius of circle with size
                // of biggest child object
                radius = Math.Sqrt(Math.Pow(sizeLargest.Height/2, 2) +
                                   Math.Pow(outerEdgeFromCenter, 2));
            }
            // Returning size of circle
            return new Size(2 * radius, 2 * radius);
        }
        // Overridin ArrangeOverride
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            double angleChild = 0;
            Point ptCenter = new Point(sizeFinal.Width / 2, 
                sizeFinal.Height / 2);
            double multiplier = Math.Min(sizeFinal.Width/(2*radius),
                                         sizeFinal.Height/(2*radius));
            foreach (UIElement child in InternalChildren)
            {
                // Reset RenderTransform
                child.RenderTransform = Transform.Identity;
                if(Orientation == RadialPanelOrientation.ByWidth)
                {
                    // Placing child object in top
                    child.Arrange(
                        new Rect(ptCenter.X - multiplier * sizeLargest.Width / 2, 
                        ptCenter.Y - multiplier * outerEdgeFromCenter, 
                        multiplier * sizeLargest.Width, 
                        multiplier * sizeLargest.Height));
                }
                else
                {
                    // Placing child object in right side
                    child.Arrange(
                        new Rect(ptCenter.X + multiplier * inneEdgeFromCenter, 
                            ptCenter.Y - multiplier * sizeLargest.Height / 2, 
                            multiplier * sizeLargest.Width,
                            multiplier * sizeLargest.Height));
                }

                // Turn of child object around center
                Point pt = TranslatePoint(ptCenter, child);
                child.RenderTransform =
                    new RotateTransform(angleChild, pt.X, pt.Y);

                // Increase of angle
                angleChild += angleEach;
            }
            return sizeFinal;
        }

        // Overriding OnRender painting edges of sectors (optionly)
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (ShowPieLines)
            {
                Point ptCenter =
                    new Point(RenderSize.Width / 2, RenderSize.Height / 2);
                double multiplier = Math.Min(RenderSize.Width/(2*radius),
                                             RenderSize.Height/(2*radius));
                Pen pen = new Pen(SystemColors.WindowTextBrush, 1);
                pen.DashStyle = DashStyles.Dash;

                // Circle building process
                dc.DrawEllipse(null, pen, ptCenter, multiplier*radius, 
                    multiplier * radius);

                // Initiating of angle
                double angleChild = -angleEach/2;
                if (Orientation == RadialPanelOrientation.ByWidth)
                    angleChild += 90;
                // Loop throught each child to draw radial lines from center
                foreach (UIElement child in InternalChildren)
                {
                    dc.DrawLine(pen, ptCenter, 
                        new Point(ptCenter.X + multiplier * radius *
                            Math.Cos(2*Math.PI * angleChild / 360),
                            ptCenter.Y + multiplier * radius *
                            Math.Sin(2 * Math.PI * angleChild / 360)));
                    angleChild += angleEach;
                }
            }
        }
    }
}
