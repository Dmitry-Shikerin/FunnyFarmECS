using System;
using Sources.BoundedContexts.SelectableItems.Presentation;
using Sources.Frameworks.StateMachines.ContextStateMachines.Interfaces.Contexts;
using UnityEngine;

namespace Sources.Frameworks.GameServices.InputServices.Inputs
{
    public class InputData : IContext
    {
        public event Action StandChanged;
        public event Action<ISelectableItem> SelectItem;
            
        public Vector3 MoveDirection { get; set; }
        public Vector3 LookPosition { get; set; }
        public Vector3 PointerPosition { get; set; }
        public float Speed { get; set; }
        public bool IsAttacking { get; set; }

        public void InvokeStand() =>
            StandChanged?.Invoke();
        
        public void InvokeSelectItem(ISelectableItem item) =>
            SelectItem?.Invoke(item);
    }
}