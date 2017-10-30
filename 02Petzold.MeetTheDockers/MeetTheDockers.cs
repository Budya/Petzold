using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _02Petzold.MeetTheDockers
{
    class MeetTheDockers : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new MeetTheDockers());
        }

        public MeetTheDockers()
        {
            Title = "Meet The Dockers";

            DockPanel dock = new DockPanel();
            Content = dock;

            // Creation Menu
            Menu menu = new Menu();
            MenuItem item = new MenuItem();
            item.Header = "Menu";
            menu.Items.Add(item);

            //Размещение меню  у верхнего края панели
            DockPanel.SetDock(menu, Dock.Top);
            dock.Children.Add(menu);

            // Создание панели инструментов
            ToolBar tool = new ToolBar();
            tool.Header = "Toolbar";

            // Размещение панели инструментов у верхнего края
            DockPanel.SetDock(tool, Dock.Top);
            dock.Children.Add(tool);

            //Создание строки состояния у нижнего края панели
            StatusBar status = new StatusBar();
            StatusBarItem statusBarItem = new StatusBarItem();
            statusBarItem.Content = "Status";
            status.Items.Add(statusBarItem);

            //Размещение строки состояния у нижнего края панели
            DockPanel.SetDock(status, Dock.Bottom);
            dock.Children.Add(status);

            // Создание списка
            ListBox list = new ListBox();
            list.Items.Add("List Box Item");

            // Размещение списка у левого края панели
            DockPanel.SetDock(list, Dock.Left);
            dock.Children.Add(list);

            // Создание текостового поля
            TextBox txtBox = new TextBox();
            txtBox.AcceptsReturn = true;

            // Размещение текстового поля и передача фокуса
            dock.Children.Add(txtBox);
            txtBox.Focus();
        }
    
    }
}
