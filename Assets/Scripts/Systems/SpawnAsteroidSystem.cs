using Aspects;
using SpaceShooter.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace SpaceShooter.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnAsteroidSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AsteroidSpawnTimer>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            BeginInitializationEntityCommandBufferSystem.Singleton ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

            new SpawnAsteroidJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Run();
        }
    }
    [BurstCompile]
    public partial struct SpawnAsteroidJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;
        
        [BurstCompile]
        void Execute(AsteroidAspect asteroid)
        {
            asteroid.AsteroidSpawnTimer -= DeltaTime;
            if(!asteroid.AsteroidIsReadyForSpawn) return;
            //if(!asteroid.ZombieSpawnPointInitialized()) return;
            
            asteroid.AsteroidSpawnTimer = asteroid.AsteroidSpawnRate;
            Entity newZombie = ECB.Instantiate(asteroid.AsteroidPrefab);
            
            LocalTransform newZombieTransform = asteroid.GetRandomSpawnLocation();
            ECB.SetComponent(newZombie, newZombieTransform);
            //
            // float zombieHeading = MathHelpers.GetHeading(newZombieTransform.Position, asteroid.Position);
            // ECB.SetComponent(newZombie, new ZombieHeading{Value = zombieHeading});
        }
    }
}