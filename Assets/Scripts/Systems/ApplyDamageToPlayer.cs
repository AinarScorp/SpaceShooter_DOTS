using Aspects;
using Unity.Burst;
using Unity.Entities;

namespace SpaceShooter.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial struct ApplyDamageToPlayer : ISystem
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
            state.Dependency.Complete();
            foreach (PlayerAspect brain in SystemAPI.Query<PlayerAspect>())
            {
                brain.GetDamaged();
            }
        }
    }
}