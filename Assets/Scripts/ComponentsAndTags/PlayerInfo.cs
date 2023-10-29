using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SpaceShooter.Components
{
    public struct PlayerMoveInput : IComponentData
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
}