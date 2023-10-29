using Aspects;
using SpaceShooter.Components;
using Unity.Burst;
using Unity.Entities;

namespace SpaceShooter.Systems
{
    [BurstCompile]
    public partial struct EndlessMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            EndSimulationEntityCommandBufferSystem.Singleton ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();


            new EndlessMoveJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }
    [BurstCompile]
    public partial struct EndlessMoveJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;
        
        [BurstCompile]
        private void Execute(EndlessMoverAspect endlessMover, [ChunkIndexInQuery] int sortKey)
        {
            endlessMover.Move(DeltaTime);

        }
    }
}