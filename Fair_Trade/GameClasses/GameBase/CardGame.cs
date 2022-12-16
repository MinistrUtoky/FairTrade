using Fair_Trade.GameClasses.GameBase.BasicCardMechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fair_Trade.GameClasses.Engine;
using System.Windows;
using System.Windows.Threading;

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
        public int _boardCapacity = 5;

        // TEMPORARY SUBSTITUDE!!! SHOULD BE REPLACED SOMEDAY :O
        private PlayerClass defaultClass = new Merchant();
        public List<Card> firstPlayerDefaultStartDeck = new List<Card>();
        public List<Card> secondPlayerDefaultStartDeck = new List<Card>();
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


        public CardGame(Window sceneViewer) : base(sceneViewer)
        {

        }


        public override void GenerateScene()
        {
            InstantiatePlayers();
            base.GenerateScene();
        }

        public void StartAnotherPlayersTurn(Player player)
        {
            player.StartTurn();
        }

        public void InstantiatePlayers()
        {
            // TEMPORARY!!!
            for (int i = 0; i < 30; i++)
            {
                Card card = new Card(this, Vector2.zero, new Vector2(83, 126), 0, firstPlayer);
                card.objectType = GameObject2D.GameObjectType.Visible;
                card.SetSprite(CreateSprite("/Sprites/white_card_sad.jpg"));
                firstPlayerDefaultStartDeck.Add(card);
            }
            for (int i = 0; i < 30; i++)
            {
                Card card = new Card(this, Vector2.zero, new Vector2(83, 126), 0, secondPlayer);
                card.objectType = GameObject2D.GameObjectType.Visible;
                card.SetSprite(CreateSprite("/Sprites/white_card_sad.jpg"));
                secondPlayerDefaultStartDeck.Add(card);
            }
            // TEMPORARY!!!
            firstPlayer = new Player(Player.Type.Human, this, defaultClass, firstPlayerDefaultStartDeck);
            if (GameMode.gameMode == "Multiplayer" || GameMode.gameMode == "OneScreen")
            {
                secondPlayer = new Player(Player.Type.Human, this, defaultClass, secondPlayerDefaultStartDeck);
            }
            else
            {
                secondPlayer = new Player(Player.Type.Bot, this, defaultClass, secondPlayerDefaultStartDeck);
            }
            firstPlayer.enemy = secondPlayer; secondPlayer.enemy = firstPlayer;
            firstPlayer.InstantiateAllCards(); secondPlayer.InstantiateAllCards();
        }

        public void StartSceneRoutines()
        {
            _actionStarted = true;
            (_sceneViewer as Game).Dispatcher.BeginInvoke(DispatcherPriority.Background, () => Display());
            SceneRoutines();
            //Parallel.Invoke(
            //() => SceneRoutines()
            //() => (_firstCardThrower.AI as ThrowerAI).SearchForACard()
            //() => (_secondCardThrower.AI as ThrowerAI).SearchForACard()
            //);
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
                            obj.collider.AffectByGravity(_actionSpeedCoefficient / GameMode.maxFrameRate);
                            obj.collider.VelocityRoutine(_actionSpeedCoefficient / GameMode.maxFrameRate);
                        }
                    }
                }
            });
        }
    }
}
