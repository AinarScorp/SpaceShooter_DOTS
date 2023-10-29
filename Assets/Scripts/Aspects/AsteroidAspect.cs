using SpaceShooter.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct AsteroidAspect : IAspect
    {
        public readonly Entity Entity;

        readonly RefRW<LocalTransform> transform;

        readonly RefRW<AsteroidSpawnTimer> asteroidSpawnTimer;
        readonly RefRW<GameRandom> gameRandom;

        readonly RefRO<AsteroidSpawnerProperties> asteroidSpawnerProperties;

        public Entity AsteroidPrefab => asteroidSpawnerProperties.ValueRO.AsteroidPrefab;
        private float3 position => transform.ValueRO.Position;

        public LocalTransform GetRandomSpawnLocation()
        {
            return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation = quaternion.identity,
                Scale = 1
            };
        }

        private float3 GetRandomPosition()
        {
            bool spawnVertically = gameRandom.ValueRW.Value.NextBool();
            bool spawnWithPositiveValue = gameRandom.ValueRW.Value.NextBool();

            float3 minCorner = MinCorner;
            float3 maxCorner = MaxCorner;
            
            float3 randomPosition = gameRandom.ValueRW.Value.NextFloat3(minCorner, maxCorner);
            if (spawnVertically)
            {
                randomPosition.z = spawnWithPositiveValue ? maxCorner.z : minCorner.z;
            }
            else
            {
                randomPosition.x = spawnWithPositiveValue ? maxCorner.x : minCorner.x;
            }

            return randomPosition;
        }

        private float3 MinCorner => position - MapDimensions;
        private float3 MaxCorner => position + MapDimensions;

        private float3 MapDimensions => new()
        {
            x = asteroidSpawnerProperties.ValueRO.MapDimensions.x,
            y = 0f,
            z = asteroidSpawnerProperties.ValueRO.MapDimensions.y
        };
        public float AsteroidSpawnTimer
        {
            get => asteroidSpawnTimer.ValueRO.Value;
            set => asteroidSpawnTimer.ValueRW.Value = value;
        }
        public bool AsteroidIsReadyForSpawn => AsteroidSpawnTimer <= 0f;
        public float AsteroidSpawnRate => asteroidSpawnerProperties.ValueRO.AsteroidSpawnRate;


    }
}