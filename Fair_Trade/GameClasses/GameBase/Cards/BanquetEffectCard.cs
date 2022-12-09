using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Fair_Trade.GameClasses.Engine;

namespace Fair_Trade.GameClasses.GameBase.BasicCardMechanics
{
    internal class BanquetEffectCard: EffectCard
    {
        public BanquetEffectCard(Vector2 size, Vector2 topLeftPoint, Player owner, OneTimeEffect oneTimeEffect, int price = 0, bool canBePlayed = true,
            Image faceSprite = null) : base(size, topLeftPoint, owner, oneTimeEffect, price, canBePlayed, faceSprite) { }
    }
}
