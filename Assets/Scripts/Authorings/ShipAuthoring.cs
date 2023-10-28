using SpaceShooter.Components;
using Unity.Entities;
using UnityEngine;

namespace SpaceShooter.Authoring
{
    public class ShipAuthoring : MonoBehaviour
    {
        public float MoveSpeed = 10;
    }
    public class ShipAuthoringBaker : Baker<ShipAuthoring>
    {
        public override void Bake(ShipAuthoring authoring)
        {
            Entity playerEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent<PlayerTag>(playerEntity);
            AddComponent<PlayerMoveInput>(playerEntity);
            
            AddComponent(playerEntity, new PlayerMoveSpeed
            {
                Value = authoring.MoveSpeed
            });
        }
    }
}