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
        private BoxCollider _pseudoCardsCollider;
        private GameObject2D _opponent;
        private System.Windows.Controls.Image sadFaceSprite;
        public ThrowerAI(GameObject2D owner, BoxCollider pseudoCardsCollider, GameObject2D opponent) : base(owner)
        {
            _pseudoCardsCollider = pseudoCardsCollider; _opponent = opponent; sadFaceSprite = _owner._parentalScene.CreateSprite("/Sprites/white_card_sad.jpg");
        }
        public override void StartAIRoutine()
        {
            base.StartAIRoutine();
            SearchForACard();
        }

        private bool ignoreCard = false;
        private async void SearchForACard()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    Task.Delay(1000/GameMode.maxFrameRate + 50).Wait(); 
                    if (!ignoreCard)
                        if (_owner.collider.CheckOnIntersectionWith(_pseudoCardsCollider))
                        {
                            //_pseudoCardsCollider.SetRBToStatic();
                            _pseudoCardsCollider.Stop();
                            _pseudoCardsCollider.AddVelocity(new Vector2(_owner.Position().x < _owner._parentalScene.SceneViewerWidth() / 2? 40: -40, 30));
                            _pseudoCardsCollider.Parent().SetRotationSpeed(-_pseudoCardsCollider.Parent().GetRotationSpeed());                          
                            ignoreCard = true; (_opponent.AI as ThrowerAI).ignoreCard = false;
                            _owner.AudioSource.Play();
                        }
                    if (_pseudoCardsCollider.Position().y < -_owner._parentalScene.SceneViewerHeight() 
                        || _pseudoCardsCollider.Position().x > _owner._parentalScene.SceneViewerWidth()
                        || _pseudoCardsCollider.Position().x < 0)
                    {
                        _owner.SetSprite(sadFaceSprite);
                        _pseudoCardsCollider.Stop();
                        break;
                    }
                }
            });
        }

    }
}
