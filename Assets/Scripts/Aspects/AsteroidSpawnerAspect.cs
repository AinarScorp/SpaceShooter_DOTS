using SpaceShooter.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct AsteroidSpawnerAspect : IAspect
    {
        readonly RefRW<LocalTransform> transform;

        readonly RefRW<AsteroidSpawnTimer> asteroidSpawnTimer;
        readonly RefRW<GameRandom> gameRandom;

        readonly RefRW<AsteroidSpawnerProperties> asteroidSpawnerProperties;

        public Entity AsteroidPrefab
        {
            get { return asteroidSpawnerProperties.ValueRO.AsteroidPrefab; }
        }

        private float3 position
        {
            get { return transform.ValueRO.Position; }
        }

        public LocalTransform GetRandomSpawnLocation()
        {
            return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation = quaternion.identity,
                Scale = GetRandomScale(0.8f,2f)
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
        float GetRandomScale(float minScale, float maxScale)
        {
            return gameRandom.ValueRW.Value.NextFloat(minScale, maxScale);
        }

        private float3 MinCorner
        {
            get { return position - MapDimensions; }
        }

        private float3 MaxCorner
        {
            get { return position + MapDimensions; }
        }

        private float3 MapDimensions
        {
            get
            {
                return new()
                {
                    x = asteroidSpawnerProperties.ValueRO.MapDimensions.x,
                    y = 0f,
                    z = asteroidSpawnerProperties.ValueRO.MapDimensions.y
                };
            }
        }

        public float AsteroidSpawnTimer
        {
            get { return asteroidSpawnTimer.ValueRO.Value; }
            set { asteroidSpawnTimer.ValueRW.Value = value; }
        }

        public bool AsteroidIsReadyForSpawn
        {
            get { return AsteroidSpawnTimer <= 0f; }
        }

        public float AsteroidSpawnRate
        {
            get => asteroidSpawnerProperties.ValueRO.AsteroidSpawnRate;
            private set=> asteroidSpawnerProperties.ValueRW.AsteroidSpawnRate = value;
        }

        public void ReduceSpawnRate(float deltaTime)
        {
            AsteroidSpawnRate = math.clamp(AsteroidSpawnRate,0, AsteroidSpawnRate - asteroidSpawnerProperties.ValueRO.AsteroidSpeedIncreaseMultiplier * deltaTime);
        }
    }
}