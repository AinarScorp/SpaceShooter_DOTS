using Aspects;
using Unity.Burst;
using Unity.Entities;

namespace SpaceShooter.Systems
{
    public partial struct DestroyEndlessMoverIfOutside : ISystem
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
            EndSimulationEntityCommandBufferSystem.Singleton ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            new DestroyEndlessMoverJob
            {
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }
    [BurstCompile]
    public partial struct DestroyEndlessMoverJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;

        [BurstCompile]
        private void Execute(EndlessMoverAspect endlessMover, [ChunkIndexInQuery] int sortKey)
        {
            if (!endlessMover.IsOutsideOfMap()) return;
            ECB.DestroyEntity(sortKey,endlessMover.Entity);
            
        }
    }
}