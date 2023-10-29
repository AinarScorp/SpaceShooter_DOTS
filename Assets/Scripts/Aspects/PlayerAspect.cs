using Helpers;
using SpaceShooter.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct PlayerAspect : IAspect
    {
        readonly RefRW<LocalTransform> transform;
        readonly RefRO<PlayerMousePos> mouseInput;
        readonly RefRO<PlayerMoveInput> moveInput;
        readonly RefRO<PlayerMoveSpeed> moveSpeed;

        public void Move(float deltaTime)
        {
            transform.ValueRW.Position.xz += moveInput.ValueRO.Value * moveSpeed.ValueRO.Value * deltaTime;
        }

        public void Rotate()
        {
            float3 mousePos = new float3(mouseInput.ValueRO.Value.x, transform.ValueRO.Position.y, mouseInput.ValueRO.Value.y);
            transform.ValueRW.Rotation = quaternion.LookRotation(MathHelpers.GetDirection(transform.ValueRO.Position, mousePos), math.up());
        }
    }
}