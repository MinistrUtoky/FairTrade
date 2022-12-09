using System;
using System.CodeDom;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fair_Trade.GameClasses.Engine
{
    public class GameObject2D
    {
        public Type objectType;
        public Scene parentalScene;
        protected Vector2 _position;
        protected Vector2 _size;
        private List<Vector2> _dimensions;
        //private int _globalPosX = 0;
        //private int _globalPosY = 0;
        protected float _rotation = 0;
        public BoxCollider collider = null;
        protected Image _sprite;

        public GameObject2D(Vector2 position, Vector2 size)
        {
            _dimensions = new List<Vector2>();
            this.MoveTo(position);
            this.Resize(size);
            UpdateDimensions();
        }

        public void MoveTo(Vector2 newPos)
        {
            _position = new Vector2(newPos.x, newPos.y);
            if (_size != null) UpdateDimensions();
            if (collider != null)
            {
                Vector2 deltaPos = newPos - _position;
                collider.MoveTo(deltaPos);
            }
        }
        public void Resize(Vector2 newSize)
        {
            _size = new Vector2(newSize.x, newSize.y);
            if (_position != null) UpdateDimensions();
            if (collider != null)
                collider.Resize(newSize);
        }

        public void Rotate(float angle)
        {
            _rotation += angle;
            _rotation %= 360;
            UpdateDimensions();
            if (collider != null)
                collider.Rotate(angle);
        }

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

        public void AssignBoxCollider(Vector2 topLeftPoint, Vector2 size, float rotation) => collider = new BoxCollider(topLeftPoint, size, rotation);
        public void AssignFastBoxCollider() => collider = new BoxCollider(_position, _size, _rotation);

        public void SetSprite(Image sprite) => _sprite = sprite;
    }
}
