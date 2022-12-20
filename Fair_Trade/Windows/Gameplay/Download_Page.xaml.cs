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
using Fair_Trade.GameClasses.GameBase.BasicCardMechanics;
using System.Windows.Media.Media3D;

namespace Fair_Trade
{
    public partial class Download_Page : Window
    {
        private DownloadingScene _downloadingScene;
        private Game _gameWindow;
        private bool _collidersAreVisible = false;
        internal Download_Page()
        {
            InitializeComponent();
            GameMode.FormatWindow(this);    
            _downloadingScene = new DownloadingScene(this);
            _downloadingScene.GenerateScene();
            _downloadingScene._pseudoCardToThrow.collider.AddVelocity(new Vector2(35, 0));
            _downloadingScene.StartSceneRoutines();
        }

        public void SetGameWindow(Game gameWindow) => _gameWindow = gameWindow;
        public void StartGameRoutines() => _gameWindow.StartGameRoutines();

        public void Display()
        {
            downloadingPageCanvas.Children.Clear();
            foreach (GameObject2D gameObject in _downloadingScene._objectsInScene)
            {
                Draw(gameObject);
                DrawCollider(gameObject);
                PlaySound(gameObject);
            }
        }
        private void Draw(GameObject2D gameObject)
        {   
            if (gameObject.objectType == GameObject2D.GameObjectType.Visible)
            {
                Image visualObj = gameObject.Sprite;
                if (visualObj != null)
                {
                    visualObj.SetValue(Canvas.TopProperty, (double)-gameObject.Position().y);
                    visualObj.SetValue(Canvas.LeftProperty, (double)gameObject.Position().x);
                    RotateTransform rt = new RotateTransform(-gameObject.Rotation());
                    rt.CenterX = (gameObject.Pivot().x - gameObject.Position().x); rt.CenterY = (-gameObject.Pivot().y + gameObject.Position().y);
                    visualObj.RenderTransform = rt;
                    downloadingPageCanvas.Children.Add(visualObj);                    
                }
            }
        }

        private void DrawCollider(GameObject2D gameObject)
        {
            if (_collidersAreVisible)
                if (gameObject.collider != null)
                {
                    System.Windows.Shapes.Rectangle r = gameObject.collider.Borderline();
                    r.SetValue(Canvas.TopProperty, (double)-gameObject.collider.Position().y);
                    r.SetValue(Canvas.LeftProperty, (double)gameObject.collider.Position().x);
                    RotateTransform rt = new RotateTransform(-gameObject.collider.Rotation());
                    rt.CenterX = (gameObject.collider.Pivot().x - gameObject.collider.Position().x); rt.CenterY = (-gameObject.collider.Pivot().y + gameObject.collider.Position().y);
                    r.RenderTransform = rt;
                    downloadingPageCanvas.Children.Add(r);
                }
        }

        private void PlaySound(GameObject2D gameObject)
        {
            if (gameObject.AudioSource != null)
                if (gameObject.AudioSource.isPlaying)
                {
                    gameObject.AudioSource.MediaPlayer.Play();
                    gameObject.AudioSource.isPlaying = false;
                }
        }

        private void Download_Page_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _downloadingScene.StopSceneRoutines();
        }
    }
}