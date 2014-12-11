using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfTimeLineSlider
{
    /// <summary>
    /// TimeLineSlider.xaml 的交互逻辑
    /// </summary>
    public partial class TimeLineSlider : UserControl
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }


        public TimeLineSlider()
        {
            this.InitializeComponent();

            ScaleCanvas.StartTime = DateTime.Parse("2014-12-10 10:00");
            ScaleCanvas.EndTime = DateTime.Parse("2014-12-31 12:03");
        }


        public void GenerateScale(double startPoint, double endPoint)
        {
            ScaleCanvas.GenerateScale(startPoint, endPoint);
        }

    }
}