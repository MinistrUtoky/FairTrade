using System;
using System.Collections.Generic;
using System.Configuration;
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

        private List<Card> _cardsInDeck;
        private List<Card> _cardsInHand;
        private List<Card> _cardsOnBoard;
        private List<Card> _cardsInDump;
        private List<CardField> _mainFields;
        private List<CardField> _extraFields;
        public Player enemy;
        private int _handsCapacity;

        private int _health;
        private int _money;

        private Random _rand = new Random();

        public int HandsCapacity { get { return _handsCapacity; } }
        public int Health { get { return _health; } }
        public int Money { get { return _money; } }
        public List<Card> Hand { get { return _cardsInHand; } }
        public List<Card> Deck { get { return _cardsInDeck; } }
        public Player(Type playerType, CardGame parentScene, CardGame.PlayerClass newPlayerClass, List<Card> startDeck, List<CardField> mainFields, List<CardField> extraFields)
        {
            type = playerType;
            _parentScene = parentScene;
            if (newPlayerClass == null)
                playerClass = parentScene.firstPlayerDefaultClass;
            playerClass = newPlayerClass;
            _cardsInDeck = new List<Card>();
            _cardsInHand = new List<Card>();
            _cardsOnBoard = new List<Card>();
            _cardsInDump = new List<Card>();
            _mainFields = new List<CardField>();
            _extraFields = new List<CardField>();
            //_cardFields = new List<CardField>();
            _handsCapacity = playerClass._startHandsCapacity; _health = playerClass._startHealth;
            _money = playerClass._startMoney; 

            AssignFields(mainFields, extraFields);
            UploadDeck(startDeck);
            ShuffleDeck();
        }

        private void UploadDeck(List<Card> startDeck)
        {
            startDeck.ForEach(c => { _cardsInDeck.Add(c); c.OwnedBy(this); });
        }

        public void AssignFields(List<CardField> mainFields, List<CardField> extraFields) { mainFields.ForEach(f => { _mainFields.Add(f); f.OwnedBy(this); }); extraFields.ForEach(f => { _extraFields.Add(f); f.OwnedBy(this);  }); }

        public void ShuffleDeck()
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

        public void DrawCardsFromDeck(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _cardsInHand.Add(_cardsInDeck[0]);
                _cardsInDeck[0].Enable();
                _cardsInDeck.RemoveAt(0);
            }
        }

        public void PlayCardFromHand(Card card)
        {
            _cardsOnBoard.Add(card);
            _cardsInHand.Remove(card);
        }

        private void DumpCardFromBoard(Card card)
        {
            _cardsInDump.Add(card);
            _cardsOnBoard.Remove(card);
        }

        //public void CreateCardField(CardField cardField)
        //{
        //   _cardFields.Add(cardField);
        // }

        public void StartTurn()
        {
            _isPlayersTurn = true;
            _cardsInDeck.ForEach(card => {
                card.MoveTo(new Vector2(_parentScene.SceneViewerWidth()*123f/143f, _parentScene.SceneViewerHeight()*2f/13f + _parentScene.SceneViewerWidth() * 3f / 58f - _parentScene.SceneViewerHeight()));
                card.Rotate(90);
            });
            _cardsInDump.ForEach(card =>
            {
                card.MoveTo(new Vector2(_parentScene.SceneViewerWidth() * 102f / 143f, _parentScene.SceneViewerHeight() * 2f / 13f - _parentScene.SceneViewerHeight()));
                card.Rotate(90);
            });
            for (int i = 0; i < _handsCapacity; i++)
            {
                _cardsInHand[i].MoveTo(new Vector2(_parentScene.SceneViewerWidth() * 0.2f + i * _parentScene.SceneViewerWidth() * 0.05f, (1f - 0.17f*(float)Math.Abs(2f-i))*(_parentScene.SceneViewerHeight() * 2f / 13f + _parentScene.SceneViewerWidth() * 1f / 29f) - _parentScene.SceneViewerHeight()));
                _cardsInHand[i].Rotate(180 / _handsCapacity - i * 90 / _handsCapacity);
            }
            for (int i = 0; i < _cardsOnBoard.Count; i++)
            {
                _cardsOnBoard[i].MoveTo(new Vector2(_parentScene.SceneViewerWidth() * 0.05f + i * _parentScene.SceneViewerWidth() * 3f / 29f + i * _parentScene.SceneViewerWidth() * 3f / 145f,
                    _parentScene.SceneViewerHeight() * 11f / 21f - _parentScene.SceneViewerHeight()));
            }

            for (int i = 0; i < 5; i++)
            {
                _mainFields[i].MoveTo(new Vector2(_parentScene.SceneViewerWidth() * 0.05f + i * _parentScene.SceneViewerWidth() * 3f / 29f + i * _parentScene.SceneViewerWidth() * 3f / 145f,
                    _parentScene.SceneViewerHeight() * 11f / 21f - _parentScene.SceneViewerHeight()));
            }
            for (int i = 0; i < 2; i++)
            {
                _extraFields[i].MoveTo(new Vector2(_parentScene.SceneViewerWidth() * 0.7f + i * _parentScene.SceneViewerWidth() * 3f / 29f + i * _parentScene.SceneViewerWidth() * 3f / 145f,
                -_parentScene.SceneViewerHeight() * 2f / 13f - _parentScene.SceneViewerHeight() * 40f / 143f - _parentScene.SceneViewerHeight() * 1f / 39f));
            }

            //throw new Exception(_cardsInDeck.Count.ToString() + _cardsInDump.Count.ToString() + _cardsInHand.Count.ToString() + _cardsOnBoard.Count.ToString());

            enemy._cardsInDeck.ForEach(card => {
                card.MoveTo(new Vector2(_parentScene.SceneViewerWidth() * 123f / 143f, _parentScene.SceneViewerHeight() * 20f / 143f));
                card.Rotate(-90);
            });
            enemy._cardsInDump.ForEach(card =>
            {
                card.MoveTo(new Vector2(_parentScene.SceneViewerWidth() * 102f / 143f, _parentScene.SceneViewerHeight() * 20f / 143f));
                card.Rotate(-90);
            });
            for (int i = 0; i < enemy._handsCapacity; i++)
            {
                enemy._cardsInHand[i].MoveTo(new Vector2(_parentScene.SceneViewerWidth() * 0.2f + i * _parentScene.SceneViewerWidth() * 0.05f, (0.17f * (float)Math.Abs(2f - i) + 1) * (_parentScene.SceneViewerHeight() * 25f / 143f)));
                enemy._cardsInHand[i].Rotate(-180 / enemy._handsCapacity +  i * 90 / enemy._handsCapacity);
            }

            for (int i = 0; i < enemy._cardsOnBoard.Count; i++)
            {
                enemy._cardsOnBoard[i].MoveTo(new Vector2(_parentScene.SceneViewerWidth() * 0.05f + i * _parentScene.SceneViewerWidth() * 3f / 29f + i * _parentScene.SceneViewerWidth() * 3f / 145f,
                    -_parentScene.SceneViewerHeight() * 2f / 13f));
            }

            for (int i = 0; i < 5; i++)
            {
                enemy._mainFields[i].MoveTo(new Vector2(_parentScene.SceneViewerWidth() * 0.05f + i * _parentScene.SceneViewerWidth() * 3f / 29f + i * _parentScene.SceneViewerWidth() * 3f / 145f,
                    -_parentScene.SceneViewerHeight() * 2f / 13f));
            }
            for (int i = 0; i < 2; i++)
            {
                enemy._extraFields[i].MoveTo(new Vector2(_parentScene.SceneViewerWidth() * 0.7f + i * _parentScene.SceneViewerWidth() * 3f / 29f + i * _parentScene.SceneViewerWidth() * 3f / 145f,
                -_parentScene.SceneViewerHeight() * 2f / 13f));
            }
        }
        public void EndTurn()
        {
            if (enemy.type == Type.Human) {
                _cardsInDeck.ForEach(c => c.AngleToZero());
                _cardsInDump.ForEach(c => c.AngleToZero());
                _cardsInHand.ForEach(c => c.AngleToZero());
                _cardsOnBoard.ForEach(c => c.AngleToZero());
                enemy._cardsInDeck.ForEach(c => c.AngleToZero());
                enemy._cardsInHand.ForEach(c => c.AngleToZero());
                enemy._cardsInDump.ForEach(c => c.AngleToZero());
                enemy._cardsOnBoard.ForEach(c => c.AngleToZero());
            }
            DrawCardsFromDeck(_handsCapacity - _cardsInHand.Count);          
            _isPlayersTurn = false;
            _parentScene.StartAnotherPlayersTurn(enemy);            
        }
    }
}
