using System;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.Domain.Models.Constants.LayerMasks;
using Sources.Frameworks.GameServices.InputServices.Inputs;
using Sources.Frameworks.GameServices.InputServices.InputServices;
using Sources.Frameworks.GameServices.Pauses.Services.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.Frameworks.GameServices.InputServices
{
    public class NewInputService : IInputService, IInputServiceUpdater
    {
        private readonly IPauseService _pauseService;
        private InputManager _inputManager;
        private float _speed;

        public NewInputService(IPauseService pauseService)
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            InputData = new InputData();
        }

        public InputData InputData { get; }

        public void Initialize()
        {
            _inputManager = new InputManager();
            _inputManager.Enable();
            _inputManager.Gameplay.Stand.performed += UpdateStandState;
            _inputManager.Gameplay.Click.performed += UpdateSelectable;
        }

        private void UpdateSelectable(InputAction.CallbackContext obj)
        {
            Debug.Log($"UpdateSelectable");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(
                    ray, out RaycastHit raycastHit, float.MaxValue, Layer.Selectable) == false)
                return;

            Debug.Log($"RaycastHit: {raycastHit.collider.name}");
            
            if (raycastHit.collider.TryGetComponent(out ISelectableItem item) == false)
                return;

            Debug.Log($"Invoke SelectItem");
            InputData.InvokeSelectItem(item);
        }

        public void Destroy()
        {
            _inputManager.Disable();
            _inputManager.Gameplay.Stand.performed -= UpdateStandState;
            _inputManager.Gameplay.Click.performed -= UpdateSelectable;
        }

        public void Update(float deltaTime)
        {
            if (_pauseService.IsPaused)
                return;

            UpdateMovement();
            UpdateAttack();
            UpdatePointerClick();
        }

        private void UpdatePointerClick()
        {
            // InputData.PointerPosition = Vector3.zero;
            //
            if (Input.GetMouseButtonDown(0) == false)
                return;

            if (TryGetLook(out Vector3 lookDirection) == false)
                return;
            
            InputData.PointerPosition = lookDirection;
        }
        
        private void UpdateStandState(InputAction.CallbackContext context) =>
            InputData.InvokeStand();

        private void UpdateAttack() =>
            InputData.IsAttacking = _inputManager.Gameplay.Attack.IsPressed();

        private void UpdateMovement()
        {
            Vector2 input = _inputManager.Gameplay.Movement.ReadValue<Vector2>();
            float speed = _inputManager.Gameplay.Run.ReadValue<float>();

            Vector3 lookDirection = Vector3.zero;

            if (TryGetLook(out Vector3 look))
                lookDirection = look;

            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;

            float angle = Vector3.SignedAngle(Vector3.forward, cameraForward, Vector3.up);
            Vector3 moveDirection = Quaternion.Euler(0, angle, 0) * new Vector3(input.x, 0, input.y);

            InputData.MoveDirection = moveDirection;
            InputData.LookPosition = lookDirection;
            InputData.Speed = speed;
        }

        private bool TryGetLook(out Vector3 lookDirection)
        {
            lookDirection = Vector3.zero;
            Ray cameraPosition = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(
                    cameraPosition, out RaycastHit raycastHit, float.MaxValue, Layer.Plane) == false)
                return false;

            lookDirection = raycastHit.point;
            
            return true;
        }
    }
}