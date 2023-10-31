using Aspects;
using SpaceShooter.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace SpaceShooter.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(TriggerCollisionSystem))]
    public partial struct HitPlayerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<SimulationSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            BeginInitializationEntityCommandBufferSystem.Singleton ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

            state.Dependency = new HitPlayerJob
            {
                
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),

                AsteroidTagComponentLookup = SystemAPI.GetComponentLookup<AsteroidTag>(true),
                PlayerTagComponentLookup = SystemAPI.GetComponentLookup<PlayerTag>(),

            }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        }
    }

    [BurstCompile]
        [UpdateAfter(typeof(TriggerCollisionSystemJob))]
        struct HitPlayerJob : ITriggerEventsJob
        {
            [ReadOnly]public ComponentLookup<AsteroidTag> AsteroidTagComponentLookup;
            public ComponentLookup<PlayerTag> PlayerTagComponentLookup;
            public EntityCommandBuffer ECB;


            public void Execute(TriggerEvent triggerEvent)
            {
                Entity AsteroidEntity = AsteroidTagComponentLookup.HasComponent(triggerEvent.EntityA) ? triggerEvent.EntityA :
                    AsteroidTagComponentLookup.HasComponent(triggerEvent.EntityB) ? triggerEvent.EntityB : Entity.Null;
                Entity PlayerEntity = PlayerTagComponentLookup.HasComponent(triggerEvent.EntityA) ? triggerEvent.EntityA :
                    PlayerTagComponentLookup.HasComponent(triggerEvent.EntityB) ? triggerEvent.EntityB : Entity.Null;
                if (AsteroidEntity == Entity.Null || PlayerEntity == Entity.Null)
                {
                    return;
                }

                ECB.AppendToBuffer(PlayerEntity, new PlayerDamageBufferElement { Value = 1 });
                ECB.DestroyEntity(AsteroidEntity);
            }
        
    }
}