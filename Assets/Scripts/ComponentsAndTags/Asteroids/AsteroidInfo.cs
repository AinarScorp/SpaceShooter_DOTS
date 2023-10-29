using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShooter.Components
{
    public struct AsteroidTag : IComponentData{}
    public struct NewlySpawnedAsteroidTag : IComponentData{}
    public struct FireProjectileTag : IComponentData, IEnableableComponent {}
    public struct ProjectilePrefab : IComponentData
    {
        public Entity Value;
    }

}