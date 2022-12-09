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

        public Card(Vector2 position, Vector2 size, int price, Player owner, bool canBePlayed=false, Image faceSprite=null): base(position, size)
        {
            _price = price;
            _owner = owner;
            parentalScene = _owner._parentScene;
            _canBePlayed = canBePlayed;
            _sprite = faceSprite;
            objectType = typeof(Image);
        }

        private void OnDrag()
        {

        }
    }
}
