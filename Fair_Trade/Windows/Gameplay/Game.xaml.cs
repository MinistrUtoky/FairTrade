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
using Fair_Trade.GameClasses.GameBase.BasicCardMechanics;
using System.Xml.Linq;
using System.Runtime.InteropServices;

namespace Fair_Trade
{
    public partial class Game : Window
    {
        private CardGame _gameScene;
        private bool _collidersAreVisible = true;
        public bool downloadingFinished = false;
        private Button _backToMainMenu;
        private Button _endTurn;
        private bool _dragStarted = false;
        private GameObject2D _draggedObject;
        public Vector2 lastMousePostition = Vector2.zero;
        public Game()
        {
            InitializeComponent();
            GameMode.FormatWindow(this);
            AwaitDownloading(); 
            _backToMainMenu = new Button { Content = "Leave", Width = 50, Height = 50 }; _backToMainMenu.SetValue(Canvas.ZIndexProperty, 3);
            _backToMainMenu.Click += Exit; mainCanvas.Children.Add(_backToMainMenu);
            _endTurn = new Button { Content = "End Turn", Width = 50, Height = 50 }; _backToMainMenu.SetValue(Canvas.ZIndexProperty, 3);
            _endTurn.Click += EndTurn; mainCanvas.Children.Add(_endTurn); _endTurn.SetValue(Canvas.TopProperty, Height - 50);

            this.MouseLeftButtonDown += MouseDownEvent;
            this.MouseLeftButtonUp += MouseUpEvent;

            Download_Page d = new Download_Page(); d.Show();
            d.SetGameWindow(this);
            _gameScene = new CardGame(this);
            _gameScene.GenerateScene();
        }

        private async void AwaitDownloading()
        {
            await Task.Run(() =>
            {
                while (!downloadingFinished) { }
                _gameScene.StartSceneRoutines();
            });
        }

        public void StartGameRoutines()
        {
            downloadingFinished = true;
        }


        public void Display()
        {
            mainCanvas.Children.Remove(_backToMainMenu);
            mainCanvas.Children.Remove(_endTurn);
            mainCanvas.Children.Clear();
            foreach (GameObject2D gameObject in _gameScene._objectsInScene)
            {               
                Draw(gameObject);
                DrawCollider(gameObject);
                PlaySound(gameObject);
                if (_dragStarted) lastMousePostition = Vector2.PointToVector(Mouse.GetPosition(this));
            }
            mainCanvas.Children.Add(_backToMainMenu);
            mainCanvas.Children.Add(_endTurn);
        }
        private void Draw(GameObject2D gameObject)
        {
            if (gameObject.objectType == GameObject2D.GameObjectType.Visible)
            {
                Image visualObj = gameObject.Sprite; visualObj.SetValue(Canvas.ZIndexProperty, 1);
                if (visualObj != null)
                {
                    visualObj.SetValue(Canvas.TopProperty, (double)-gameObject.Position().y);
                    visualObj.SetValue(Canvas.LeftProperty, (double)gameObject.Position().x); 
                    ScaleTransform st = new ScaleTransform(gameObject.Size().x / visualObj.ActualWidth, gameObject.Size().y / visualObj.ActualHeight);
                    RotateTransform rt = new RotateTransform(-gameObject.Rotation());
                    rt.CenterX = (gameObject.Pivot().x - gameObject.Position().x); rt.CenterY = (-gameObject.Pivot().y + gameObject.Position().y);
                    TransformGroup tg = new TransformGroup(); tg.Children.Add(st); tg.Children.Add(rt);                 
                    visualObj.RenderTransform = tg;
                    mainCanvas.Children.Add(visualObj);
                }
            }
        }

