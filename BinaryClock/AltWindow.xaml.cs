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
    /// Interaction logic for AltWindow.xaml
    /// </summary>
    public partial class AltWindow : Window
    {
        DispatcherTimer dTimer;
        DateTime time;
        Rectangle[] hours;
        Rectangle[] hoursB;
        Rectangle[] minutes;
        Rectangle[] minutesB;
        Rectangle[] seconds;
        Rectangle[] secondsB;

        public AltWindow()
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
            };

            hoursB = new Rectangle[]
            {
                hb10,
                hb20
            };
        }

        private void setMinutesTable()
        {
            minutes = new Rectangle[]
            {
                mb01,
                mb02,
                mb04,
                mb08,
            };

            minutesB = new Rectangle[]
            {
                mb10,
                mb20,
                mb40
            };
        }
        private void setSecondsTable()
        {
            seconds = new Rectangle[]
            {
                sb01,
                sb02,
                sb04,
                sb08
            };

            secondsB = new Rectangle[]
            {
                sb10,
                sb20,
                sb40
            };
        }
        #endregion

        private void Window_Unloaded(object Sender, EventArgs e)
        {
            dTimer.Stop();
        }

        private void Window_Loaded(object Sender, EventArgs e)
        {
            setHoursTable();
            setMinutesTable();
            setSecondsTable();

            time = DateTime.Now;

            dTimer = new DispatcherTimer();
            dTimer.Interval = new TimeSpan(0, 0, 1);
            dTimer.Tick += DTimer_Tick;
            dTimer.Start();

            setTime(time.Hour % 10, hours);
            setTime(time.Hour / 10, hoursB);
            setTime(time.Minute % 10, minutes);
            setTime(time.Minute / 10, minutesB);
            setTime(time.Second % 10, seconds);
            setTime(time.Second / 10, secondsB);
        }

        private void DTimer_Tick(object sender, EventArgs e)
        {
            var currentTime = DateTime.Now;

            setTime(currentTime.Second % 10, seconds);
            setTime(currentTime.Second / 10, secondsB);
            if (currentTime.Minute != time.Minute)
            {
                setTime(currentTime.Minute % 10, minutes);
                setTime(currentTime.Minute / 10, minutesB);
            }
            if (currentTime.Hour != time.Hour)
            {
                setTime(currentTime.Hour % 10, hours);
                setTime(currentTime.Hour / 10, hoursB);
            }
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

        private void ViewChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    }
}
