using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShooter.Components
{
    public struct GameRandom : IComponentData
    {
        public Random Value;
    }
}