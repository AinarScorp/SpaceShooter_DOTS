using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShooter.Components
{
    public struct AsteroidSpawnerProperties: IComponentData
    {
        public Entity AsteroidPrefab;
        public float2 MapDimensions;
        public float AsteroidSpawnRate;
        public float AsteroidSpeedIncreaseMultiplier;
    }

    

    public struct AsteroidSpawnTimer : IComponentData
    {
        public float Value;
    }
    
}