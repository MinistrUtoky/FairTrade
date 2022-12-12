using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using System.Threading.Tasks;
using Fair_Trade.GameClasses.Engine;
using Fair_Trade.GameClasses.GameBase;

namespace Fair_Trade.GameClasses.GameBase.BasicCardMechanics
{
    internal class UnitCard : Card
    {
        protected bool _isOnBoard;
        protected StatusEffect _statusEffect;

        public UnitCard(Scene parentalScene, Vector2 size, Vector2 topLeftPoint, Player owner, int price = 0, bool canBePlayed = true,
            Image faceSprite = null, bool isOnBoard= false, StatusEffect statusEffect=null): base(parentalScene ,topLeftPoint, size, price, owner, canBePlayed, faceSprite)
        {
            _isOnBoard = isOnBoard;
            _statusEffect = statusEffect;
        }

        private void MoveCardToBoard()
        {
            _statusEffect.EnactEffect();
        }
    }
}
