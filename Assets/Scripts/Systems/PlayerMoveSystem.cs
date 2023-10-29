using Aspects;
using SpaceShooter.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SpaceShooter.Systems
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct PlayerMoveSystem :ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Dependency.Complete();
            var deltaTime = SystemAPI.Time.DeltaTime;
            new PlayerMoveJob
            {
                DeltaTime = deltaTime
            }.Schedule();
        }
    }
    [BurstCompile]
    public partial struct PlayerMoveJob : IJobEntity
    {
        public float DeltaTime;
        [BurstCompile]
        void Execute(PlayerAspect player)
        {
            player.Move(DeltaTime);
            player.Rotate();
        }
    }
}