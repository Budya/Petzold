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

        //Переопределение Vis

    }
}
