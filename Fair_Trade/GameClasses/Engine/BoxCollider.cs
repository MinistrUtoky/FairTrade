using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics;
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
        private bool _isBuilding = false;

        public Vector2 Size() => _size;
        public Vector2 Position() => _position;
        public List<Vector2> Dimensions() => _dimensions;

        public BoxCollider(GameObject2D parent, Vector2 topLeftPoint, Vector2 size, float rotation, float mass = 0)
        { 
            _parentalGameObject = parent;
            _rigidBodyType = 0;
            _position = topLeftPoint;
            _size = size;
            _rotation = rotation;
            _dimensions = new List<Vector2>();
            UpdateDimensions();
        }

        public BoxCollider(GameObject2D parent, float topY, float leftX, float height, float width, float rotation, float mass = 0)
        {
            _parentalGameObject = parent;
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
            _isBuilding = true;
            _dimensions.Clear();
            _dimensions.Add(_position);
            Vector2 v = new Vector2(_position.x + _size.x, _position.y);
            Vector2.Rotate(ref v, _position, _rotation); _dimensions.Add(v);
            v = new Vector2(_position.x + _size.x, _position.y - _size.x);
            Vector2.Rotate(ref v, _position, _rotation); _dimensions.Add(v);
            v = new Vector2(_position.x, _position.y - _size.x);
            Vector2.Rotate(ref v, _position, _rotation); _dimensions.Add(v);
        }

        public bool CheckOnIntersectionWith(BoxCollider collider)
        {
            List<Vector2> ds = new List<Vector2>();
            if (!_isBuilding)//this.AllDimensionsUploaded() && collider.AllDimensionsUploaded())
            {
                Vector2 d1 = collider._dimensions[0], d2 = collider._dimensions[1], d3 = collider._dimensions[2], d4 = collider._dimensions[3];
                while (d1 == null || d2 == null || d3 == null || d4 == null)
                {
                    d1 = collider._dimensions[0]; d2 = collider._dimensions[1]; d3 = collider._dimensions[2]; d4 = collider._dimensions[3];
                }
                if (IsInside(d1)) return true;
                if (IsInside(d2)) return true;
                if (IsInside(d3)) return true;
                if (IsInside(d4)) return true;
            }
            return false;
        }

        private bool IsInside(Vector2 dot)
        {
            Vector2 AB = _dimensions[0] - _dimensions[1], AD = _dimensions[2] - _dimensions[1], AM = dot - _dimensions[1];

            if ((0 < AB * AM) && (0 < AD * AM))
            {
                return true;
            }
            return false;
        }

        //public bool AllDimensionsUploaded() => _dimensions == null ? false : _dimensions.Count == 4;
    }
}
