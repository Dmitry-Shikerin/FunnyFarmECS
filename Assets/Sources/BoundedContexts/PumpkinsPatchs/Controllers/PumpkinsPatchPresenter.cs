using System;
using JetBrains.Annotations;
using Sources.BoundedContexts.PumpkinsPatchs.Domain;
using Sources.BoundedContexts.PumpkinsPatchs.Presentation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.MVPPassiveView.Controllers.Implementation;
using UnityEngine;

namespace Sources.BoundedContexts.PumpkinsPatchs.Controllers
{
    public class PumpkinsPatchPresenter : PresenterBase
    {
        private readonly PumpkinPatchView _view;
        private readonly PumpkinPatch _pumpkinPatch;

        public PumpkinsPatchPresenter(
            string id,
            PumpkinPatchView view, 
            IEntityRepository entityRepository)
        {
            _pumpkinPatch = entityRepository.Get<PumpkinPatch>(id);
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public override void Enable()
        {
            Debug.Log("PumpkinsPatchPresenter Enable");
        }

        public override void Disable()
        {
            base.Disable();
        }
    }
}