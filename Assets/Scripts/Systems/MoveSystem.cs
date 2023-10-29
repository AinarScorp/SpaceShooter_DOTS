using Aspects;
using SpaceShooter.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SpaceShooter.Systems
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct MoveSystem :ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            new MoveJob
            {
                DeltaTime = deltaTime
            }.Schedule();
        }
    }
    [BurstCompile]
    public partial struct MoveJob : IJobEntity
    {
        public float DeltaTime;
        [BurstCompile]
        void Execute(PlayerAspect transform, in PlayerMoveInput moveInput, PlayerMoveSpeed moveSpeed)
        {
            transform.Move(moveInput.Value * moveSpeed.Value * DeltaTime);
            //transform.Position.xz += moveInput.Value * moveSpeed.Value * DeltaTime;
            if (math.lengthsq(moveInput.Value) > float.Epsilon)
            {
                float3 forward = new float3(moveInput.Value.x, 0f, moveInput.Value.y);
                transform.Rotate(forward);
                //transform.Rotation = quaternion.LookRotation(forward, math.up());
            }
        }
    }
}