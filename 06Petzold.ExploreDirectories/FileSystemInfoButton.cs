using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _06Petzold.ExploreDirectories
{
    public class FileSystemInfoButton : Button
    {
        FileSystemInfo info;
        // Непараметризированный конструктор создает кнопку
        // для каталога "Мои документы"

        public FileSystemInfoButton() 
            : this(new DirectoryInfo(
                Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments)))
        {}

        // Конструктор с одним аргументом создает кнопку
        // для каталога или файла

        public FileSystemInfoButton(FileSystemInfo info)
        {
            this.info = info;
            Content = info.Name;
            if (info is DirectoryInfo)
            {
                FontWeight = FontWeights.Bold;
            }
            Margin = new Thickness(10);
        }

        // Конструктор с двумя агрументами создает кнопку
        // родительского каталога

        public FileSystemInfoButton(FileSystemInfo info, string str) :
            this(info)
        {
            Content = str;
        }

        // Переопределение OnClick делает всё остальное

        protected override void OnClick()
        {
            if (info is FileInfo)
            {
                Process.Start(info.FullName);
            }
            else if(info is DirectoryInfo)
            {
                DirectoryInfo dir = info as DirectoryInfo;
                Application.Current.MainWindow.Title = dir.FullName;

                Panel pnl = Parent as Panel;
                pnl.Children.Clear();

                if (dir.Parent != null)
                    pnl.Children.Add(new FileSystemInfoButton(
                                         dir.Parent, ".."));
                foreach (FileSystemInfo inf in dir.GetFileSystemInfos())
                {
                    pnl.Children.Add(new FileSystemInfoButton(inf));
                }
                
                base.OnClick();
            }
            
        }
    }
}
