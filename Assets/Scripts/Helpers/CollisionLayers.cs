using System;

namespace Helpers
{

        [Flags]
        public enum CollisionLayers
        {
            Player = 1 << 0,
            Enemy = 1 << 1,
            Projectile = 1 << 2,
            Ground = 1 << 3
        }
    
}