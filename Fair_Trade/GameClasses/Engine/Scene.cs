using Fair_Trade.GameClasses.GameBase;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Media;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Windows.Media;
using static Fair_Trade.GameClasses.Engine.AudioSource;

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
        public float SceneViewerHeight() => -_sceneViewerCenter.y * 2;
        public float SceneViewerWidth() => _sceneViewerCenter.x * 2;

        protected virtual void Display() { }
        public virtual void GenerateScene() { }
        public virtual void SceneRoutines() { }

        public void AssignViewport(Window viewport) => _sceneViewer = viewport;


        public void InstantiateObject(GameObject2D obj)
        {
            _objectsInScene.Add(obj);
        }

        public static Image CreateSprite(string imgSrc)
        {
            Image imageSprite = new Image();
            BitmapImage bitmapImg = new BitmapImage(); bitmapImg.BeginInit();
            bitmapImg.UriSource = new Uri(imgSrc, UriKind.Relative); bitmapImg.EndInit();
            imageSprite.Stretch = System.Windows.Media.Stretch.None;
            imageSprite.Source = bitmapImg;
            return imageSprite;
        }

        public List<Image> CreateAnimation(string folderName)
        {
            string[] imageSources = Directory.GetFiles(
                Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName) + folderName,
                "*.png");
            if (imageSources.Length == 0) imageSources = Directory.GetFiles(folderName, "*.jpg", SearchOption.TopDirectoryOnly);
            List<Image> newAnimation = new List<Image>();
            foreach (string imageSource in imageSources) newAnimation.Add(CreateSprite(Path.Combine(folderName, Path.GetFileName(imageSource))));
            return newAnimation;
        }
        public static MediaPlayer CreateAudio(string audioSrc, AudioSource.AudioType audioType)
        {
            MediaPlayer mp = new MediaPlayer();
            Uri uri = new Uri(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName) + audioSrc);
            mp.Open(uri);
            if (audioType == AudioSource.AudioType.OneTime) mp.Volume = 0.5 * (double)GameMode.GetSFXLevel() / 100.0;
            else mp.Volume = 0.5 * (double)GameMode.GetMusicLevel() / 100.0;
            return mp;
        }
        
        
        public static void PlayBasicClickSound()
        {
            AudioSource basicClickSound = new AudioSource(AudioType.OneTime);
            basicClickSound.SetPlayer(CreateAudio("/Audio/Sounds/click.wav", AudioSource.AudioType.OneTime));
            basicClickSound.Play();
            basicClickSound.MediaPlayer.Play();
        }

        public static void PlayNotReadyPudge()
        {
            AudioSource notReadyElementPudge = new AudioSource(AudioType.OneTime);
            notReadyElementPudge.SetPlayer(CreateAudio("/Audio/Sounds/pudge.mpeg", AudioSource.AudioType.OneTime));
            notReadyElementPudge.Play();
            notReadyElementPudge.MediaPlayer.Play();
        }
    }
}
