using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace _03Petzold.BuildButtonFactory
{
    class BuildButtonFactory : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new BuildButtonFactory());
        }

        public BuildButtonFactory()
        {
            Title = "Build Button Factory";

            // Creating ControlTemplate obj for Button
            ControlTemplate template = new ControlTemplate(typeof(Button));

            // Creation FrameworkElementFactory for Border
            FrameworkElementFactory factoryBorder = 
                new FrameworkElementFactory(typeof(Border));

            // Set name for link
            factoryBorder.Name = "border";

            // Creation few base properties
            factoryBorder.SetValue(Border.BorderBrushProperty, Brushes.Red);
            factoryBorder.SetValue(Border.BorderThicknessProperty,
                                   new Thickness(3));
            factoryBorder.SetValue(Border.BackgroundProperty,
                SystemColors.ControlLightLightBrush);

            // Создание объекта FrameworkElementFactory for ContentPresenter
            FrameworkElementFactory factoryContent = 
                new FrameworkElementFactory(typeof(ContentPresenter));

            factoryContent.Name = "content";

            // Binding properties of ContentPresenter to props of Button
            factoryContent.SetValue(ContentPresenter.ContentProperty, 
                new TemplateBindingExtension(Button.ContentProperty));

            // Обратите внимание: свойство Padding кнопки
            // соответствует свойству Margin содержимого


        }

    }
}
