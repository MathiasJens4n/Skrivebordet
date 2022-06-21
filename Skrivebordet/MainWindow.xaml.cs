using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Timers;
using System.Diagnostics;

namespace Skrivebordet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> urls = new List<string>();
        public const int SPI_SETDESKWALLPAPER = 20;
        public const int SPIF_UPDATEINIFILE = 1;
        public const int SPIF_SENDCHANGE = 2;
        int index = 0;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        public MainWindow()
        {
            InitializeComponent();
        }
        public void SetTimer(int time)
        {
            Timer timer = new Timer(time);
            timer.Elapsed += ChangeWallpaper;
            timer.AutoReset = true;
            timer.Start();
            
        }

        private void ChangeWallpaper(object sender, ElapsedEventArgs e)
        {
            if (index < urls.Count - 1)
            {
                index++;
            } else
            {
                index = 0;
            }

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, urls[index], SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);

            
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            urls.Remove(textBoxUrl.Text);
            textBoxUrl.Text = "";
            foreach (string tb in urls)
            {
                Debug.WriteLine(tb);
            }
        }

        private void buttonUpload_Click(object sender, RoutedEventArgs e)
        {
            urls.Add(textBoxUrl.Text);
            textBoxUrl.Text = "";
            foreach (string tb in urls)
            {
                Debug.WriteLine(tb);
            }
            
        }

        private void buttonHour_Click(object sender, RoutedEventArgs e)
        {
            SetTimer(10000);    
        }

        private void buttonDay_Click(object sender, RoutedEventArgs e)
        {
            SetTimer(86400000);
        }

        private void buttonWeek_Click(object sender, RoutedEventArgs e)
        {
            SetTimer(604800000);

        }
    }
}
