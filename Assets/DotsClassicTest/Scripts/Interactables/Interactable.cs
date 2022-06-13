using System;
using DotsClassicTest.Utils;
using UnityEngine;

namespace DotsClassicTest.Interactables
{
    public class Interactable:MonoBehaviour, IInteractable
    {
        private Action _action;
        
        public void Init(Action action)
        {
            _action = action;
        }

        public void DoAction()
        {
            _action.Call();
        }
    }
}