using Fair_Trade.GameClasses;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Threading;

namespace Fair_Trade
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        public Game()
        {
            InitializeComponent();
            GameMode.FormatWindow(this);
            DownloadingAwaiter();
        }
        private async void DownloadingAwaiter()
        {
            await Task.Run(() => StartDownloading());
        }

        private void StartDownloading()
        {
            Download_Page dp = new Download_Page();
            dp.ShowDialog();
            Thread.Sleep(10000);
            dp.Close();
        }

    }
}
