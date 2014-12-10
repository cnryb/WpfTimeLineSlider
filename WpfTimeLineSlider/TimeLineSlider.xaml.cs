using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Stopwatch stopwatch = new Stopwatch(); stopwatch.Start(); //  开始监视代码






            StartTime = DateTime.Parse("2014-12-10 10:00");
            EndTime = DateTime.Parse("2014-12-11 12:03");
            int i = 0;
            long scaleTimeTicks = StartTime.Ticks;

            const int interval = 6;

            TimeSpan ts = new TimeSpan(0, 0, 1);
            scaleTimeTicks += ts.Ticks;

            Scale scaleStart = new Scale();
            scaleStart.Width += 1.5;
            scaleStart.Height += 14;
            scaleStart.Background = new SolidColorBrush(Colors.Red);
            Canvas.SetLeft(scaleStart, interval * i);
            ScaleCanvas.Children.Add(scaleStart);

            while (scaleTimeTicks < EndTime.Ticks)
            {

                i++;

                Scale scale = new Scale();
                Canvas.SetLeft(scale, interval * i);
                ScaleCanvas.Children.Add(scale);

                TimeSpan showTimeSpan = new TimeSpan(scaleTimeTicks);



                //整分钟
                if (showTimeSpan.Seconds == 0)
                {
                    scale.Width += 0.1;
                    scale.Height += 5;

                }

                //整小时
                if (showTimeSpan.Minutes == 0)
                {
                    scale.Width += 0.1;
                    scale.Height += 5;
                }

                //整天
                if (showTimeSpan.Hours == 0)
                {
                    scale.Width += 0.1;
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



            stopwatch.Stop(); //  停止监视
            TimeSpan timeSpan = stopwatch.Elapsed; //  获取总时间
            TimeSpan totalTimeSpan = new TimeSpan(EndTime.Ticks - StartTime.Ticks);

            const string msg = "{0}小时 生成用时{1}";
            
            Console.WriteLine(msg,totalTimeSpan.TotalHours, timeSpan);

        }




        private Scale GenerateScale()
        {
            Scale scale = new Scale();


            return scale;
        }
    }
}