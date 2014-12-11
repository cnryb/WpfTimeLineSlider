using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfTimeLineSlider
{
    /// <summary>
    /// xaml 的交互逻辑
    /// </summary>
    public partial class ScaleCanvas
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public ScaleCanvas()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startPoint">开始点</param>
        /// <param name="endPoint">结束点</param>
        /// <param name="textBlockTop">文字距顶部距离</param>
        /// <param name="interval">刻度间距</param>
        public void GenerateScale(double startPoint, double endPoint, double textBlockTop = 15, int interval = 5)
        {
            Children.Clear();

            Stopwatch stopwatch = new Stopwatch(); stopwatch.Start(); //  开始监视代码

            int i = 0;
            long scaleTimeTicks = StartTime.Ticks;

            Rectangle startRec = new Rectangle
            {
                Width = 2,
                Height = 10,
                Fill = new SolidColorBrush(Colors.Red)
            };
            SetLeft(startRec, interval * i);
            Children.Add(startRec);

            var startTb = new TextBlock
            {
                Text = "开始\r\n" + StartTime.ToString("yyyy-MM-dd hh:mm"),
                Foreground = new SolidColorBrush(Colors.Red)
            };
            SetTop(startTb, textBlockTop);
            SetLeft(startTb, 2);
            Children.Add(startTb);
            

            TimeSpan ts = new TimeSpan(0, 0, 1);

            int num = 0;

            while (scaleTimeTicks < EndTime.Ticks)
            {
                i++;

                scaleTimeTicks += ts.Ticks;
                if (scaleTimeTicks >= EndTime.Ticks) break;
                double left = interval * i;
                if (!(startPoint < left) || !(endPoint > left)) continue;
                num++;

                Rectangle scale = new Rectangle
                {
                    Width = 1,
                    Height = 5
                };
                SetLeft(scale, left);

                TimeSpan showTimeSpan = new TimeSpan(scaleTimeTicks);

                //整分钟
                if (showTimeSpan.Seconds == 0)
                {
                    scale.Width += 0.1;
                    scale.Height += 5;

                    if (showTimeSpan.Minutes != 0)
                    {
                        var tb = new TextBlock
                        {
                            Text = new DateTime(showTimeSpan.Ticks).ToString("yyyy-MM-dd hh:mm")
                        };
                        SetTop(tb, textBlockTop);
                        SetLeft(tb, left - 2);
                        Children.Add(tb);
                    }
                }

                //整小时
                if (showTimeSpan.Minutes == 0 && showTimeSpan.Seconds == 0)
                {
                    scale.Width += 0.1;
                    scale.Height += 5;

                    if (showTimeSpan.Hours != 0)
                    {
                        var tb = new TextBlock
                        {
                            Text = new DateTime(showTimeSpan.Ticks).ToString("yyyy-MM-dd hh:mm")
                        };
                        SetTop(tb, textBlockTop);
                        SetLeft(tb, left - 2);
                        Children.Add(tb);
                    }
                }
                //整天
                if (showTimeSpan.Hours == 0 && showTimeSpan.Minutes == 0 && showTimeSpan.Seconds == 0)
                {
                    scale.Width += 0.1;
                    scale.Height += 5;

                    if (showTimeSpan.Days != 0)
                    {
                        var tb = new TextBlock
                        {
                            Text = new DateTime(showTimeSpan.Ticks).ToString("yyyy-MM-dd hh:mm")
                        };
                        SetTop(tb, textBlockTop);
                        SetLeft(tb, left - 2);
                        Children.Add(tb);
                    }
                }
                Children.Add(scale);
            }
            i++;
            Width = interval * i;

            Rectangle endRec = new Rectangle
            {
                Width = 2,
                Height = 10,
                Fill = new SolidColorBrush(Colors.Red)
            };
            SetLeft(endRec, interval * (i - 1));
            Children.Add(endRec);

            var endTb = new TextBlock
            {
                Text = "结束\r\n" + EndTime.ToString("yyyy-MM-dd hh:mm"),
                TextAlignment = TextAlignment.Right,
                Foreground = new SolidColorBrush(Colors.Red)
            };
            SetTop(endTb, textBlockTop);
            SetRight(endTb, 2);
            Children.Add(endTb);
           

            stopwatch.Stop(); //  停止监视
            TimeSpan timeSpan = stopwatch.Elapsed; //  获取总时间

            const string msg = "生成 {0} 个刻度 生成用时{1}";

            Console.WriteLine(msg, num, timeSpan);
        }
    }
}
