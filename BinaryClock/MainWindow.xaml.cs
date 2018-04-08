using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BinaryClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dTimer;
        DateTime time;
        Rectangle[] hours;
        Rectangle[] minutes;
        Rectangle[] seconds;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Tables Setting
        private void setHoursTable()
        {
            hours = new Rectangle[]
            {
                hb01,
                hb02,
                hb04,
                hb08,
                hb16
            };

            foreach (var b in hours)
            {
                b.Fill = Brushes.Snow;
                
            }
        }

        private void setMinutesTable()
        {
            minutes = new Rectangle[]
            {
                mb01,
                mb02,
                mb04,
                mb08,
                mb16,
                mb32
            };
            foreach (var b in minutes)
            {
                b.Fill = Brushes.Snow;
            }
        }
        private void setSecondsTable()
        {
            seconds = new Rectangle[]
            {
                sb01,
                sb02,
                sb04,
                sb08,
                sb16,
                sb32
            };
        }
        #endregion

        private void WindowUnloaded(object Sender, EventArgs e)
        {
            dTimer.Stop();
        }

        private void WindowLoaded(object Sender, EventArgs e)
        {
            setHoursTable();
            setMinutesTable();
            setSecondsTable();

            time = DateTime.Now;

            dTimer = new DispatcherTimer();
            dTimer.Interval = new TimeSpan(0, 0, 1);
            dTimer.Tick += DTimer_Tick;
            dTimer.Start();

            setTime(time.Hour, hours);
            setTime(time.Minute, minutes);
            setTime(time.Second, seconds);
        }

        private void DTimer_Tick(object sender, EventArgs e)
        {
            var currentTime = DateTime.Now;
            setTime(currentTime.Second, seconds);
            if (currentTime.Hour != time.Hour)
                setTime(currentTime.Hour, hours);
            if (currentTime.Minute != time.Minute)
                setTime(currentTime.Minute, minutes);
        }

        private void setTime(int cTime, Rectangle[] tab)
        {
            var list = new List<int>();

            while (cTime != 0)
            {
                list.Add(cTime % 2);
                cTime /= 2;
            }

            if (list.Count != tab.Length)
            {
                int dif = tab.Length - list.Count;
                for (int i = 0; i < dif; i++)
                {
                    list.Add(0);
                }
            }


            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == 1)
                    tab[i].Fill = Brushes.Red;
                else
                    tab[i].Fill = Brushes.Snow;
            }

        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
