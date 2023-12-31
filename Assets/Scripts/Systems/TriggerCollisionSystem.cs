﻿using SpaceShooter.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace SpaceShooter.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PhysicsSystemGroup))]
    [UpdateBefore(typeof(HitPlayerSystem))]
    public partial struct TriggerCollisionSystem : ISystem
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
            state.Dependency = new TriggerCollisionSystemJob
            {
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),

                AsteroidTagComponentLookup = SystemAPI.GetComponentLookup<AsteroidTag>(true),
                ProjectileTagComponentLookup = SystemAPI.GetComponentLookup<ProjectileTag>(),
            }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
            
        }
    }

    [BurstCompile]
    [UpdateBefore(typeof(HitPlayerJob))]
    struct TriggerCollisionSystemJob : ITriggerEventsJob
    {
        [ReadOnly]public ComponentLookup<AsteroidTag> AsteroidTagComponentLookup;
        public ComponentLookup<ProjectileTag> ProjectileTagComponentLookup;
        public EntityCommandBuffer ECB;

        public void Execute(TriggerEvent triggerEvent)
        {
            Entity AsteroidEntity = AsteroidTagComponentLookup.HasComponent(triggerEvent.EntityA) ? triggerEvent.EntityA :
                AsteroidTagComponentLookup.HasComponent(triggerEvent.EntityB) ? triggerEvent.EntityB : Entity.Null;
            Entity ProjectileEntity = ProjectileTagComponentLookup.HasComponent(triggerEvent.EntityA) ? triggerEvent.EntityA :
                ProjectileTagComponentLookup.HasComponent(triggerEvent.EntityB) ? triggerEvent.EntityB : Entity.Null;
            if (AsteroidEntity == Entity.Null || ProjectileEntity == Entity.Null)
            {
                return;
            }

            ECB.DestroyEntity(ProjectileEntity);
            ECB.DestroyEntity(AsteroidEntity);
        }
    }
}