using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fair_Trade.GameClasses
{
    internal class Collider
    {
        private int _rigidBodyType = 0; // 0 - none, 1 - static, 2 - kinematic, 3 - dynamic
        public void SetRBToStatic() { _rigidBodyType = 1; WorldParameters.DisableGravity(); }
        public void SetRBToKinematic() { _rigidBodyType = 2; WorldParameters.DisableGravity(); }
        public void SetRBToDynamic() { _rigidBodyType = 3; WorldParameters.EnableGravity(); }
        public void SetRBToNone() { _rigidBodyType = 0; WorldParameters.DisableGravity(); }
    }
}
