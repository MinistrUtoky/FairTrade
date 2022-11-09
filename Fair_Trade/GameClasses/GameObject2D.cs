using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fair_Trade.GameClasses
{
    internal class GameObject2D
    {
        private int _height = 0;
        private int _width = 0;
        private int _screenPosX = 0;
        private int _screenPosY = 0;
        //private int _globalPosX = 0;
        //private int _globalPosY = 0;
        private double _rotationDegree = 0;
        private BoxCollider _collider;

        public GameObject2D()
        {
            _collider = new BoxCollider();
        }
    }
}
