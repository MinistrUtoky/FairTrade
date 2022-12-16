using System;
using System.CodeDom;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;

namespace Fair_Trade.GameClasses.Engine
{
    public class GameObject2D
    {
        public enum GameObjectType
        {
            Hidden,
            Visible
        }
        public GameObjectType objectType = GameObjectType.Hidden;
        public Scene _parentalScene;
        protected Vector2 _position;
        protected Vector2 _size;
        protected Vector2 _pivot;
        //private int _globalPosX = 0;
        //private int _globalPosY = 0;
        protected float _rotation = 0;
        protected float _rotationSpeed = 0f;
        public BoxCollider collider = null;
        protected AI ai = null;
        protected Image _sprite = null;
        protected Animation _animation = null;
        protected AudioSource _audioSource = null;

        public Vector2 Size() => _size;
        public Vector2 Position() => _position;
        public Vector2 Pivot() => _pivot;
        public float Rotation() => _rotation;
        public void SetRotationSpeed(float angle) => _rotationSpeed = angle;
        public float GetRotationSpeed() => _rotationSpeed;
        public void StopRotation() => _rotationSpeed = 0f;
        public AI AI { get { return ai; } }
        public GameObject2D(Scene parentalScene, Vector2 position, Vector2 size)
        {
            _parentalScene = parentalScene;
            _position = position;
            _size = size;
            _pivot = new Vector2(position.x + size.x / 2, position.y - size.y / 2);
        }

        public void MoveTo(Vector2 newPos)
        {
            _position = new Vector2(newPos.x, newPos.y);
            Repivot();
            if (collider != null)
                collider.MoveTo(newPos);
        }
        public void Resize(Vector2 newSize)
        {
            _size = new Vector2(newSize.x, newSize.y);
            Repivot();
            if (collider != null)
                collider.Resize(newSize);
        }

        public void Rotate(float angle)
        {
            _rotation = (_rotation + angle) % 360f;
            if (collider != null)
                collider.Rotate(angle);
        }

        public void Repivot() { 
            _pivot = _position + new Vector2(_size.x/2,-_size.y/2); 
        }

        /*public List<Vector2> GetDimensions()
        {
            List<Vector2> _dimensions = new List<Vector2>();
            _dimensions.Add(_position);
            Vector2 v = new Vector2(_position.x + _size.x, _position.y);
            Vector2.Rotate(ref v, _position, _rotation); _dimensions.Add(v);
            v = new Vector2(_position.x + _size.x, _position.y - _size.x);
            Vector2.Rotate(ref v, _position, _rotation); _dimensions.Add(v);
            v = new Vector2(_position.x, _position.y - _size.x);
            Vector2.Rotate(ref v, _position, _rotation); _dimensions.Add(v);
            return _dimensions;
            //if (collider != null)
            //    collider.UpdateDimensions();
        }*/

        public void AssignBoxCollider(Vector2 topLeftPoint, Vector2 size, float rotation) => collider = new BoxCollider(this, topLeftPoint, size, rotation);
        public void AssignFastBoxCollider() => collider = new BoxCollider(this, _position, _size, _rotation);

        public void SetSprite(Image sprite) => _sprite = sprite;
        public Image Sprite { get { return _sprite; } }
        
        public void GoToNextFrameSprite() {
            if (_animation != null) _sprite = _animation.GetNextFrame();
        }
        public Animation Animation { get { return _animation; } }
        public void AssignAnimation(List<Image> animation) => _animation = new Animation(Animation.AnimationType.Loop, animation); 
        public void AddFrameToAnimation(Image frame) => _animation.AddFrame(frame);
        
        public void AssignAI(AI aiToAssign) => ai = aiToAssign;

        public AudioSource AudioSource { get { return _audioSource; } }
        public void AssignAudioSource(AudioSource audioSource) => _audioSource = audioSource;

        public void ResizeGameObjectAccordingToSpriteSize() { _size.x = (float)_sprite.Width; _size.y = (float)_sprite.Height; }
    }
}
