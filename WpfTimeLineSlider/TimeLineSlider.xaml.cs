using System;
using System.Collections.Generic;
using System.Text;
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

            StartTime = DateTime.Parse("2014-12-10 10:00");
            EndTime = DateTime.Parse("2014-12-14 12:03");
            int i = 0;
            long scaleTimeTicks = StartTime.Ticks;

            const int interval = 6;

            TimeSpan ts = new TimeSpan(1, 0, 0);
            scaleTimeTicks += ts.Ticks;

            Scale scaleStart = new Scale();
            scaleStart.Width += 1.5;
            scaleStart.Height += 14;
            scaleStart.Background = new SolidColorBrush(Colors.Red);
            Canvas.SetLeft(scaleStart, interval * i);
            ScaleCanvas.Children.Add(scaleStart);
            const long second = 10000 * 1000;
            const long minute = second * 60;
            const long hour = minute * 60;

            const long day = hour*24;

            while (scaleTimeTicks < EndTime.Ticks)
            {

                i++;

                Scale scale = new Scale();
                Canvas.SetLeft(scale, interval * i);
                ScaleCanvas.Children.Add(scale);

                TimeSpan showTimeSpan=new TimeSpan(scaleTimeTicks);

                

                //整分钟
                if (showTimeSpan.Seconds == 0)
                {
                    scale.Width += 0.8;
                    scale.Height += 5;

                }

                //整小时
                if (scaleTimeTicks % hour == 0 && ts.Ticks == minute)
                {
                    scale.Width += 0.8;
                    scale.Height += 5;
                }

                //整小时
                if (scaleTimeTicks % day == 0 && ts.Ticks == hour)
                {
                    scale.Width += 0.8;
                    scale.Height += 5;
                }




                scaleTimeTicks += ts.Ticks;
            }


            Scale scaleEnd = new Scale();
            scaleEnd.Width += 1.5;
            scaleEnd.Height += 14;
            scaleEnd.Background = new SolidColorBrush(Colors.Red);
            Canvas.SetLeft(scaleEnd, interval * i);
            ScaleCanvas.Children.Add(scaleEnd);

        }

        private Scale GenerateScale()
        {
            Scale scale = new Scale();


            return scale;
        }
    }
}