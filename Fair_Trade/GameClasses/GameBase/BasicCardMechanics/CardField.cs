using Fair_Trade.GameClasses.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fair_Trade.GameClasses.Engine.GameObject2D;

namespace Fair_Trade.GameClasses.GameBase.BasicCardMechanics
{
    internal class CardField: GameObject2D
    {
        protected Player _owner;
        private bool _containsACard = false;
        public Player Owner { get { return _owner; } }
        public void OwnedBy(Player byWhom) => _owner = byWhom;
        public CardField(Scene parentalScene, Vector2 position, Vector2 size, Player owner, bool containsACard = false) : base(parentalScene, position, size)
        {
            _owner = owner;
            _containsACard = containsACard;
        }

    }
}
