using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.BoundedContexts.Dogs.Domain;
using Sources.BoundedContexts.Dogs.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.BoundedContexts.Trucks.Domain;
using Sources.BoundedContexts.Trucks.Presentation;
using Sources.Frameworks.GameServices.Cameras.Domain;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.MVPPassiveView.Controllers.Implementation;
using UnityEngine;

namespace Sources.BoundedContexts.Dogs.Controllers
{
    public class DogPresenter : PresenterBase
    {
        private readonly Dog _dog;
        private readonly DogView _view;
        private readonly ICameraService _cameraService;
        private readonly ISelectableService _selectableService;

        private CancellationTokenSource _token;

        public DogPresenter(
            string id, 
            DogView view, 
            IEntityRepository entityRepository,
            ICameraService cameraService,
            ISelectableService selectableService)
        {
            _dog = entityRepository.Get<Dog>(id);
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public override void Enable()
        {
            _token = new CancellationTokenSource();
            _dog.Selected += SelectView;
        }

        public override void Disable()
        {
            _token.Cancel();
            _dog.Selected -= SelectView;
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