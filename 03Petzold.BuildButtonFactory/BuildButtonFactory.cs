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
            factoryContent.SetValue(ContentPresenter.MarginProperty, 
                new TemplateBindingExtension(Button.PaddingProperty));

            // Set ContentPresenter as child of Border object
            factoryBorder.AppendChild(factoryContent);

            // Border назначается корневым узлом визуального дерева
            template.VisualTree = factoryBorder;

            // Definititon trigger for condidion IsMouseOver = true
            Trigger trig = new Trigger();
            trig.Property = UIElement.IsMouseOverProperty;
            trig.Value = true;

            // Связывание объекта Setter с триггером
            // для изменения свойства CornerRadius элемента Border
            Setter set = new Setter();
            set.Property = Border.CornerRadiusProperty;
            set.Value = new CornerRadius(24);
            set.TargetName = "border";

            // Включение объекта Setter в коллекцию Setters триггера
            trig.Setters.Add(set);

            // Определение объекта Setter для изменения FontStyle
            // для свойства кнопки задавать TargetName не нужно
            set = new Setter();
            set.Property = Control.FontStyleProperty;
            set.Value = FontStyles.Italic;

            // Добавление в коллекцию Setters того же триггера
            trig.Setters.Add(set);

            //Включение триггера в шаблон
            template.Triggers.Add(trig);

            // Определение триггера для IsPressed
            trig = new Trigger();
            trig.Property = Button.IsPressedProperty;
            trig.Value = true;
            set = new Setter();
            set.Property = Border.BackgroundProperty;
            set.Value = SystemColors.ControlDarkBrush;
            set.TargetName = "border";
            
            // Включение объекта Setter в коллекцию Setters триггера
            trig.Setters.Add(set);

            // Включение триггера в шаблон
            template.Triggers.Add(trig);

            // Создание объекта Button
            Button btn = new Button();
            btn.Template = template;

            // Другие свойства определяются обычным образом
            btn.Content = "Button with Custom Template";
            btn.Padding = new Thickness(20);
            btn.FontSize = 48;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Click += ButtonOnClick;
            Content = btn;
        }

        private void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("You clicked the button", Title);
        }
    }
}
