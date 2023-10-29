using SpaceShooter.Components;
using Unity.Entities;
using UnityEngine;

namespace SpaceShooter.Authoring
{
    public class Projectile : MonoBehaviour
    {
        public float MoveSpeed = 10;

    }
    public class ProjectileBaker : Baker<Projectile>
    {
        public override void Bake(Projectile authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<ProjectileTag>(entity);
            AddComponent(entity, new MoveSpeed()
            {
                Value = authoring.MoveSpeed
            });
            AddComponent<MoveDirection>(entity);
            //AddComponent<NewlySpawnedAsteroidTag>(entity);
            SetComponentEnabled<MoveSpeed>(entity,false);
        }
    }
}