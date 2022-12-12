using Fair_Trade.GameClasses.GameBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Fair_Trade.GameClasses.Engine
{
    public class Scene
    {
        protected Window _sceneViewer;
        protected Vector2 _sceneViewerCenter;
        public readonly List<GameObject2D> _objectsInScene;
        protected static bool _gravityOn = false;
        protected static float _gravity = 10;
        protected bool _actionStarted = false;
        protected float _actionSpeedCoefficient = 6f;

        protected Scene(Window sceneViewer)
        {
            _sceneViewer = sceneViewer;
            _sceneViewerCenter = new Vector2(_sceneViewer.WindowState == WindowState.Maximized ? (float)System.Windows.SystemParameters.PrimaryScreenWidth / 2 : (float)_sceneViewer.Width / 2,
                _sceneViewer.WindowState == WindowState.Maximized ? -(float)System.Windows.SystemParameters.PrimaryScreenHeight / 2 : -(float)_sceneViewer.Height / 2);
            _objectsInScene = new List<GameObject2D>(); 
        }

        public void ChangeGravityTo(float g) => _gravity = g;
        public void EnableGravity() => _gravityOn = true;
        public void DisableGravity() => _gravityOn = false;
        public float GetGravity() => _gravityOn ? _gravity : 0;

        protected virtual void Display() { }
        public virtual void GenerateScene() { }
        public virtual void SceneRoutines() { }

        public void AssignViewport(Window viewport) => _sceneViewer = viewport;


        public void InstantiateObject(GameObject2D obj)
        {
            _objectsInScene.Add(obj);
        }

        protected Image CreateSprite(string imgSrc)
        {
            Image imageSprite = new Image(); BitmapImage
            bitmapImg = new BitmapImage(); bitmapImg.BeginInit();
            bitmapImg.UriSource = new Uri(imgSrc, UriKind.Relative); bitmapImg.EndInit();
            imageSprite.Stretch = System.Windows.Media.Stretch.None;
            imageSprite.Source = bitmapImg;
            return imageSprite;
        }

    }
}
