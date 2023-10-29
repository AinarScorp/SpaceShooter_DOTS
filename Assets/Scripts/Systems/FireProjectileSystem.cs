using Helpers;
using SpaceShooter.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SpaceShooter.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct FireProjectileSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach ((ProjectilePrefab projectilePrefab, LocalTransform transform) in SystemAPI.Query<ProjectilePrefab, LocalTransform>().WithAll<FireProjectileTag>())
            {
                Entity newProjectile = ecb.Instantiate(projectilePrefab.Value);

                LocalTransform projectileTransform = transform;
                projectileTransform.Scale = 1;
                
                ecb.SetComponent(newProjectile, projectileTransform);
                ecb.SetComponent(newProjectile, new MoveDirection{Value = MathHelpers.GetDirectionFromForward(transform.Rotation)});

                ecb.SetComponentEnabled<MoveSpeed>(newProjectile, true);
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}