using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShooter.Components
{
    public struct AsteroidMoveSpeed : IComponentData
    {
        public float Value;
    }

    public struct AsteroidMoveDirection : IComponentData
    {
        public float3 Value;
    }

    public struct AsteroidTag : IComponentData{}
}