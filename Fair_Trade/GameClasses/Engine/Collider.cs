using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fair_Trade.GameClasses.Engine
{
    public class Collider
    {
        protected GameObject2D _parentalGameObject;
        protected int _rigidBodyType = 0; // 0 - none, 1 - static, 2 - kinematic, 3 - dynamic
        protected float mass = 0;

        public void SetRBToStatic() { _rigidBodyType = 1; _parentalGameObject.parentalScene.DisableGravity(); }
        public void SetRBToKinematic() { _rigidBodyType = 2; _parentalGameObject.parentalScene.DisableGravity(); }
        public void SetRBToDynamic() { _rigidBodyType = 3; _parentalGameObject.parentalScene.EnableGravity(); }
        public void SetRBToNone() { _rigidBodyType = 0; _parentalGameObject.parentalScene.DisableGravity(); }
    }
}
