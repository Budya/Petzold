using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _03Petzold.DiagonalizeThebuttons
{
    class DiagonalPanel : FrameworkElement
    {
        // Private collection of children elements
        List<UIElement> children = new List<UIElement>();

        //Private field
        private Size sizeChildrenTotal;

        //Dependency prperty
        public static readonly DependencyProperty BackgroundProperty;

        //Static constructor for creating
        //DepProp Background
        static DiagonalPanel()
        {
            BackgroundProperty =
                DependencyProperty.Register(
                    "Background", typeof (Brush), typeof (DiagonalPanel),
                    new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender));
        }

        //Property Background
        public Brush Background
        {
            set{SetValue(BackgroundProperty, value);}
            get { return (Brush) GetValue(BackgroundProperty); }
        }

        //Methods for manipulaton of child objects collection
        public void Add(UIElement el)
        {
            children.Add(el);
            AddVisualChild(el);
            AddLogicalChild(el);
            InvalidateMeasure();
        }
        public void Remove(UIElement el)
        {
            children.Remove(el);
            RemoveVisualChild(el);
            RemoveLogicalChild(el);
            InvalidateMeasure();
        }
        public int IndexOf(UIElement el)
        {
            return children.IndexOf(el);
        }

        // Overridden properties & methods
        protected override int VisualChildrenCount
        {
            get { return children.Count;}
        }
        protected override Visual GetVisualChild(int index)
        {
            if(index >= children.Count)
                throw new ArgumentOutOfRangeException("index");
            return children[index];
        }
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            sizeChildrenTotal = new Size(0, 0);
            foreach (UIElement child in children)
            {
                // Call Measure for every child ....
                child.Measure(new Size(Double.PositiveInfinity, 
                    Double.PositiveInfinity));
                // .... with following check of DesiredSize property
                sizeChildrenTotal.Width += child.DesiredSize.Width;
                sizeChildrenTotal.Height += child.DesiredSize.Height;
            }
            return sizeChildrenTotal;
        }

        protected override Size ArrangeOverride(Size sizeFinal)
        {
            Point ptChild = new Point(0, 0);
            foreach (UIElement child in children)
            {
                Size sizeChild = new Size(0, 0);
                sizeChild.Width = child.DesiredSize.Width*
                    (sizeFinal.Width/sizeChildrenTotal.Width);
                sizeChild.Height = child.DesiredSize.Height*
                    (sizeFinal.Height/sizeChildrenTotal.Height);
                child.Arrange(new Rect(ptChild, sizeChild));
                ptChild.X += sizeChild.Width;
                ptChild.Y += sizeChild.Height;
            }
            return sizeFinal;
        }
        protected override void OnRender(DrawingContext dc)
        {
            dc.DrawRectangle(Background, null,
                new Rect(new Point(0, 0), RenderSize));
        }
    }
}
