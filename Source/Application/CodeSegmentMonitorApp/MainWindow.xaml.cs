using CodeSegmentMonitorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodeSegmentMonitorApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {

        SegmentViewModel _vm = new SegmentViewModel();
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = _vm;

        }

        //private void SendMessageRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (this.SendDocument == null) return;

        //    //foreach (var item in this.SendDocument.Blocks)
        //    //{
        //        foreach (var c in SendDocument.Blocks.OfType<Paragraph>().SelectMany(b => b.Inlines))
        //        {
        //            Run r = c as Run;
        //            if (r == null) continue;
        //            Debug.WriteLine(r.Text);

        //        }
        //    //foreach (var key in spans.Keys)
        //    //    {
        //    //        var span = spans[key];
        //    //        var par = parasList[key];
        //    //        par.Inlines.Add(span);
        //    //        MessageDocument.Blocks.Add(par);
        //    //    }
        //    //}

          

        //    //Debug.WriteLine(this.SendDocument);
        //}
    }
}
