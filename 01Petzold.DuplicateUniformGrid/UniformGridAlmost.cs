using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;


namespace _01Petzold.DuplicateUniformGrid
{
    class UniformGridAlmost : Panel
    {
        // Public static dependecy prop only read
        public static readonly DependencyProperty ColumnsProperty;

        // Static constructor for depProp
        static  UniformGridAlmost()
        {
            ColumnsProperty =
                DependencyProperty.Register(
                    "Columns", typeof (int), typeof (UniformGridAlmost),
                    new FrameworkPropertyMetadata(1,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        // Property COLUMN
        public int Columns
        {
            set {SetValue(ColumnsProperty, value);}
            get { return (int) GetValue(ColumnsProperty); }
        }

        // Proerty ROWs readonly
        public int Rows
        {
            get { return (InternalChildren.Count + Columns - 1)/Columns; }
        }

        // Overriding MeasureOverride (распределяет место)
        protected override Size  MeasureOverride(Size sizeAvailable)
        {
            // Calculate size child's element
            Size sizeChild = new Size(sizeAvailable.Width / Columns, 
                sizeAvailable.Height / Rows);


            // Variables for maximums of width & heights storing
            double maxwidth = 0;
            double maxheight = 0;
            foreach (UIElement child in InternalChildren)
            {
                // Call Measure for every child object
                child.Measure(sizeChild);

                // ... then check property DesiredSize
                // of child object
                maxwidth = Math.Max(maxwidth, child.DesiredSize.Width);
                maxheight = Math.Max(maxheight, child.DesiredSize.Height);
            }

            // Now calculating desiring size for (размер решетки)
            return new Size(Columns * maxwidth, Rows * maxheight);
            //return base.MeasureOverride(sizeAvailable);
        }

        // Overriding ArrangeOverride - organize childs objects
        protected override Size  ArrangeOverride(Size sizeFinal)
        {
            // Calculate size of child objects
            // for rows & colls with same size
            Size sizeChild = new Size(sizeFinal.Width / Columns, 
                sizeFinal.Height / Rows);
            for (int index = 0; index < InternalChildren.Count; index++)
            {
                int row = index / Columns;
                int col = index % Columns;

                // Calcualating size for rectangle for every child 
                // object inside of "sizeFinal..."
                Rect rectChild = new Rect(new Point(col * sizeChild.Width, 
                    row * sizeChild.Height),
                    sizeChild);
                // ... and call Arrange for it object
                InternalChildren[index].Arrange(rectChild);
            }

            return sizeFinal;
        }

    }
}
