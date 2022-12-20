using Fair_Trade.GameClasses;
using Fair_Trade.GameClasses.Engine;
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

namespace Fair_Trade
{
    /// <summary>
    /// Логика взаимодействия для GameIntroPage.xaml
    /// </summary>
    public partial class GameIntroPage : Window
    {
        public GameIntroPage()
        {
            InitializeComponent();
            GameMode.FormatWindow(this);
        }
        private void GameStart(string gameMode)
        {
            GameMode.gameMode = gameMode;
            Game g = new Game();
            g.Show();
            this.Close();
        }
        private void Play_Online_Click(object sender, RoutedEventArgs e)
        {
            //GameStart("Multiplayer");
            //Scene.PlayBasicClickSound();
            Scene.PlayNotReadyPudge();
        }
        private void Play_With_Bots_Click(object sender, RoutedEventArgs e) {
            //GameStart("Bots");
            //Scene.PlayBasicClickSound();
            Scene.PlayNotReadyPudge();
        }
        private void Play_On_One_Screen_Click(object sender, RoutedEventArgs e)
        {
            Scene.PlayBasicClickSound();
            GameStart("OneScreen"); 
        }
        private void Tutorial_Click(object sender, RoutedEventArgs e) {
            //GameStart("Tutorial");
            //Scene.PlayBasicClickSound();
            Scene.PlayNotReadyPudge();
        }
    }
}
