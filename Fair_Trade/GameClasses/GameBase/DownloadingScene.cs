using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Fair_Trade.GameClasses.Engine;
using Fair_Trade.GameClasses.GameBase.BasicCardMechanics;

namespace Fair_Trade.GameClasses.GameBase
{
    internal class DownloadingScene: Scene
    {
        private GameObject2D _firstCardThrower;
        private GameObject2D _secondCardThrower;
        private CardsBack _pseudoCardToThrow;
        public DownloadingScene() {
            EnableGravity();
            _firstCardThrower = new GameObject2D(Vector2.zero, Vector2.zero);
            _secondCardThrower = new GameObject2D(Vector2.zero, Vector2.zero);
            _pseudoCardToThrow = new CardsBack(Vector2.zero, Vector2.zero);
            _firstCardThrower.objectType = typeof(Image);
            _secondCardThrower.objectType = typeof(Image);
            _pseudoCardToThrow.objectType = typeof(Image);  
        }

        public override void Display()
        {
            base.Display();
            (_sceneViewer as Download_Page).Display();
        }

        public override void GenerateScene()
        {
            base.GenerateScene();
            InstantiateObject(_firstCardThrower);
            InstantiateObject(_secondCardThrower);
            InstantiateObject(_pseudoCardToThrow);
        }
    }
}
