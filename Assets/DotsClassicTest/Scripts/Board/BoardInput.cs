using System;
using DotsClassicTest.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DotsClassicTest.Board
{
    public class BoardInput : IDisposable
    {
        private const string SelectionActionName = "Selection";
        private const string PointerPositionActionName = "PointerPosition";
        
        public event Action StartSelection;
        public event Action EndSelection;
        public Vector2 MousePosition { get; private set; }

        private PlayerInput _playerInput;

        public BoardInput(PlayerInput playerInput)
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
            _playerInput.actions[SelectionActionName].performed += OnStartSelection;
            _playerInput.actions[SelectionActionName].canceled += OnEndSelection;

            _playerInput.actions[PointerPositionActionName].performed += OnPointerPositionChange;
        }

        private void RemoveListeners()
        {
            _playerInput.actions[SelectionActionName].performed -= OnStartSelection;
            _playerInput.actions[SelectionActionName].canceled -= OnEndSelection;

            _playerInput.actions[PointerPositionActionName].performed -= OnPointerPositionChange;
        }

        private void OnStartSelection(InputAction.CallbackContext obj)
        {
            StartSelection.Call();
        }

        private void OnEndSelection(InputAction.CallbackContext obj)
        {
            EndSelection.Call();
        }

        private void OnPointerPositionChange(InputAction.CallbackContext obj)
        {
            MousePosition = obj.ReadValue<Vector2>();
        }
    }
}