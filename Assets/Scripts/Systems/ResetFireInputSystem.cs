using SpaceShooter.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace SpaceShooter.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial struct ResetFireInputSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            foreach ((FireProjectileTag tag, Entity entity) in SystemAPI.Query<FireProjectileTag>().WithEntityAccess())
            {
                ecb.SetComponentEnabled<FireProjectileTag>(entity, false);
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}