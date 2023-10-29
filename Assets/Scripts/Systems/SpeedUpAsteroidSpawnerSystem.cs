using System.Runtime.CompilerServices;
using Aspects;
using SpaceShooter.Components;
using Unity.Burst;
using Unity.Entities;

namespace SpaceShooter.Systems
{
    [BurstCompile]
    public partial struct SpeedUpAsteroidSpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            
            new SpeedUpAsteroidSpawnerJob
            {
                DeltaTime = deltaTime,
            }.Schedule();
        }
    }
    [BurstCompile]
    public partial struct SpeedUpAsteroidSpawnerJob : IJobEntity
    {
        public float DeltaTime;
        [BurstCompile]
        void Execute(AsteroidSpawnerAspect asteroidSpawnerAspect)
        {
            asteroidSpawnerAspect.ReduceSpawnRate(DeltaTime);
        }

    }

}