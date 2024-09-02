using JetBrains.Annotations;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.BoundedContexts.Movements.Domain.Types;
using Sources.BoundedContexts.Movements.Presentation.Views.Implementation;
using Sources.OldBoundedContexts.Movements.Domain.Types;

namespace Sources.BoundedContexts.Movements.Infrastructure.NodeCanvasIntegration.Actions
{
    [UsedImplicitly]
    [Category("Custom/Movement")]
    public class SetMovementState : ActionTask
    {
        [RequiredField] public BBParameter<MovementView> _view;
        [RequiredField] public BBParameter<StateId> _stateId;
        [RequiredField] public BBParameter<MovementState> _movementState;
        
        protected override string OnInit()
        {
            return null;
        }

        protected override void OnExecute() =>
            _view.value.ChangeState(_movementState.value, _stateId.value);

        protected override void OnUpdate()
        {
        }
    }
}