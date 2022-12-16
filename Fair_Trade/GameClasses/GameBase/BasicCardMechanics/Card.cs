﻿using System;
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

        public Card(Scene parentalScene, Vector2 position, Vector2 size, int price, Player owner, bool canBePlayed=false, Image faceSprite=null): base(parentalScene, position, size)
        {
            _price = price;
            _owner = owner;
            _parentalScene = parentalScene;
            _canBePlayed = canBePlayed;
            _sprite = faceSprite;
            objectType = GameObjectType.Visible;
        }

        private void OnDrag()
        {

        }
    }
}
