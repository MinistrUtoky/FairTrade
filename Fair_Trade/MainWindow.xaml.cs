using Fair_Trade.GameClasses;
using Fair_Trade.GameClasses.Engine;
using Fair_Trade.Windows.Menu;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fair_Trade
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GameMode.UploadSettings();
            GameMode.FormatWindow(this);
            
        }

        private void Start_Game_Click(object sender, RoutedEventArgs e)
        {
            Scene.PlayBasicClickSound();
            GameIntroPage GIP = new GameIntroPage();
            GIP.Show();
            this.Close();
        }

        private void Shop_Click(object sender, RoutedEventArgs e)
        {
            Scene.PlayNotReadyPudge();
            //Scene.PlayBasicClickSound();
            //InGameShop shop = new InGameShop();
            //shop.Show();
            //this.Close();
        }
        private void Deck_Builder_Click(object sender, RoutedEventArgs e)
        {
            Scene.PlayNotReadyPudge();
            //Scene.PlayBasicClickSound();
            //DecksBuildPage shop = new DecksBuildPage();
            //shop.Show();
            //this.Close();
        }

        private void Game_Settings_Click(object sender, RoutedEventArgs e)
        {
            Scene.PlayBasicClickSound();
            GameSettings settings = new GameSettings();
            settings.Show();
            this.Close();
        }

        private void Game_Exit_Click(object sender, RoutedEventArgs e)
        {
            Scene.PlayBasicClickSound();
            this.Close();
        }
    }
}
