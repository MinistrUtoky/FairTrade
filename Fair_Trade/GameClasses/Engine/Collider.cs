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
        public enum RigidBodyType
        {
            Static,
            Kinematic,
            Dynamic
        }
        protected GameObject2D _parentalGameObject;
        protected RigidBodyType _rigidBodyType = RigidBodyType.Static; 
        protected float mass = 0;
        private Vector2 _velocity = Vector2.zero;

        public Vector2 v { get { return _velocity; } }
        public void SetRBToStatic() { _rigidBodyType = RigidBodyType.Static; _parentalGameObject._parentalScene.DisableGravity(); }
        public void SetRBToKinematic() { _rigidBodyType = RigidBodyType.Kinematic; _parentalGameObject._parentalScene.DisableGravity(); }
        public void SetRBToDynamic() { _rigidBodyType = RigidBodyType.Dynamic; _parentalGameObject._parentalScene.EnableGravity(); }

        public void AddVelocity(Vector2 velocity) => _velocity += velocity;
        public void Stop() => _velocity = Vector2.zero;

        public void AffectByGravity(float frameRateCoef) { if (_rigidBodyType == RigidBodyType.Dynamic) AddVelocity(new Vector2(0, -_parentalGameObject._parentalScene.GetGravity()* frameRateCoef)); }

        public void VelocityRoutine(float frameRateCoef) { if (_rigidBodyType == RigidBodyType.Dynamic) _parentalGameObject.MoveTo(_parentalGameObject.Position() + _velocity * frameRateCoef); }
    }
}
