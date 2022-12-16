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
        private bool collidersAreVisible = true;
        private bool downloadingFinished = false;
        private bool downloadingShowOffFinished = false;
        public Game()
        {
            InitializeComponent();
            GameMode.FormatWindow(this);
           
            this.Dispatcher.BeginInvoke(() => { Download_Page d = new Download_Page(); d.Show(); });
            _gameScene = new CardGame(this);
            _gameScene.GenerateScene(); 
            while (!downloadingFinished || !downloadingShowOffFinished) { }
            _gameScene.StartSceneRoutines();

        }

        public void Display()
        {
            mainCanvas.Children.Clear();
            foreach (GameObject2D gameObject in _gameScene._objectsInScene)
                if (gameObject.objectType == GameObject2D.GameObjectType.Visible)
                {
                    Draw(gameObject);
                    DrawCollider(gameObject);
                    PlaySound(gameObject);
                }
        }
        private void Draw(GameObject2D gameObject)
        {
            Image visualObj = gameObject.Sprite;
            if (visualObj != null)
            {
                visualObj.SetValue(Canvas.TopProperty, (double)-gameObject.Position().y);
                visualObj.SetValue(Canvas.LeftProperty, (double)gameObject.Position().x);
                RotateTransform rt = new RotateTransform(gameObject.Rotation());
                rt.CenterX = (gameObject.Pivot().x - gameObject.Position().x); rt.CenterY = (-gameObject.Pivot().y + gameObject.Position().y);
                visualObj.RenderTransform = rt;
                mainCanvas.Children.Add(visualObj);
            }
        }

        private void DrawCollider(GameObject2D gameObject)
        {
            if (collidersAreVisible)
                if (gameObject.collider != null)
                {
                    System.Windows.Shapes.Rectangle r = gameObject.collider.Borderline();
                    r.SetValue(Canvas.TopProperty, (double)-gameObject.collider.Position().y);
                    r.SetValue(Canvas.LeftProperty, (double)gameObject.collider.Position().x);
                    RotateTransform rt = new RotateTransform(gameObject.collider.Rotation());
                    rt.CenterX = (gameObject.collider.Pivot().x - gameObject.collider.Position().x);
                    rt.CenterY = (-gameObject.collider.Pivot().y + gameObject.collider.Position().y);
                    r.RenderTransform = rt;
                    mainCanvas.Children.Add(r);
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
    }
}
