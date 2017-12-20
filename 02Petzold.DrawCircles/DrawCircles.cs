using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _02Petzold.DrawCircles
{
    class DrawCircles : Window
    {
       // Canvas canv;

        // Fields for drawing
        bool isDrawing;
        Ellipse elips;
        Point ptCenter;

        // Fields for Dragging
        bool isDragging;
        FrameworkElement elDragging;
        Point ptMouseStart, ptElementStart;

        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new DrawCircles());
        }

        public DrawCircles ()
        {
            Title = "Draw Circles";
            Canvas canv = new Canvas();
            Content = canv;
        }

        protected override void OnMouseLeftButtonDown(
            MouseButtonEventArgs args)
        {
            base.OnMouseLeftButtonDown(args);

            if (isDragging)
            return;

            // Creating new Ellipse
            // and adding it to Canvas panel
            ptCenter = args.GetPosition(canv);
            elips = new Ellipse();
            elips.Stroke = SystemColors.WindowTextBrush;
            elips.StrokeThickness = 1;
            elips.Width = 0;
            elips.Height = 0;
            canv.Children.Add(elips);
            Canvas.SetLeft(elips, ptCenter.X);
            Canvas.SetTop(elips, ptCenter.Y);

            // Capture mouse and prepare for future events
            CaptureMouse();
            isDrawing = true;
        }

        protected override void OnMouseRightButtonDown(
            MouseButtonEventArgs args)
        {
            base.OnMouseRightButtonDown(args);

            if (isDrawing) return;

            // Get element, on which click was made
            // and prepare for future events
            ptMouseStart = args.GetPosition(canv);
            elDragging = canv.InputHitTest(ptMouseStart)
                         as FrameworkElement;
            if(elDragging != null)
            {
                ptElementStart = new Point(Canvas.GetLeft(elDragging),
                    Canvas.GetTop(elDragging));
                isDragging = true;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs args)
        {
            base.OnMouseDown(args);

            if(args.ChangedButton == MouseButton.Middle)
            {
                Shape shape = canv.InputHitTest(args.GetPosition(canv))
                              as Shape;
                if (shape != null)
                    shape.Fill = (shape.Fill == Brushes.Red
                                              ? Brushes.Transparent
                                              : Brushes.Red);
            }

        }

        protected override void OnMouseMove(MouseEventArgs args)
        {
            base.OnMouseMove(args);

            Point ptMouse = args.GetPosition(canv);

            // Dragging & resize ellips
            if(isDrawing)
            {
                double dRadius = Math.Sqrt(
                    Math.Pow(ptCenter.X - ptMouse.X, 2) +
                    Math.Pow(ptCenter.Y - ptMouse.Y, 2));
                Canvas.SetLeft(elips, ptCenter.X - dRadius);
                Canvas.SetTop(elips, ptCenter.Y - dRadius);
            }

            // Dragging elips
            else if (isDragging)
            {
                Canvas.SetLeft(elDragging,
                ptElementStart.X + ptMouse.X - ptMouse.X);
                Canvas.SetTop(elDragging, 
                ptElementStart.Y + ptMouse.Y - ptMouseStart.Y);
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs args)
        {
            base.OnMouseUp(args);

            //Ending drawing
            if (isDrawing && args.ChangedButton == MouseButton.Left)
            {
                elips.Stroke = Brushes.Blue;
                elips.StrokeThickness = Math.Min(24, elips.Width/2);
                elips.Fill = Brushes.Red;
                isDrawing = false;
                ReleaseMouseCapture();
            }
            else if (isDragging && args.ChangedButton == MouseButton.Right)
            {
                isDragging = false;
            }
        }

        protected override void OnTextInput(TextCompositionEventArgs args)
        {
            base.OnTextInput(args);

            // If ESCAPE pressed - ending drawing and dragging
            if (args.Text.IndexOf('\x1B') !=-1)
            {
                if(isDrawing)
                    ReleaseMouseCapture();
                else if (isDragging)
                {
                    Canvas.SetLeft(elDragging, ptElementStart.X);
                    Canvas.SetTop(elDragging, ptElementStart.Y);
                    isDragging = false;
                }
                
            }
        }
        protected override void OnLostMouseCapture(MouseEventArgs args)
        {
            base.OnLostMouseCapture(args);

            //Anomalic ending of drawing: deleting ellipse
            if (isDrawing)
            {
                canv.Children.Remove(elips);
                isDrawing = false;
            }
        }

        


    }
}
