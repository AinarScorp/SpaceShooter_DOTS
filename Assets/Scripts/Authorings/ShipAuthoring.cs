using SpaceShooter.Components;
using Unity.Entities;
using UnityEngine;

namespace SpaceShooter.Authoring
{
    public class ShipAuthoring : MonoBehaviour
    {
        public float MoveSpeed = 10;
        public GameObject ProjectilePrefab;

    }
    public class ShipAuthoringBaker : Baker<ShipAuthoring>
    {
        public override void Bake(ShipAuthoring authoring)
        {
            Entity playerEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent<PlayerTag>(playerEntity);
            AddComponent<PlayerMoveInput>(playerEntity);
            AddComponent<PlayerMousePos>(playerEntity);
            AddComponent(playerEntity, new PlayerMoveSpeed
            {
                Value = authoring.MoveSpeed
            });
            
            AddComponent<FireProjectileTag>(playerEntity);
            SetComponentEnabled<FireProjectileTag>(playerEntity, false);
            AddComponent(playerEntity, new ProjectilePrefab
            {
                Value = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}