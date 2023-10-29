using SpaceShooter.Components;
using Unity.Entities;
using UnityEngine;

namespace SpaceShooter.Authoring
{
    public class Asteroid : MonoBehaviour
    {
        public float MoveSpeed;
    }

    public class AsteroidBaker : Baker<Asteroid>
    {
        public override void Bake(Asteroid authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<AsteroidTag>(entity);
            AddComponent(entity, new MoveSpeed()
            {
                Value = authoring.MoveSpeed
            });
            SetComponentEnabled<MoveSpeed>(entity,false);
            AddComponent<MoveDirection>(entity);
            //AddComponent<NewlySpawnedAsteroidTag>(asteroidEntity);

        }
    }
}