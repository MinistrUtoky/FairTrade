using Fair_Trade.GameClasses.GameBase.BasicCardMechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fair_Trade.GameClasses.Engine;
using System.Windows;

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
        public List<Card> defaultStartDeck = new List<Card>();
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


        public CardGame(Window sceneViewer) : base(sceneViewer)
        {
            // TEMPORARY!!!
            for (int i = 0; i < 30; i++);
            // TEMPORARY!!!
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
            firstPlayer = new Player(Player.Type.Human, this, defaultClass);
            if (GameMode.gameMode == "Multiplayer" || GameMode.gameMode == "OneScreen") { 
                secondPlayer = new Player(Player.Type.Human, this, defaultClass);
            }
            else {
                secondPlayer = new Player(Player.Type.Bot, this, defaultClass);
            }
            firstPlayer.enemy = secondPlayer; secondPlayer.enemy = firstPlayer;
            firstPlayer.InstantiateAllCards(); secondPlayer.InstantiateAllCards();
        }
    }
}
