using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fair_Trade.GameClasses
{
    public static class WorldParameters
    {
        private static bool _gravityOn = false;
        private static double _gravity = 10;

        public static void ChangeGravityTo(double g) => _gravity = g;
        public static void EnableGravity() => _gravityOn = true;
        public static void DisableGravity() => _gravityOn = false;
        public static double GetGravity() => _gravityOn ? _gravity : 0;
    }
}
