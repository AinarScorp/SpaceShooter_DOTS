using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShooter.Components
{
    public struct PlayerMoveInput : IComponentData
    {
        public float2 Value;
    }
    public struct PlayerTag : IComponentData {}
    public struct PlayerMoveSpeed : IComponentData
    {
        public float Value;
    }
}