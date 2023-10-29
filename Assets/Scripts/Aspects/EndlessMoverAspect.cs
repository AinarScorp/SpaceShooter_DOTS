using SpaceShooter.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct EndlessMoverAspect : IAspect
    {
        const float MAP_RADIUS = 200.0f;
        
        public readonly Entity Entity;
        readonly RefRO<MoveDirection> moveDirectionRef;
        readonly RefRO<MoveSpeed> moveSpeedRef;


        float3 MoveDirection => moveDirectionRef.ValueRO.Value;

        readonly RefRW<LocalTransform> transformRef;
        float3 Position
        {
            get => transformRef.ValueRO.Position;
            set => transformRef.ValueRW.Position = value;
        }
        float MoveSpeed => moveSpeedRef.ValueRO.Value;

        public bool IsOutsideOfMap()
        {
            return math.distance(float3.zero, Position) > MAP_RADIUS;
        }
        public void Move(float deltaTime)
        {
            Position += MoveDirection * MoveSpeed * deltaTime;
        }
    }
}