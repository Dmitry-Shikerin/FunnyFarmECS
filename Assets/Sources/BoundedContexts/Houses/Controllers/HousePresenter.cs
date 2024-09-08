using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.BoundedContexts.Houses.Domain;
using Sources.BoundedContexts.Houses.Presentation;
using Sources.BoundedContexts.Jeeps.Domain;
using Sources.BoundedContexts.Jeeps.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.Frameworks.GameServices.Cameras.Domain;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.MVPPassiveView.Controllers.Implementation;
using UnityEngine;

namespace Sources.BoundedContexts.Houses.Controllers
{
    public class HousePresenter : PresenterBase
    {
        private readonly House _house;
        private readonly HouseView _view;
        private readonly ICameraService _cameraService;
        private readonly ISelectableService _selectableService;

        private CancellationTokenSource _token;

        public HousePresenter(
            string id, 
            HouseView view, 
            IEntityRepository entityRepository,
            ICameraService cameraService,
            ISelectableService selectableService)
        {
            _house = entityRepository.Get<House>(id);
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public override void Enable()
        {
            _token = new CancellationTokenSource();
            _view.SelectButton.onClickEvent.AddListener(SelectView);
        }

        public override void Disable()
        {
            _token.Cancel();
            _view.SelectButton.onClickEvent.RemoveListener(SelectView);
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