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
using Fair_Trade.GameClasses.GameBase;
using Fair_Trade.GameClasses.Engine;

namespace Fair_Trade
{
    public partial class Game : Window
    {
        private CardGame _gameScene;

        public Game()
        {
            InitializeComponent();
            GameMode.FormatWindow(this);
            _gameScene = new CardGame();
            _gameScene.GenerateScene();
        }
    }
}
