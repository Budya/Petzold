﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;

namespace Petzold.ToggleBoldAndItalic
{
    class ToggleBoldAndItalic : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ToggleBoldAndItalic());

        }

        public ToggleBoldAndItalic()
        {
            Title = "Togle Bold And Italic";
            TextBlock text = new TextBlock();
            text.FontSize = 32;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            Content = text;

            string strQuote = "To be, or not to be, that is question";
            string[] strWords = strQuote.Split();

            foreach (var strWord in strWords)
            {
                Run run = new Run(strWord);
                run.MouseDown += RunOnMouseDown;
                text.Inlines.Add(run);
                text.Inlines.Add(" ");
            }

        }

        private void RunOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Run run = sender as Run;

            if (e.ChangedButton == MouseButton.Left)
            {
                run.FontStyle = run.FontStyle == FontStyles.Italic
                                    ? FontStyles.Normal
                                    : FontStyles.Italic;

            }

            if(e.ChangedButton == MouseButton.Right)
            {
                run.FontWeight = run.FontWeight == FontWeights.Bold
                    ? FontWeights.Normal : FontWeights.Bold;

            }
        }
    }
}
