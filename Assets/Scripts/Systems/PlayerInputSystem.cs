using Helpers;
using SpaceShooter.Components;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;
using UnityEngine.InputSystem;
using Ray = UnityEngine.Ray;
using RaycastHit = Unity.Physics.RaycastHit;

namespace SpaceShooter.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial class PlayerInputSystem: SystemBase
    {
        Camera mainCamera;

        MainInput mainInput;
        Entity playerEntity;
        
        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
            RequireForUpdate<PlayerMoveInput>();
            RequireForUpdate<PlayerMousePos>();
            mainCamera = Camera.main;

            mainInput = new MainInput();
        }
        protected override void OnStartRunning()
        {
            mainInput.Enable();
            mainInput.Player.Shoot.performed += OnPlayerShoot;

            playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();

        }
        protected override void OnUpdate()
        {
            SetPlayerMoveInput();
            SetPlayerMousePosition();
        }



        protected override void OnStopRunning()
        {
            mainInput.Player.Shoot.performed -= OnPlayerShoot;

            mainInput.Disable();
            playerEntity = Entity.Null;
        }

        void SetPlayerMoveInput()
        {
            Vector2 MoveInput = mainInput.Player.Move.ReadValue<Vector2>();
            SystemAPI.SetSingleton(new PlayerMoveInput { Value = MoveInput });
        }

        void SetPlayerMousePosition()
        {
            //Read input
            Vector2 MouseInput =  mainInput.Player.MousePos.ReadValue<Vector2>();
            //Get physics singleton
            PhysicsWorldSingleton worldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>(); 
            //Shoot ray
            Ray ray = Camera.main.ScreenPointToRay(MouseInput);
            //CreateRayInput
            RaycastInput raycastInput = new()
            {
                Start = ray.origin,
                End = ray.GetPoint(1000),
                Filter = new CollisionFilter
                {
                    BelongsTo =(uint) CollisionLayers.Ground,
                    CollidesWith =(uint) CollisionLayers.Ground,
                }
            };
            if (worldSingleton.CastRay(raycastInput, out RaycastHit raycastHit))
            {
                SystemAPI.SetSingleton(new PlayerMousePos { Value = raycastHit.Position.xz });
            }
        }
        private void OnPlayerShoot(InputAction.CallbackContext obj)
        {
            if (!SystemAPI.Exists(playerEntity)) return;
            
            SystemAPI.SetComponentEnabled<FireProjectileTag>(playerEntity, true);
        }
    }
}