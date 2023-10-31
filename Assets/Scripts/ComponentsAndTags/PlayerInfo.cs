using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SpaceShooter.Components
{
    public struct PlayerMoveInput : IComponentData
    {
        public float2 Value;
    }

    public struct PlayerClampDimensions : IComponentData
    {
        public float2 Value;
    }
    public struct PlayerMousePos : IComponentData
    {
        public float2 Value;
    }
    public struct PlayerTag : IComponentData {}
    public struct PlayerMoveSpeed : IComponentData
    {
        public float Value;
    }

    public struct PlayerHealth : IComponentData
    {
        public float CurrentHealth;
        public float MaxHealth;
    }

    public struct PlayerDamageBufferElement : IBufferElementData
    {
        public float Value;
    }
}