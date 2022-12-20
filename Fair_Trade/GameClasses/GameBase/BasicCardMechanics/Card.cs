using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Fair_Trade.GameClasses.Engine;
using Fair_Trade.GameClasses.GameBase;

namespace Fair_Trade.GameClasses.GameBase.BasicCardMechanics
{
    internal class Card : GameObject2D
    {
        protected int _price;
        protected Player _owner;
        protected bool _canBePlayed;
        
        public bool CanBePlayed { get { return _canBePlayed; } }
        public Player Owner { get { return _owner; } }

        public Card(Scene parentalScene, Vector2 position, Vector2 size, int price, Player owner, bool canBePlayed=false, Image faceSprite=null): base(parentalScene, position, size)
        {
            _price = price;
            _owner = owner;
            _canBePlayed = canBePlayed;
            _sprite = faceSprite;
            objectType = GameObjectType.Visible;
            EnableDragging();
        }

        public void OwnedBy(Player byWhom) => _owner = byWhom;
        public void Enable() => _canBePlayed = true;
        public void Disable() => _canBePlayed = false;

       
    }
}
