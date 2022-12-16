using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Security.Policy;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Fair_Trade.GameClasses.Engine
{
    public class BoxCollider : Collider
    {
        private Vector2 _position;
        private Vector2 _size;
        private float _rotation;
        private Vector2 _pivot;
        private System.Windows.Shapes.Rectangle _borderline;

        public Vector2 Size() => _size;
        public Vector2 Position() => _position;
        public Vector2 Pivot() => _pivot;
        public float Rotation() => _rotation;
        public System.Windows.Shapes.Rectangle Borderline() => _borderline;


        public BoxCollider(GameObject2D parent, Vector2 topLeftPoint, Vector2 size, float rotation, float mass = 0)
        { 
            _parentalGameObject = parent;
            _rigidBodyType = 0;
            _position = topLeftPoint;
            _size = size;           
            _pivot = new Vector2(topLeftPoint.x + size.x / 2, topLeftPoint.y - size.y / 2);
            _rotation = rotation;
            _borderline = new System.Windows.Shapes.Rectangle(); //((int)topLeftPoint.x, (int)topLeftPoint.y, (int)size.x, (int)size.y);
            _borderline.Width = size.x; _borderline.Height = size.y; _borderline.Stroke = Brushes.Red; _borderline.StrokeThickness = 5;
        }

        public BoxCollider(GameObject2D parent, float topY, float leftX, float height, float width, float rotation, float mass = 0)
        {
            _parentalGameObject = parent;
            _rigidBodyType = 0;
            _position = new Vector2(leftX, topY);
            _size = new Vector2(width, height);
            _pivot = new Vector2(leftX + _size.x / 2, topY - _size.y / 2);
            _rotation = rotation;
            _borderline = new System.Windows.Shapes.Rectangle(); //((int)leftX, (int)topY, (int)width, (int)height);
            _borderline.Width = width; _borderline.Height = height; _borderline.Stroke = Brushes.Red; _borderline.StrokeThickness = 5;
        }

        public void Rotate(float angle) { _rotation = (_rotation + angle)%360f;  }
        public void MoveTo(Vector2 newPos) { _position = new Vector2(newPos.x, newPos.y); Repivot(); }//_borderline.Offset(new System.Drawing.Point((int)(newPos.x - _borderline.Left), (int)(newPos.y - _borderline.Top)));
        public void Resize(Vector2 newSize) { _size = newSize; Repivot(); }
        public void Repivot() { _pivot = _position + new Vector2(_size.x / 2, -_size.y / 2); }



        /*
        public List<Vector2> GetDimensions()
        {
            List<Vector2> _dimensions = new List<Vector2>();
            _dimensions.Add(_position);
            Vector2 v = new Vector2(_position.x + _size.x, _position.y);
            Vector2.Rotate(ref v, _position, _rotation); _dimensions.Add(v);
            v = new Vector2(_position.x + _size.x, _position.y - _size.y);
            Vector2.Rotate(ref v, _position, _rotation); _dimensions.Add(v);
            v = new Vector2(_position.x, _position.y - _size.y);
            Vector2.Rotate(ref v, _position, _rotation); _dimensions.Add(v);
            return _dimensions;
        }*/
        public List<Vector2> GetDimensions()
        {
            List<Vector2> _dimensions = new List<Vector2>();
            Vector2 v = new Vector2(_position.x, _position.y);
            Vector2.Rotate(ref v, _pivot, _rotation); _dimensions.Add(v);
            v = new Vector2(_position.x + _size.x, _position.y);
            Vector2.Rotate(ref v, _pivot, _rotation); _dimensions.Add(v);
            v = new Vector2(_position.x + _size.x, _position.y - _size.y);
            Vector2.Rotate(ref v, _pivot, _rotation); _dimensions.Add(v);
            v = new Vector2(_position.x, _position.y - _size.y);
            Vector2.Rotate(ref v, _pivot, _rotation); _dimensions.Add(v);
            return _dimensions;
        }


        public bool CheckOnIntersectionWith(BoxCollider collider)
        {
            List<Vector2> anotherColliderDimensions = collider.GetDimensions();
            if (IsInside(anotherColliderDimensions[0])) return true;
            if (IsInside(anotherColliderDimensions[1])) return true;
            if (IsInside(anotherColliderDimensions[2])) return true;
            if (IsInside(anotherColliderDimensions[3])) return true;
            return false;
        }
        
        private List<Vector2> ds;
        private Vector2 AB, AD, AM, CA, CD, CM;
        private bool IsInside(Vector2 dot)
        {
            ds = GetDimensions();
            AB = ds[0] - ds[1]; AD = ds[2] - ds[1]; AM = dot - ds[1]; CA = ds[0] - ds[3]; CD = ds[2] - ds[3]; CM = dot - ds[3];
            if ((0 < AB * AM) && (0 < AD * AM) && (0 < CA * CM) && (0 < CD * CM))
            {
                return true;
            }
            return false;
        }

    }
}
