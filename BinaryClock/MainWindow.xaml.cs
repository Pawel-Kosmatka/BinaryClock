using System;
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
        DateTime currentTime;
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
            currentTime = DateTime.Now;
            setTime(currentTime.Second, seconds);
            if (currentTime.Hour != time.Hour)
            {
                setTime(currentTime.Hour, hours);
                time = currentTime;
            }
            if (currentTime.Minute != time.Minute)
            {
                setTime(currentTime.Minute, minutes);
                time = currentTime;
            }
        }

        private void setTime(int cTime, Rectangle[] tab)
        {
            for (int i = 0; i < tab.Length; ++i)
            {
                if (cTime%2 == 1)
                {
                    tab[i].Fill = Brushes.Red;
                }
                else
                {
                    tab[i].Fill = Brushes.Snow;
                }
                cTime /= 2;
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
            var nWindow = new AltWindow();
            nWindow.Show();
            this.Close();
        }
    }
}
