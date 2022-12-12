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
using System.Threading;
using Fair_Trade.GameClasses.Engine;
using System.IO;
using System.Windows.Threading;
using System.Diagnostics;
using static Fair_Trade.GameClasses.GameBase.DownloadingScene;
using System.Drawing;

namespace Fair_Trade
{
    public partial class Download_Page : Window
    {
        private DownloadingScene _downloadingScene;

        internal Download_Page()
        {
            InitializeComponent();
            GameMode.FormatWindow(this);
            _downloadingScene = new DownloadingScene(this);
            _downloadingScene.GenerateScene();
            _downloadingScene._pseudoCardToThrow.collider.AddVelocity(new Vector2(15, 15));
            _downloadingScene.StartSceneRoutines();
        }

        TextBlock tb = new TextBlock { Foreground = System.Windows.Media.Brushes.White, TextWrapping = TextWrapping.Wrap };

        public void Display()
        {
            downloadingPageCanvas.Children.Clear();
            tb.SetValue(Canvas.RightProperty, 0.0);
            tb.SetValue(Canvas.BottomProperty, 0.0);
            downloadingPageCanvas.Children.Add(tb);
            foreach (GameObject2D gameObject in _downloadingScene._objectsInScene)
                if (gameObject.objectType == GameObject2D.GameObjectType.Visible)
                {
                    Image visualObj = gameObject.GetSprite();
                    if (visualObj != null)
                    { 
                        visualObj.SetValue(Canvas.TopProperty, (double)-gameObject.Position().y);
                        visualObj.SetValue(Canvas.LeftProperty, (double)gameObject.Position().x);
                        //tb.Text = gameObject.collider.v.ToString();
                        downloadingPageCanvas.Children.Add(visualObj);
                    }
                }
        }

        public void PrintToTb(string s) => tb.Text = s;
        public void PrintToTb2(long s) => tb.Text = (s - Int64.Parse(tb.Text)).ToString();

        private void Download_Page_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _downloadingScene.StopSceneRoutines();
        }
    }
}