        private void DrawCollider(GameObject2D gameObject)
        {
            if (_collidersAreVisible)
                if (gameObject.collider != null)
                {
                    System.Windows.Shapes.Rectangle r = gameObject.collider.Borderline(); r.SetValue(Canvas.ZIndexProperty, 1);
                    r.SetValue(Canvas.TopProperty, (double)-gameObject.collider.Position().y);
                    r.SetValue(Canvas.LeftProperty, (double)gameObject.collider.Position().x);
                    RotateTransform rt = new RotateTransform(-gameObject.collider.Rotation());
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
                    gameObject.AudioSource.Stop();
                }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            _gameScene.StopSceneRoutines();
            MainWindow mw = new MainWindow(); mw.Show();
            Close();
        }
        private void EndTurn(object sender, RoutedEventArgs e)
        {
            _gameScene.CurrentPlayer.EndTurn();
        }
        private void Download_Page_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _gameScene.StopSceneRoutines();
        }

        private void MouseDownEvent(object sender, MouseButtonEventArgs e)
        {
            if (!_dragStarted)
            {
                lastMousePostition = Vector2.PointToVector(Mouse.GetPosition(this));
                GameObject2D cursor = new GameObject2D(_gameScene, lastMousePostition, new Vector2(0.001f, 0.001f));
                cursor.AssignFastBoxCollider();

                for (int i = _gameScene._objectsInScene.Count-1; i > -1; i--)
                {
                    GameObject2D obj = _gameScene._objectsInScene[i]; 
                    if (obj.collider != null)
                        if (obj.IsDraggable)
                        {
                            if (obj.GetType() == typeof(Card))
                                if ((obj as Card).CanBePlayed && (obj as Card).Owner == _gameScene.CurrentPlayer)
                                    if (obj.collider.CheckOnIntersectionWith(cursor.collider))
                                    {
                                        //throw new Exception(click.Position().ToString() + " " + new Vector2(_gameScene.SceneViewerWidth() * 0.2f + 3 * _gameScene.SceneViewerWidth() * 0.05f, (1f - 0.17f * (float)Math.Abs(2f - 3)) * (_gameScene.SceneViewerHeight() * 2f / 13f + _gameScene.SceneViewerWidth() * 1f / 29f) - _gameScene.SceneViewerHeight()));
                                        //List<Vector2> d = obj.collider.GetDimensions(); List<Vector2> cd = cursor.collider.GetDimensions();
                                        //throw new Exception(d[0] + " ; " + d[1] + " ; " + d[2] + " ; " + d[3] + " !!! " + cd[0] + " ; " + cd[1] + " ; " + cd[2] + " ; " + cd[3]);
                                        _draggedObject = obj;                                       
                                        obj.StartDragging();
                                        obj.AngleToZero();
                                        _dragStarted = true;
                                        break;
                                    };
                        }
                }
            }
        }


        private void MouseUpEvent(object sender, MouseButtonEventArgs e)
        {
            if (_dragStarted)
            {
                _dragStarted = false;
                GameObject2D cursor = new GameObject2D(_gameScene, lastMousePostition, new Vector2(0.001f, 0.001f));
                cursor.AssignFastBoxCollider();

                for (int i = _gameScene._objectsInScene.Count - 1; i > -1; i--)
                {
                    GameObject2D obj = _gameScene._objectsInScene[i];
                    if (obj.collider != null)
                        {
                        if (_draggedObject.GetType() == typeof(Card))
                            if (obj.GetType() == typeof(CardField))
                                if ((obj as CardField).Owner == _gameScene.CurrentPlayer)
                                    if (obj.collider.CheckOnIntersectionWith(cursor.collider))
                                    {
                                        _draggedObject.ChangeDragEndPosition(obj.Position());
                                        (_draggedObject as Card).Disable();
                                        _gameScene.CurrentPlayer.PlayCardFromHand(_draggedObject as Card);
                                        _dragStarted = false;
                                        break;
                                    };
                            }
                    }
                    _draggedObject.StopDragging();
                _draggedObject.AngleToZero();
            }
        }
    }
}
