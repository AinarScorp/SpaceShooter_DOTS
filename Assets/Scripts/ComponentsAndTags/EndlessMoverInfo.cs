using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShooter.Components
{
    public struct MoveSpeed : IComponentData, IEnableableComponent
    {
        public float Value;
    }

    public struct MoveDirection : IComponentData
    {
        public float3 Value;
    }


}