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
            StartTime = DateTime.Parse("2014-12-10 10:00");
            EndTime = DateTime.Parse("2014-12-31 12:03");
        }

        //void TimeLineSlider_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Stopwatch stopwatch = new Stopwatch(); stopwatch.Start(); //  开始监视代码






        //    StartTime = DateTime.Parse("2014-12-10 10:00");
        //    EndTime = DateTime.Parse("2014-12-11 12:03");
        //    int i = 0;
        //    long scaleTimeTicks = StartTime.Ticks;

        //    const int interval = 6;

        //    TimeSpan ts = new TimeSpan(0, 0, 1);
        //    scaleTimeTicks += ts.Ticks;



        //    List<Rectangle> rectangles = new List<Rectangle>();

        //    while (scaleTimeTicks < EndTime.Ticks)
        //    {
        //        i++;

        //        Rectangle scale = new Rectangle();
        //        scale.Width = 1;
        //        scale.Height = 5;
        //        //scale.Fill= new SolidColorBrush(Colors.Blue);
        //        Canvas.SetLeft(scale, interval * i);
        //        //ScaleCanvas.Children.Add(scale);
        //        rectangles.Add(scale);
        //        //Thread newWindowThread = new Thread(new ThreadStart(() =>
        //        //{
        //        //    Border border=new Border();
        //        //}));
        //        //newWindowThread.SetApartmentState(ApartmentState.STA);
        //        //newWindowThread.Start();

        //        //Path path=new Path();

        //        //Scale scale = new Scale();
        //        //Canvas.SetLeft(scale, interval * i);
        //        //ScaleCanvas.Children.Add(scale);

        //        TimeSpan showTimeSpan = new TimeSpan(scaleTimeTicks);



        //        //整分钟
        //        if (showTimeSpan.Seconds == 0)
        //        {
        //            scale.Width += 0.1;
        //            scale.Height += 5;

        //        }

        //        //整小时
        //        if (showTimeSpan.Minutes == 0)
        //        {
        //            scale.Width += 0.1;
        //            scale.Height += 5;
        //        }

        //        //整天
        //        if (showTimeSpan.Hours == 0)
        //        {
        //            scale.Width += 0.1;
        //            scale.Height += 5;
        //        }




        //        scaleTimeTicks += ts.Ticks;
        //    }



        //    ThreadPool.QueueUserWorkItem(callback =>
        //    {
        //        //foreach (Action action in rectangles.Select(rectangle => (Action) (() => ScaleCanvas.Children.Add(rectangle))))
        //        //{
        //        //    Dispatcher.Invoke(action);
        //        //}
        //        foreach (var rec in rectangles)
        //        {
        //            var rectangle = rec;
        //            //todo:在生成时判断会更好 判断用户能看到的画布的区域
        //            Func<double> func = () => Canvas.GetLeft(rectangle);
        //            object obj = Dispatcher.Invoke(func);
        //            double left = (double)obj;
        //            if (!ScaleCanvas.IsArrangeValid && left < ScaleCanvas.ActualWidth)
        //            {
        //                Dispatcher.Invoke((Action)(() => ScaleCanvas.Children.Add(rectangle)));
        //            }
        //            else if (left < ScaleCanvas.ActualWidth)
        //            {
        //                Dispatcher.Invoke((Action)(() => ScaleCanvas.Children.Add(rectangle)));
        //            }
        //        }
        //    });

        //    stopwatch.Stop(); //  停止监视
        //    TimeSpan timeSpan = stopwatch.Elapsed; //  获取总时间
        //    TimeSpan totalTimeSpan = new TimeSpan(EndTime.Ticks - StartTime.Ticks);

        //    const string msg = "{0}小时 生成用时{1}";

        //    Console.WriteLine(msg, totalTimeSpan.TotalHours, timeSpan);


        //}


        public void GenerateScale(double startPoint, double endPoint)
        {
            ScaleCanvas.Children.Clear();

            Stopwatch stopwatch = new Stopwatch(); stopwatch.Start(); //  开始监视代码

            int i = 0;
            long scaleTimeTicks = StartTime.Ticks;
            //文字距顶部距离
            double textBlockTop = 15;

            //刻度间距
            const int interval = 5;




            Rectangle startRec = new Rectangle
            {
                Width = 2,
                Height = 10,
                Fill = new SolidColorBrush(Colors.Red)
            };
            var startTb = new TextBlock();
            startTb.Text = "开始\r\n" + StartTime.ToString("yyyy-MM-dd hh:mm");
            startTb.Foreground = new SolidColorBrush(Colors.Red);
            Canvas.SetTop(startTb, textBlockTop);
            Canvas.SetLeft(startTb, 2);
            ScaleCanvas.Children.Add(startTb);
            Canvas.SetLeft(startRec, interval * i);
            ScaleCanvas.Children.Add(startRec);


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
                Canvas.SetLeft(scale, left);

                TimeSpan showTimeSpan = new TimeSpan(scaleTimeTicks);

                //整分钟
                if (showTimeSpan.Seconds == 0)
                {
                    scale.Width += 0.1;
                    scale.Height += 5;

                    if (showTimeSpan.Minutes != 0)
                    {
                        var tb = new TextBlock();
                        tb.Text = new DateTime(showTimeSpan.Ticks).ToString("yyyy-MM-dd hh:mm");
                        Canvas.SetTop(tb, textBlockTop);
                        Canvas.SetLeft(tb, left - 2);
                        ScaleCanvas.Children.Add(tb);
                    }
                }

                //整小时
                if (showTimeSpan.Minutes == 0 && showTimeSpan.Seconds == 0)
                {
                    scale.Width += 0.1;
                    scale.Height += 5;

                    if (showTimeSpan.Hours != 0)
                    {
                        var tb = new TextBlock();
                        tb.Text = new DateTime(showTimeSpan.Ticks).ToString("yyyy-MM-dd hh:mm");
                        Canvas.SetTop(tb, textBlockTop);
                        Canvas.SetLeft(tb, left - 2);
                        ScaleCanvas.Children.Add(tb);
                    }
                }
                //整天
                if (showTimeSpan.Hours == 0 && showTimeSpan.Minutes == 0 && showTimeSpan.Seconds == 0)
                {
                    scale.Width += 0.1;
                    scale.Height += 5;

                    if (showTimeSpan.Days != 0)
                    {
                        var tb = new TextBlock();
                        tb.Text = new DateTime(showTimeSpan.Ticks).ToString("yyyy-MM-dd hh:mm");
                        Canvas.SetTop(tb, textBlockTop);
                        Canvas.SetLeft(tb, left - 2);
                        ScaleCanvas.Children.Add(tb);
                    }
                }
                ScaleCanvas.Children.Add(scale);
            }
            i++;
            ScaleCanvas.Width = interval * i;

            Rectangle endRec = new Rectangle
            {
                Width = 2,
                Height = 10,
                Fill = new SolidColorBrush(Colors.Red)
            };
            var endTb = new TextBlock();
            endTb.Text = "结束\r\n" + EndTime.ToString("yyyy-MM-dd hh:mm");
            endTb.TextAlignment = TextAlignment.Right;
            endTb.Foreground = new SolidColorBrush(Colors.Red);
            Canvas.SetTop(endTb, textBlockTop);
            Canvas.SetRight(endTb, 2);
            ScaleCanvas.Children.Add(endTb);
            Canvas.SetLeft(endRec, interval * (i-1));
            ScaleCanvas.Children.Add(endRec);


            stopwatch.Stop(); //  停止监视
            TimeSpan timeSpan = stopwatch.Elapsed; //  获取总时间

            const string msg = "生成 {0} 个刻度 生成用时{1}";

            Console.WriteLine(msg, num, timeSpan);
        }

    }
}