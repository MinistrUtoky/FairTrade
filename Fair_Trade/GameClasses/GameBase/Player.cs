using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fair_Trade.GameClasses.Engine;
using Fair_Trade.GameClasses.GameBase.BasicCardMechanics;

namespace Fair_Trade.GameClasses.GameBase
{
    internal class Player
    {
        public enum Type { Human, Bot }
        //public enum State { Turn, EnemyTurn }
        public Type type;
        public CardGame _parentScene;
        public CardGame.PlayerClass playerClass;
        private bool _isPlayersTurn;
        public List<Card> startDeck;

        private List<Card> _cardsInDeck;
        private List<Card> _cardsInHand;
        private List<Card> _cardsOnBoard;
        private List<Card> _cardsInDump;
        public Player enemy;
        private int _handsCapacity;

        private int _health;
        private int _money;

        private Random _rand = new Random();

        public Player(Type playerType, CardGame parentScene, CardGame.PlayerClass newPlayerClass)
        {
            // TEMPORARY!!!
            startDeck = parentScene.defaultStartDeck;
            // TEMPORARY!!!
            type = playerType;
            _parentScene = parentScene;
            if (newPlayerClass == null)
                playerClass = new CardGame.Merchant();
            playerClass = newPlayerClass;
            _cardsInDeck = new List<Card>();
            _cardsInHand = new List<Card>();
            _cardsOnBoard = new List<Card>();
            _cardsInDump = new List<Card>();
            _handsCapacity = playerClass._startHandsCapacity; _health = playerClass._startHealth;
            _money = playerClass._startMoney;
            UploadDeck();
            ShuffleDeck();
        }

        private void UploadDeck()
        {
            startDeck.ForEach(c => _cardsInDeck.Add(c));
        }

        private void ShuffleDeck()
        {
            for (int i = 0; i < _cardsInDeck.Count; i++)
            {
                int pos = _rand.Next(1, _cardsInDeck.Count());
                _cardsInDeck.Add(_cardsInDeck[pos]);
                _cardsInDeck.RemoveAt(pos);
            }
        }

        public void InstantiateAllCards()
        {
            _cardsInDeck.ForEach(c => _parentScene.InstantiateObject(c));
        }

        private void DrawCardsFromDeck(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _cardsInHand.Add(_cardsInDeck[0]);
                _cardsInDeck.RemoveAt(0);
            }
        }

        private void PlayCardFromHand(Card card)
        {
            _cardsOnBoard.Add(card);
            _cardsInHand.Remove(card);
        }

        private void DumpCardFromBoard(Card card)
        {
            _cardsInDump.Add(card);
            _cardsOnBoard.Remove(card);
        }

        public void StartTurn()
        {
            _isPlayersTurn = true;
        }
        private void EndTurn()
        {
            DrawCardsFromDeck(_handsCapacity - _cardsInHand.Count);
            _isPlayersTurn = false;
            _parentScene.StartAnotherPlayersTurn(enemy);
        }
    }
}
