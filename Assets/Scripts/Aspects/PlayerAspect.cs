using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct PlayerAspect : IAspect
    {
        readonly RefRW<LocalTransform> transform;

        public void Move(float2 deltaLocation)
        {
            transform.ValueRW.Position.xz += deltaLocation;
        }

        public void Rotate(float3 direction)
        {
            transform.ValueRW.Rotation = quaternion.LookRotation(direction, math.up());
        }
    }
}