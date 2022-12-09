using Fair_Trade.GameClasses;
using Fair_Trade.GameClasses.GameBase;
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
using Fair_Trade.GameClasses.Engine;

namespace Fair_Trade
{
    public partial class Download_Page : Window
    {
        private DownloadingScene _downloadingScene;
        internal Download_Page()
        {
            InitializeComponent();
            GameMode.FormatWindow(this);
            _downloadingScene = new DownloadingScene();
            _downloadingScene.GenerateScene();
        }

        public void Display()
        {
            foreach (GameObject2D gameObject in _downloadingScene._objectsInScene)
                if (gameObject.objectType != null)
                    if (gameObject.objectType == typeof(Image))
                    {

                    }
        }
    }
}
