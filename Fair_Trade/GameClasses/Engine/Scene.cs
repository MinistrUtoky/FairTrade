using Fair_Trade.GameClasses.GameBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fair_Trade.GameClasses.Engine
{
    public class Scene
    {
        protected Window _sceneViewer;
        public List<GameObject2D> _objectsInScene;
        protected static bool _gravityOn = false;
        protected static float _gravity = 10;

        public void ChangeGravityTo(float g) => _gravity = g;
        public void EnableGravity() => _gravityOn = true;
        public void DisableGravity() => _gravityOn = false;
        public float GetGravity() => _gravityOn ? _gravity : 0;

        public virtual void Display() { }
        public virtual void GenerateScene() { }

        public void AssignViewport(Window viewport) => _sceneViewer = viewport;


        public void InstantiateObject(GameObject2D obj)
        {
            _objectsInScene.Add(obj);
        }

        public bool IsCasted(GameObject2D caster, GameObject2D castedOn)
        {
            BoxCollider castersCollider = caster.collider;
            BoxCollider castedObjectsCollider = castedOn.collider;
            if (castersCollider.MostRightPointX() >= castedObjectsCollider.MostLeftPointX() && castersCollider.TopPointY() >= castedObjectsCollider.BottomPointY())
                return true;
            else return false;
        }
    }
}
