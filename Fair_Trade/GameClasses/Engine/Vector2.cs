using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Fair_Trade.GameClasses.Engine
{
    public class Vector2
    {
        public float x;
        public float y;
        public Vector2(float nx, float ny)
        {
            x = nx;
            y = ny;
        }

        public static Vector2 zero { get { return new Vector2(0f,0f); } }
        public static Vector2 operator +(Vector2 vector1, Vector2 vector2) => new Vector2(vector1.x + vector2.x, vector1.y + vector2.y);
        public static Vector2 operator -(Vector2 vector) => new Vector2(-vector.x, -vector.y);
        public static Vector2 operator -(Vector2 vector1, Vector2 vector2) => (vector1 + -vector2);
        public static Vector2 operator *(float coef, Vector2 vector) => new Vector2(vector.x * coef, vector.y * coef);
        public static Vector2 operator *(Vector2 vector, float coef) => new Vector2(vector.x * coef, vector.y * coef);
        public static void Rotate(ref Vector2 point, Vector2 relativeTo, float angle) {
            point -= relativeTo;
            point = new Vector2((float)(point.x * Math.Cos(angle * Math.PI / 180) - point.y * Math.Sin(angle * Math.PI / 180)),
            (float)(point.x * Math.Sin(angle * Math.PI / 180) + point.y * Math.Cos(angle * Math.PI / 180)));
            point += relativeTo;
        }

        public static float operator *(Vector2 vector1, Vector2 vector2) => vector1.x*vector2.x + vector1.y*vector2.y;

        public override string ToString() => (x.ToString() + " " + y.ToString());
    }
}
