using SpaceShooter.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SpaceShooter.Authoring
{
    public class ShipAuthoring : MonoBehaviour
    {
        public float MoveSpeed = 10;
        public float2 MoveClampDimensions = new float2(145, 80);
        public GameObject ProjectilePrefab;
        public float MaxHealth = 100;


    }
    public class ShipAuthoringBaker : Baker<ShipAuthoring>
    {
        public override void Bake(ShipAuthoring authoring)
        {
            Entity playerEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent<PlayerTag>(playerEntity);
            AddComponent<PlayerMoveInput>(playerEntity);
            AddComponent<PlayerClampDimensions>(playerEntity, new PlayerClampDimensions
            {
                Value =authoring.MoveClampDimensions
            });
            AddComponent<PlayerHealth>(playerEntity, new PlayerHealth
            {
                MaxHealth = authoring.MaxHealth,
                CurrentHealth = authoring.MaxHealth
            });
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
            AddBuffer<PlayerDamageBufferElement>(playerEntity);

    
        }
    }
}