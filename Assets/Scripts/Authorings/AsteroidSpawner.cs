using SpaceShooter.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace SpaceShooter.Authoring
{
    public class AsteroidSpawner : MonoBehaviour
    {
        public GameObject AsteroidPrefab;
        public float2 MapDimensions;
        public float AsteroidSpawnRate;

        public uint RandomSeed;

    }
    public class AsteroidSpawnerBaker : Baker<AsteroidSpawner>
    {
        public override void Bake(AsteroidSpawner authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new AsteroidSpawnerProperties
            {
                MapDimensions = authoring.MapDimensions,
                AsteroidSpawnRate = authoring.AsteroidSpawnRate,
                AsteroidPrefab = GetEntity(authoring.AsteroidPrefab, TransformUsageFlags.Dynamic),
            });
            AddComponent(entity, new GameRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
            AddComponent<AsteroidSpawnTimer>(entity);
        }
    }
}