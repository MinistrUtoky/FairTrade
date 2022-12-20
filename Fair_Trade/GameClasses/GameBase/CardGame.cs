using Fair_Trade.GameClasses.GameBase.BasicCardMechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fair_Trade.GameClasses.Engine;
using System.Windows;
using System.Windows.Threading;
using System.Security.RightsManagement;
using System.Windows.Input;

namespace Fair_Trade.GameClasses.GameBase
{
    internal class CardGame : Scene
    {
        public class PlayerClass { public int _startMoney; public int _startHealth; public int _startHandsCapacity; public static List<Card> _basicClassCards; }
        public class Merchant : PlayerClass { public Merchant() { _startMoney = 100; _startHealth = 30; _startHandsCapacity = 5; } }
        public class Fraud : PlayerClass { public Fraud() { _startMoney = 50; _startHealth = 30; _startHandsCapacity = 6; } }
        public class Borgouiese : PlayerClass { public Borgouiese() { _startMoney = 200; _startHealth = 40; _startHandsCapacity = 4; } }
        public class Usurer : PlayerClass { public Usurer() { _startMoney = 150; _startHealth = 30; _startHandsCapacity = 5; } }

        Player firstPlayer; Player secondPlayer;
        private Player _currentPlayer;
        public Player CurrentPlayer { get { return _currentPlayer; } }
        public int _boardCapacity = 5;

        // TEMPORARY SUBSTITUDE!!! SHOULD BE REPLACED SOMEDAY :O
        public PlayerClass firstPlayerDefaultClass = new Merchant();
        public PlayerClass secondPlayerDefaultClass = new Merchant();
        public List<Card> firstPlayerDefaultStartDeck = new List<Card>();
        public List<Card> secondPlayerDefaultStartDeck = new List<Card>();
        public List<CardField> firstPlayerCardMainFields = new List<CardField>();
        public List<CardField> secondPlayerCardMainFields = new List<CardField>();
        public List<CardField> firstPlayerCardExtraFields = new List<CardField>();
        public List<CardField> secondPlayerCardExtraFields = new List<CardField>();
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


        public CardGame(Window sceneViewer) : base(sceneViewer) { }


        public override void GenerateScene()
        {
            InstantiatePlayers();
            base.GenerateScene();
        }

        public void StartAnotherPlayersTurn(Player player)
        {
            _currentPlayer = player;
            player.StartTurn();
        }

