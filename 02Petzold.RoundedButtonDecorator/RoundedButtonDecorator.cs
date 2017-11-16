using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace _02Petzold.RoundedButtonDecorator
{
    class RoundedButtonDecorator : Decorator
    {
        // Public dependProp
        public static readonly DependencyProperty IsPressedProperty;

        // Static Constructor
        static RoundedButtonDecorator()
        {
            IsPressedProperty =
                DependencyProperty.Register("IsPressed", typeof (bool),
                    typeof (RoundedButtonDecorator),
                    new FrameworkPropertyMetadata(false,
                        FrameworkPropertyMetadataOptions.AffectsRender));
        }

        // Public property
        public bool IsPressed
        {
            set {SetValue(IsPressedProperty, value);}
            get { return (bool) GetValue(IsPressedProperty); }
        }

        // Override MeassureOverride
        protected override Size MeasureOverride(Size sizeAvailible)
        {
            //return base.MeasureOverride(sizeAvailible);
            Size szDesired = new Size(2,2);
            sizeAvailible.Width -= 2;
            sizeAvailible.Height -= 2;
            if (Child != null)
            {
                Child.Measure(sizeAvailible);
                szDesired.Width += Child.DesiredSize.Width;
                szDesired.Height += Child.DesiredSize.Height;
            }
            return szDesired;
        }

        // Override ArrangeOverride
        protected override Size ArrangeOverride(Size sizeArrange)
        {
            //return base.ArrangeOverride(sizeArrange);
            if (Child != null)
            {
                Point ptChild =
                    new Point(Math.Max(1, (sizeArrange.Width -
                        Child.DesiredSize.Width) / 2), 
                        Math.Max(1, (sizeArrange.Height - 
                        Child.DesiredSize.Height) / 2));
                Child.Arrange(new Rect(ptChild, Child.DesiredSize));
            }
            return sizeArrange;
        }

        //Override OnRender
        protected override void OnRender(DrawingContext dc)
        {
            //base.OnRender(dc);
            RadialGradientBrush brush = new RadialGradientBrush(
                IsPressed ? SystemColors.ControlDarkColor : 
                SystemColors.ControlLightLightColor,
                SystemColors.ControlColor);
            brush.GradientOrigin = IsPressed ? new Point(0.75,0.75) :
                                               new Point(0.25,0.25);

            dc.DrawRoundedRectangle(brush, 
                new Pen(SystemColors.ControlDarkDarkBrush, 1),
                new Rect(new Point(0,0), RenderSize),
                    RenderSize.Height / 2, RenderSize.Height / 2);
        }
    }
}
