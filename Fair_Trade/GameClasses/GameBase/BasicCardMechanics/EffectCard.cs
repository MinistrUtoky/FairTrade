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
    internal class EffectCard : Card
    {
        protected OneTimeEffect _oneTimeEffect;
        public EffectCard(Scene parentalScene, Vector2 size, Vector2 topLeftPoint, Player owner, OneTimeEffect oneTimeEffect, int price = 0, bool canBePlayed = true,
            Image faceSprite = null): base(parentalScene, topLeftPoint, size, price, owner, canBePlayed)
        {
            _oneTimeEffect = oneTimeEffect;
        }
        public virtual void MakeEffect() { }
    }
}
