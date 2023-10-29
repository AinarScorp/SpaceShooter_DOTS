using Aspects;
using Helpers;
using SpaceShooter.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
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
            Entity playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            float3 playerLocation = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position;

            new SpawnAsteroidJob
            {
                PlayerLocation = playerLocation,
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Schedule();
        }
    }
    [BurstCompile]
    public partial struct SpawnAsteroidJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;
        public float3 PlayerLocation;
        
        [BurstCompile]
        void Execute(AsteroidSpawnerAspect asteroidSpawner)
        {
            asteroidSpawner.AsteroidSpawnTimer -= DeltaTime;
            if(!asteroidSpawner.AsteroidIsReadyForSpawn) return;
            //if(!asteroid.ZombieSpawnPointInitialized()) return;
            
            asteroidSpawner.AsteroidSpawnTimer = asteroidSpawner.AsteroidSpawnRate;
            Entity newAsteroid = ECB.Instantiate(asteroidSpawner.AsteroidPrefab);
            
            LocalTransform newAsteroidPosition = asteroidSpawner.GetRandomSpawnLocation();
            ECB.SetComponent(newAsteroid, newAsteroidPosition);

            ECB.SetComponent(newAsteroid, new MoveDirection{Value = MathHelpers.GetDirection(newAsteroidPosition.Position, PlayerLocation)});
            ECB.SetComponentEnabled<MoveSpeed>(newAsteroid, true);


        }
    }
}