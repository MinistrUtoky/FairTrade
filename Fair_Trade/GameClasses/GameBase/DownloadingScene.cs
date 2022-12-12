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

namespace Fair_Trade.GameClasses.GameBase
{
    internal class DownloadingScene : Scene
    {
        private GameObject2D _firstCardThrower;
        private GameObject2D _secondCardThrower;
        public CardsBack _pseudoCardToThrow;
        private ThrowerAI _firstCardThrowerAI;
        private ThrowerAI _secondCardThrowerAI;

        public DownloadingScene(Window sceneViewer) : base(sceneViewer)
        {
            EnableGravity();
            _firstCardThrower = new GameObject2D(this, Vector2.zero, Vector2.zero);
            _secondCardThrower = new GameObject2D(this, Vector2.zero, Vector2.zero);
            _pseudoCardToThrow = new CardsBack(this, Vector2.zero, Vector2.zero);
            _pseudoCardToThrow.objectType = GameObject2D.GameObjectType.Visible;
            _firstCardThrower.objectType = GameObject2D.GameObjectType.Visible;
            _secondCardThrower.objectType = GameObject2D.GameObjectType.Visible;
            _pseudoCardToThrow.Resize(new Vector2(80, 120));
            _firstCardThrower.Resize(new Vector2(80, 120));
            _secondCardThrower.Resize(new Vector2(80, 120));
            _pseudoCardToThrow.MoveTo(_sceneViewerCenter + new Vector2(0, 200));
            _firstCardThrower.MoveTo(_sceneViewerCenter + new Vector2(-_firstCardThrower.Size().x / 2 - 200, _firstCardThrower.Size().y / 2));
            _secondCardThrower.MoveTo(_sceneViewerCenter + new Vector2(-_firstCardThrower.Size().x / 2 + 200, _firstCardThrower.Size().y / 2));
            _pseudoCardToThrow.SetSprite(CreateSprite("/Sprites/white_card.png"));
            _firstCardThrower.SetSprite(CreateSprite("/Sprites/white_card.png"));
            _secondCardThrower.SetSprite(CreateSprite("/Sprites/white_card.png"));
            _pseudoCardToThrow.AssignFastBoxCollider();
            _firstCardThrower.AssignFastBoxCollider();
            _secondCardThrower.AssignFastBoxCollider();
            _firstCardThrowerAI = new ThrowerAI(_firstCardThrower);
            _secondCardThrowerAI = new ThrowerAI(_secondCardThrower);
            _firstCardThrower.AssignAI(_firstCardThrowerAI);
            _secondCardThrower.AssignAI(_secondCardThrowerAI);
            _pseudoCardToThrow.collider.SetRBToDynamic();
        }

        public override void GenerateScene()
        {
            base.GenerateScene();
            InstantiateObject(_firstCardThrower);
            InstantiateObject(_secondCardThrower);
            InstantiateObject(_pseudoCardToThrow);
        }

        Stopwatch downloadingTimer = new Stopwatch();
        Stopwatch _frameRateCap = new Stopwatch();
        public void StartSceneRoutines()
        {
            _actionStarted = true;
            downloadingTimer.Start(); _frameRateCap.Start();
            (_sceneViewer as Download_Page).Dispatcher.BeginInvoke(DispatcherPriority.Background, () => Display());
            SceneRoutines();
            //Parallel.Invoke(
            //() => SceneRoutines()
            //() => (_firstCardThrower.AI as ThrowerAI).SearchForACard()
            //() => (_secondCardThrower.AI as ThrowerAI).SearchForACard()
            //);
            _firstCardThrowerAI.SearchForACard();
        }
        public void StopSceneRoutines()
        {
            _actionStarted = false;
        }

        protected override void Display()
        {
            //(_sceneViewer as Download_Page).PrintToTb(_frameRateCap.ElapsedMilliseconds.ToString());
            if (downloadingTimer.ElapsedMilliseconds > 15000 || !_actionStarted)
            {
                StopSceneRoutines();
                downloadingTimer.Stop();
                (_sceneViewer as Download_Page).Close();
                return;
            }  
            base.Display();
            (_sceneViewer as Download_Page).Display();
            //(_sceneViewer as Download_Page).PrintToTb2(_frameRateCap.ElapsedMilliseconds);
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
                        if (obj.collider != null)
                        {
                            obj.collider.AffectByGravity(_actionSpeedCoefficient / GameMode.maxFrameRate);
                            obj.collider.VelocityRoutine(_actionSpeedCoefficient / GameMode.maxFrameRate);
                        }
                }

                //Parallel.Invoke(() => SceneRoutines());
            });
        }

        
    }
}
