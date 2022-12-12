using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Fair_Trade.GameClasses.Engine;
using Fair_Trade.GameClasses.GameBase.BasicCardMechanics;

namespace Fair_Trade.GameClasses.GameBase.AIs
{ 
    internal class ThrowerAI : AI
    {
        public ThrowerAI(GameObject2D owner) : base(owner) { }
        public async override void StartAIRoutine()
        {
            base.StartAIRoutine();
            await Task.Run(() =>
            {
                Parallel.Invoke(() => SearchForACard());
            });
        }

        public async void SearchForACard()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    Task.Delay(1000/GameMode.maxFrameRate); 
                    foreach (GameObject2D obj in _owner._parentalScene._objectsInScene)
                        if (obj.GetType() == typeof(CardsBack))
                            if (_owner.collider.CheckOnIntersectionWith(obj.collider))
                            {
                                obj.collider.SetRBToStatic();   
                                obj.collider.Stop();
                                return;
                            }
                }//Parallel.Invoke(() => SearchForACard());
            });
        }

    }
}
