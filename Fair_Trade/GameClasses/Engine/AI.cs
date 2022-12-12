using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fair_Trade.GameClasses.Engine
{
    public class AI
    {
        protected GameObject2D _owner;
        protected bool _aiRoutineStarted = false;
        
        public AI(GameObject2D owner) { _owner = owner; }
        public virtual void StartAIRoutine() { }
    }
}
