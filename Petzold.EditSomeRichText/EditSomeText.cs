using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

namespace Petzold.EditSomeRichText
{
    public class EditSomeText : Window
    {
        private RichTextBox txtBox;

        string strFilter =
            "Documet Files(*.xaml)|*.xaml|All files (*.*)|*.*";

        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new EditSomeText());
        }

        public EditSomeText()
        {
            Title = "Edit Some Rich Text";
            txtBox = new RichTextBox();
            txtBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            Content = txtBox;
            txtBox.Focus();

        }

        protected override void OnPreviewTextInput(
            TextCompositionEventArgs e)
        {
            //base.OnPreviewTextInput(e);

            if (e.ControlText.Length > 0 &&
                e.ControlText[0] == '\x0F')
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.CheckFileExists = true;
                dlg.Filter = strFilter;

                if ((bool) dlg.ShowDialog(this))
                {
                    FlowDocument flow = txtBox.Document;
                    TextRange range = new TextRange(flow.ContentStart,
                                                    flow.ContentEnd);
                    Stream strm = null;
                    try
                    {
                        strm = new FileStream(dlg.FileName, FileMode.Open);
                        range.Load(strm, DataFormats.Xaml);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Title);
                    }
                    finally
                    {
                        if(strm != null)
                        strm.Close();
                    }
                }

                e.Handled = true;
            }

            if (e.ControlText.Length > 0 &&
                e.ControlText[0] == '\x13')
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = strFilter;

                if ((bool)dlg.ShowDialog(this))
                {
                    FlowDocument flow = txtBox.Document;
                    TextRange range = new TextRange(flow.ContentStart,
                                                    flow.ContentEnd);
                    Stream strm = null;

                    try
                    {
                        strm = new FileStream(dlg.FileName, FileMode.Create);
                        range.Save(strm, DataFormats.Xaml);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Title);

                    }
                    finally
                    {
                        if(strm != null) strm.Close();
                    }
                }
                e.Handled = true;
            }
            base.OnPreviewTextInput(e);
        }
    }
}
