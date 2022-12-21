using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Diagnostics;
using Fair_Trade.GameClasses.Engine;
using Fair_Trade.GameClasses.GameBase.BasicCardMechanics;
using Fair_Trade.GameClasses.GameBase.AIs;
using Microsoft.VisualBasic;
using System.Threading;
using System.Windows.Media;

namespace Fair_Trade.GameClasses.GameBase
{
    internal class DownloadingScene : Scene
    {
        private GameObject2D _firstCardThrower;
        private GameObject2D _secondCardThrower;
        public CardsBack _pseudoCardToThrow;
        private GameObject2D _downloadCircle;
        private ThrowerAI _firstCardThrowerAI;
        private ThrowerAI _secondCardThrowerAI;
        private Stopwatch downloadingTimer = new Stopwatch();

        public DownloadingScene(Window sceneViewer) : base(sceneViewer)
        {
            EnableGravity();
            _firstCardThrower = new GameObject2D(this, Vector2.zero, Vector2.zero);
            _secondCardThrower = new GameObject2D(this, Vector2.zero, Vector2.zero);
            _pseudoCardToThrow = new CardsBack(this, Vector2.zero, Vector2.zero);
            _pseudoCardToThrow.objectType = GameObject2D.GameObjectType.Visible;
            _firstCardThrower.objectType = GameObject2D.GameObjectType.Visible;
            _secondCardThrower.objectType = GameObject2D.GameObjectType.Visible;
            _pseudoCardToThrow.Resize(new Vector2(83, 126));;
            _firstCardThrower.Resize(new Vector2(83, 126));
            _secondCardThrower.Resize(new Vector2(83, 126));

            _pseudoCardToThrow.MoveTo(_sceneViewerCenter + new Vector2(-_pseudoCardToThrow.Size().x / 2, 200));
            _firstCardThrower.MoveTo(_sceneViewerCenter + new Vector2(-_firstCardThrower.Size().x / 2 - 200, _firstCardThrower.Size().y / 2));
            _secondCardThrower.MoveTo(_sceneViewerCenter + new Vector2(-_secondCardThrower.Size().x / 2 + 200, _secondCardThrower.Size().y / 2));

            _pseudoCardToThrow.SetSprite(CreateSprite("/Sprites/white_card.png"));
            _firstCardThrower.SetSprite(CreateSprite("/Sprites/white_card.png"));
            _secondCardThrower.SetSprite(CreateSprite("/Sprites/white_card.png"));

            _pseudoCardToThrow.AssignFastBoxCollider();
            _firstCardThrower.AssignFastBoxCollider();
            _secondCardThrower.AssignFastBoxCollider();
            _pseudoCardToThrow.collider.SetRBToDynamic();
            _pseudoCardToThrow.SetRotationSpeed(-3f);

            _firstCardThrowerAI = new ThrowerAI(_firstCardThrower, _pseudoCardToThrow.collider, _secondCardThrower);
            _secondCardThrowerAI = new ThrowerAI(_secondCardThrower, _pseudoCardToThrow.collider, _firstCardThrower);
            _firstCardThrower.AssignAI(_firstCardThrowerAI);
            _secondCardThrower.AssignAI(_secondCardThrowerAI);

            
            _firstCardThrower.AssignAudioSource(new AudioSource(AudioSource.AudioType.OneTime));
            _secondCardThrower.AssignAudioSource(new AudioSource(AudioSource.AudioType.OneTime));
            _firstCardThrower.AudioSource.SetPlayer(CreateAudio("/Audio/Sounds/1.wav", AudioSource.AudioType.OneTime));
            _secondCardThrower.AudioSource.SetPlayer(CreateAudio("/Audio/Sounds/2.wav", AudioSource.AudioType.OneTime));

            _downloadCircle = new GameObject2D(this, Vector2.zero, Vector2.zero);
            _downloadCircle.objectType = GameObject2D.GameObjectType.Visible;
            _downloadCircle.Resize(new Vector2(50, 50));
            _downloadCircle.MoveTo(_sceneViewerCenter + new Vector2(-_downloadCircle.Size().x / 2, -100));
            _downloadCircle.SetSprite(CreateSprite("/Sprites/download_wheel_0.png"));
            _downloadCircle.AssignAnimation(CreateAnimation("/Sprites/download_wheel_animation/"));
            _downloadCircle.Animation.LoopAnimation();
            _downloadCircle.Animation.SetFrameRate(8);
        }

        public override void GenerateScene()
        {
            base.GenerateScene();
            InstantiateObject(_firstCardThrower);
            InstantiateObject(_secondCardThrower);
            InstantiateObject(_pseudoCardToThrow);
            InstantiateObject(_downloadCircle);
        }

        public void StartSceneRoutines()
        {
            _actionStarted = true;
            downloadingTimer.Restart();
            _downloadCircle.Animation.Play();
            (_sceneViewer as Download_Page).Dispatcher.BeginInvoke(DispatcherPriority.Background, () => Display());
            SceneRoutines();
            //Parallel.Invoke(
            //() => SceneRoutines()
            //() => (_firstCardThrower.AI as ThrowerAI).SearchForACard()
            //() => (_secondCardThrower.AI as ThrowerAI).SearchForACard()
            //);
            _firstCardThrowerAI.StartAIRoutine();
            _secondCardThrowerAI.StartAIRoutine();
        }
        public void StopSceneRoutines()
        {
            _actionStarted = false;
        }

        protected override void Display()
        {
            if (downloadingTimer.ElapsedMilliseconds > 15000 || !_actionStarted)
            {
                StopSceneRoutines();
                downloadingTimer.Stop();                
                (_sceneViewer as Download_Page).Close();
                (_sceneViewer as Download_Page).StartGameRoutines();
                return;
            }  
            base.Display();
            (_sceneViewer as Download_Page).Display();
            if (_actionStarted)
            {
                Task.Delay(1000 / GameMode.maxFrameRate).Wait();
                (_sceneViewer as Download_Page).Dispatcher.BeginInvoke(DispatcherPriority.Background, () => Display());
            }
        }

        public async void SceneRoutines()
        {
            await Task.Run(() =>
            {
                while (_actionStarted) { 
                    Task.Delay(1000 / GameMode.maxFrameRate).Wait();
                    if (!_actionStarted) return;
                    base.SceneRoutines();
                    foreach (GameObject2D obj in _objectsInScene)
                    {
                        if (obj.Animation != null)
                            obj.GoToNextFrameSprite();
                        if (obj.collider != null)
                        {
                            obj.collider.AffectByGravity(_actionSpeedCoefficient / GameMode.maxFrameRate);
                            obj.collider.VelocityRoutine(_actionSpeedCoefficient / GameMode.maxFrameRate);
                            _pseudoCardToThrow.Rotate(_pseudoCardToThrow.GetRotationSpeed());
                        }
                    }
                }
            });
        }

        
    }
}
