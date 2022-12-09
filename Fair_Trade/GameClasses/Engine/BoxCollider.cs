using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Fair_Trade.GameClasses.Engine
{
    public class BoxCollider : Collider
    {
        private Vector2 _position;
        private Vector2 _size;
        private List<Vector2> _dimensions;
        private float _rotation;
        private Vector2 _velocity;

        public Vector2 Size() => _size;
        public Vector2 Position() => _position;

        public BoxCollider(Vector2 topLeftPoint, Vector2 size, float rotation, float mass = 0)
        {
            _rigidBodyType = 0;
            _position = topLeftPoint;
            _size = size;
            _rotation = rotation;
            _dimensions = new List<Vector2>();
            UpdateDimensions();
        }

        public BoxCollider(float topY, float leftX, float height, float width, float rotation, float mass = 0)
        {
            _rigidBodyType = 0;
            _position = new Vector2(leftX, topY);
            _size = new Vector2(width, height);
            _rotation = rotation;
            _dimensions = new List<Vector2>();
            UpdateDimensions();
        }

        public void Rotate(float angle) { _rotation += angle; UpdateDimensions(); }
        public void MoveTo(Vector2 newPos) { _position = new Vector2(newPos.x, newPos.y); UpdateDimensions(); }
        public void Resize(Vector2 newSize) { _size = newSize; UpdateDimensions(); }

        public void UpdateDimensions()
        {
            _dimensions.Clear();
            _dimensions.Add(_position);
            Vector2 v = new Vector2(_position.x + _size.x, _position.y);
            Vector2.Rotate(ref v, _position, _rotation); _dimensions.Add(v);
            v = new Vector2(_position.x + _size.x, _position.y - _size.x);
            Vector2.Rotate(ref v, _position, _rotation); _dimensions.Add(v);
            v = new Vector2(_position.x, _position.y - _size.x);
            Vector2.Rotate(ref v, _position, _rotation); _dimensions.Add(v);
        }
        public float MostLeftPointX() => Math.Min(Math.Min(Math.Min(_dimensions[0].x, _dimensions[1].x), _dimensions[2].x), _dimensions[3].x);
        public float MostRightPointX() => Math.Max(Math.Max(Math.Max(_dimensions[0].x, _dimensions[1].x), _dimensions[2].x), _dimensions[3].x);
        public float BottomPointY() => Math.Min(Math.Min(Math.Min(_dimensions[0].y, _dimensions[1].y), _dimensions[2].y), _dimensions[3].y);
        public float TopPointY() => Math.Max(Math.Max(Math.Max(_dimensions[0].y, _dimensions[1].y), _dimensions[2].y), _dimensions[3].y);


        public void AddVelocity(Vector2 velocity) => _velocity += velocity;

        public void AffectByGravity()
        {
            AddVelocity(new Vector2(0, -_parentalGameObject.parentalScene.GetGravity()));
        }

        
    }
}
