using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace _04Petzold.DrawingVisual1.ColorCell
{
    class ColorCell : FrameworkElement
    {
        // Private fields
        static readonly  Size sizeCell = new Size(20, 20);
        private DrawingVisual visColor;
        private Brush brush;

        // DependencyProperties
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty IsHighlightedProperty;

        static ColorCell()
        {
            IsSelectedProperty =
                DependencyProperty.Register("IsSelected", typeof (bool),
                    typeof (ColorCell), new FrameworkPropertyMetadata(false,
                        FrameworkPropertyMetadataOptions.AffectsRender));

            IsHighlightedProperty =
                DependencyProperty.Register("IsHighlighted", typeof (bool),
                    typeof (ColorCell), new FrameworkPropertyMetadata(false,
                        FrameworkPropertyMetadataOptions.AffectsRender));
        }
        //Properties
        public bool IsSelected
        {
            set{SetValue(IsSelectedProperty, value);}
            get { return (bool) GetValue(IsSelectedProperty); }
        }
        public bool IsHighlighted
        {
            set { SetValue(IsHighlightedProperty, value); }
            get { return (bool)GetValue(IsHighlightedProperty); }
        }

        public Brush Brush
        {
            get { return brush; }
        }

        // Constructor takes argument Color
        public ColorCell(Color clr)
        {
            // Creation new DrawingVisusl object
            // and saving it in field
            visColor = new DrawingVisual();
            DrawingContext dc = visColor.RenderOpen();

            // Drawing rectangle 
            Rect rect = new Rect(new Point(0, 0), sizeCell);
            rect.Inflate(-4, -4);
            Pen pen = new Pen(SystemColors.ControlTextBrush, 1);
            brush = new SolidColorBrush(clr);
            dc.DrawRectangle(brush, pen, rect);
            dc.Close();
        }

        // Переопределение защищенных свойств и методов
        // для визуального дочернего объекта
        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }
        protected override Visual GetVisualChild(int index)
        {
            if (index > 0) throw new ArgumentOutOfRangeException("index");
            return visColor;
        }

        // Переопределение защищенных методов определения размеров
        // и воспроизведения элемента
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            return sizeCell;
        }
        protected override void OnRender(DrawingContext dc)
        {
            //base.OnRender(drawingContext);
            Rect rect = new Rect(new Point(0,0), RenderSize);
            rect.Inflate(-1,-1);
            Pen pen = new Pen(SystemColors.HighlightBrush, 1);
            if(IsHighlighted)
                dc.DrawRectangle(SystemColors.ControlDarkBrush, pen, rect);
            else if (IsSelected)
                dc.DrawRectangle(SystemColors.ControlLightBrush, pen, rect);
            else 
                dc.DrawRectangle(Brushes.Transparent, null, rect);
        }
    }
}
