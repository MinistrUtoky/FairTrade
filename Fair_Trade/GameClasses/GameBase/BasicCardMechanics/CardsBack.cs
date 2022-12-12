using Fair_Trade.GameClasses.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Fair_Trade.GameClasses.GameBase.BasicCardMechanics
{
    internal class CardsBack: GameObject2D
    {
        public CardsBack(Scene parentalScene, Vector2 position, Vector2 size, Image backDesign = null) : base(parentalScene, position, size)
        { 
            _sprite = backDesign;
        }
    }
}
