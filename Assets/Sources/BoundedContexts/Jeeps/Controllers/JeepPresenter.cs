using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sources.BoundedContexts.Jeeps.Domain;
using Sources.BoundedContexts.Jeeps.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.Frameworks.GameServices.Cameras.Domain;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.MVPPassiveView.Controllers.Implementation;
using UnityEngine;

namespace Sources.BoundedContexts.Jeeps.Controllers
{
    public class JeepPresenter : PresenterBase
    {
        private readonly Jeep _jeep;
        private readonly JeepView _view;
        private readonly ICameraService _cameraService;
        private readonly ISelectableService _selectableService;

        private CancellationTokenSource _token;

        public JeepPresenter(
            string id, 
            JeepView view, 
            IEntityRepository entityRepository,
            ICameraService cameraService,
            ISelectableService selectableService)
        {
            _jeep = entityRepository.Get<Jeep>(id);
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public override void Enable()
        {
            _token = new CancellationTokenSource();
            _jeep.Selected += SelectView;
        }

        public override void Disable()
        {
            _token.Cancel();
            _jeep.Selected -= SelectView;
        }

        private void SelectView() =>
            _selectableService.Select(_view);

        public async void Select()
        {
            Debug.Log($"Select Jeep");
            _cameraService.ShowCamera(CameraId.Jeep);
             _view.HighlightEffect.highlighted = true;
             await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
             _view.HighlightEffect.HitFX();
        }

        public void Deselect()
        {
             _view.HighlightEffect.highlighted = false;
        }
    }
}