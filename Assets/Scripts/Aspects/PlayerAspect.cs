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
        readonly RefRO<PlayerClampDimensions> moveClamp;
        readonly RefRW<PlayerHealth> health;
        readonly RefRO<PlayerMoveSpeed> moveSpeed;
        
        readonly DynamicBuffer<PlayerDamageBufferElement> playerDamageBuffer;

        public void Move(float deltaTime)
        {
            float2 NewPosXZ =  transform.ValueRW.Position.xz + moveInput.ValueRO.Value * moveSpeed.ValueRO.Value * deltaTime;
            NewPosXZ = math.clamp(NewPosXZ, -moveClamp.ValueRO.Value, moveClamp.ValueRO.Value);
            transform.ValueRW.Position.xz = NewPosXZ;
        }

        public void Rotate()
        {
            float3 mousePos = new float3(mouseInput.ValueRO.Value.x, transform.ValueRO.Position.y, mouseInput.ValueRO.Value.y);
            transform.ValueRW.Rotation = quaternion.LookRotationSafe(MathHelpers.GetDirection(transform.ValueRO.Position,mousePos ), math.up());
        }

        public void GetDamaged()
        {
            foreach (PlayerDamageBufferElement playerDamageBufferElement in playerDamageBuffer)
            {
                health.ValueRW.CurrentHealth -= playerDamageBufferElement.Value;
            }

            playerDamageBuffer.Clear();

        }
    }
}