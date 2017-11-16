using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using _01Petzold.BetterEllipse;


namespace _01Petzold.EllipseWithChild
{
    public class EllipseWithChild : BetterEllipse.BetterEllipse
    {
        private UIElement child;

        // Public property Child
        public UIElement Child
        {
            set
            {
                if(child != null)
                {
                    RemoveVisualChild(child);
                    RemoveLogicalChild(child);
                }
                if((child = value) != null)
                {
                    AddVisualChild(child);
                    AddLogicalChild(child);
                }
            }

            get { return child; }
        }

        // Переопределение VisualChildrenCount возвращает 1,
        // если свойство Child отлично от null
        protected override int VisualChildrenCount
        {
            get { return Child != null ? 1 : 0; }
        }

        // Override GetVisualChildren returns Child
        protected override Visual GetVisualChild(int index)
        {
            //return base.GetVisualChild(index);
            if(index > 0 || Child == null)
                throw new ArgumentOutOfRangeException("index");
            return Child;
        }

        // Override MeasureOverride callback method
        // Measure child's object
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            //return base.MeasureOverride(sizeAvailable);
            Size sizeDesired = new Size(0,0);
            if (Stroke != null)
            {
                sizeDesired.Width += 2*Stroke.Thickness;
                sizeDesired.Height += 2*Stroke.Thickness;
                sizeAvailable.Width =
                    Math.Max(0, sizeAvailable.Height - 2*Stroke.Thickness);
                sizeAvailable.Height =
                    Math.Max(0, sizeAvailable.Height -
                                    2*Stroke.Thickness);
            }
            if(child != null)
            {
                Child.Measure(sizeAvailable);
                sizeDesired.Width += Child.DesiredSize.Width;
                sizeDesired.Height += Child.DesiredSize.Height;
            }
            return sizeDesired;
        }

        // Override ArrangeOverride who callback method
        // Arrange of child object
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            //return base.ArrangeOverride(sizeFinal);
            if (Child != null)
            {
                Rect rect = new Rect(
                    new Point((sizeFinal.Width - Child.DesiredSize.Width) / 2,
                        (sizeFinal.Height - Child.DesiredSize.Height) / 2),
                        Child.DesiredSize);
                Child.Arrange(rect);
            }
            return sizeFinal;
        }

    }
}
