using SpaceShooter.Components;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceShooter.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial class PlayerInputSystem: SystemBase
    {
        MainInput mainInput;
        Entity _playerEntity;
        
        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
            RequireForUpdate<PlayerMoveInput>();

            mainInput = new MainInput();
        }
        protected override void OnStartRunning()
        {
            mainInput.Enable();
            //mainInput.Player.Shoot.performed += OnPlayerShoot;
            
            _playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        }
        protected override void OnUpdate()
        {
            Vector2 MoveInput = mainInput.Player.Move.ReadValue<Vector2>();
            SystemAPI.SetSingleton(new PlayerMoveInput { Value = MoveInput });
        }



        protected override void OnStopRunning()
        {
           // mainInput.Player.Shoot.performed -= OnPlayerShoot;
            mainInput.Disable();
            
            _playerEntity = Entity.Null;
        }

        private void OnPlayerShoot(InputAction.CallbackContext obj)
        {
            if (!SystemAPI.Exists(_playerEntity)) return;
            
            SystemAPI.SetComponentEnabled<FireProjectileTag>(_playerEntity, true);
        }
    }
}