        public void InstantiatePlayers()
        {
            // TEMPORARY!!!
            for (int i = 0; i < 30; i++)
            {
                Card card = new Card(this, Vector2.zero,
                    new Vector2(SceneViewerWidth() * 3f / 29f, SceneViewerHeight() * 40f / 143f), 0, firstPlayer);
                card.SetSprite(CreateSprite("/Sprites/white_card_sad.jpg"));
                card.AssignFastBoxCollider();
                firstPlayerDefaultStartDeck.Add(card);
            }
            for (int i = 0; i < 30; i++)
            {
                Card card = new Card(this, Vector2.zero,
                    new Vector2(SceneViewerWidth() * 3f / 29f, SceneViewerHeight() * 40f / 143f), 0, secondPlayer);
                card.SetSprite(CreateSprite("/Sprites/white_card.png"));
                card.AssignFastBoxCollider();
                secondPlayerDefaultStartDeck.Add(card);
            }
            for (int i = 0; i < 5; i++)
            {
                CardField cardField = new CardField(this, Vector2.zero,
                    new Vector2(SceneViewerWidth() * 3f / 29f, SceneViewerHeight() * 40f / 143f), firstPlayer);
                cardField.SetSprite(CreateSprite("/Sprites/white_card.png"));
                cardField.AssignFastBoxCollider();
                InstantiateObject(cardField);
                firstPlayerCardMainFields.Add(cardField);
            }
            for (int i = 0; i < 5; i++)
            {
                CardField cardField = new CardField(this, Vector2.zero,
                    new Vector2(SceneViewerWidth() * 3f / 29f, SceneViewerHeight() * 40f / 143f), secondPlayer);
                cardField.SetSprite(CreateSprite("/Sprites/white_card.png"));
                cardField.AssignFastBoxCollider();
                InstantiateObject(cardField);
                secondPlayerCardMainFields.Add(cardField);
            }
            for (int j = 0; j < 2; j++)
            {
                CardField cardField = new CardField(this, Vector2.zero,
                new Vector2(SceneViewerWidth() * 3f / 29f, SceneViewerHeight() * 40f / 143f), firstPlayer);
                cardField.SetSprite(CreateSprite("/Sprites/white_card.png"));
                cardField.AssignFastBoxCollider();
                InstantiateObject(cardField);
                firstPlayerCardExtraFields.Add(cardField);
            }
            for (int j = 0; j < 2; j++)
            {
                CardField cardField = new CardField(this, Vector2.zero,
                new Vector2(SceneViewerWidth() * 3f / 29f, SceneViewerHeight() * 40f / 143f), secondPlayer);
                cardField.SetSprite(CreateSprite("/Sprites/white_card.png"));
                cardField.AssignFastBoxCollider();
                InstantiateObject(cardField);
                secondPlayerCardExtraFields.Add(cardField);
            }

            // TEMPORARY!!!

            firstPlayer = new Player(Player.Type.Human, this, firstPlayerDefaultClass, firstPlayerDefaultStartDeck, firstPlayerCardMainFields, firstPlayerCardExtraFields);
            _currentPlayer = firstPlayer;
            if (GameMode.gameMode == "Multiplayer" || GameMode.gameMode == "OneScreen")
            {
                secondPlayer = new Player(Player.Type.Human, this, secondPlayerDefaultClass, secondPlayerDefaultStartDeck, secondPlayerCardMainFields, secondPlayerCardExtraFields);
            }
            else
            {
                secondPlayer = new Player(Player.Type.Bot, this, secondPlayerDefaultClass, secondPlayerDefaultStartDeck, secondPlayerCardMainFields, secondPlayerCardExtraFields);
            }
            firstPlayer.enemy = secondPlayer; secondPlayer.enemy = firstPlayer;
            
            firstPlayer.InstantiateAllCards(); secondPlayer.InstantiateAllCards();
            firstPlayer.DrawCardsFromDeck(firstPlayer.HandsCapacity);
            secondPlayer.DrawCardsFromDeck(secondPlayer.HandsCapacity);
        }

        public void StartSceneRoutines()
        {
            _actionStarted = true;
            _currentPlayer = firstPlayer;
            firstPlayer.StartTurn();
            (_sceneViewer as Game).Dispatcher.BeginInvoke(DispatcherPriority.Background, () => Display());
            Parallel.Invoke(() => SceneRoutines());
        }
        public void StopSceneRoutines()
        {
            _actionStarted = false;
        }

        protected override void Display()
        {
            base.Display();
            (_sceneViewer as Game).Display();
            if (_actionStarted)
            {
                Task.Delay(1000 / GameMode.maxFrameRate).Wait();
                (_sceneViewer as Game).Dispatcher.BeginInvoke(DispatcherPriority.Background, () => Display());
            }
        }

        public async void SceneRoutines()
        {
            await Task.Run(() =>
            {
                while (_actionStarted)
                {
                    Task.Delay(1000 / GameMode.maxFrameRate).Wait();
                    if (!_actionStarted) return;
                    base.SceneRoutines();
                    foreach (GameObject2D obj in _objectsInScene)
                    {
                        if (obj.Animation != null)
                            obj.GoToNextFrameSprite();
                        if (obj.collider != null)
                        {
                            if (obj.IsDragged)
                            {
                                obj.MoveTo((_sceneViewer as Game).lastMousePostition - (obj.Pivot() - obj.Position()));
                            }
                            else
                            {
                                obj.collider.AffectByGravity(_actionSpeedCoefficient / GameMode.maxFrameRate);
                                obj.collider.VelocityRoutine(_actionSpeedCoefficient / GameMode.maxFrameRate);
                            }
                        }
                    }
                }
            });
        }
    }
}
