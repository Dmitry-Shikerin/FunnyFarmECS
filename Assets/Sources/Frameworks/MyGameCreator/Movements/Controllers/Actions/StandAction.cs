using System;
using JetBrains.Annotations;
using Sources.BoundedContexts.Movements.Controllers.Actions.Interfaces;
using Sources.BoundedContexts.Movements.Domain.Types;
using Sources.BoundedContexts.Movements.Presentation.Views.Implementation;
using Sources.Frameworks.MyGameCreator.Movements.Domain.Models;
using Sources.OldBoundedContexts.Movements.Presentation.Views.Implementation;
using UnityEngine;

namespace Sources.BoundedContexts.Movements.Controllers.Actions
{
    public class StandAction : IAction
    {
        private readonly Movement _movement;
        private readonly MovementView _view;

        public StandAction(Movement movement, MovementView view)
        {
            _movement = movement ?? throw new ArgumentNullException(nameof(movement));
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Execute()
        {
            _movement.ChangeStandState();
            float stand = _movement.StandState == StandState.Stand ? 1 : 0.5f;
            Debug.Log(_movement.StandState.ToString());
            _view.MovementAnimation.SetStand(stand);
        }
    }
}