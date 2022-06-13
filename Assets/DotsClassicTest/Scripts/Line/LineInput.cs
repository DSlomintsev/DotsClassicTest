using System;
using DotsClassicTest.Utils.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DotsClassicTest.Line
{
    public class LineInput : IDisposable
    {
        private const string PointerPositionActionName = "PointerPosition";

        public ActiveData<Vector2> PointerPosition = new();

        private PlayerInput _playerInput;

        public LineInput(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            AddListeners();
        }

        public void Dispose()
        {
            RemoveListeners();
            _playerInput = null;
        }

        private void AddListeners()
        {
            _playerInput.actions[PointerPositionActionName].performed += OnPointerPositionChange;
        }

        private void RemoveListeners()
        {
            _playerInput.actions[PointerPositionActionName].performed -= OnPointerPositionChange;
        }

        private void OnPointerPositionChange(InputAction.CallbackContext obj)
        {
            PointerPosition.Value = obj.ReadValue<Vector2>();
        }
    }
}