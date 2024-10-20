using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.BoundedContexts.Cats.Domain;
using Sources.BoundedContexts.Cats.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.Frameworks.GameServices.Cameras.Domain;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.MVPPassiveView.Controllers.Implementation;
using UnityEngine;

namespace Sources.BoundedContexts.Cats.Controllers
{
    public class CatHousePresenter : PresenterBase
    {
        private readonly Cat _cat;
        private readonly CatHouseView _houseView;
        private readonly ICameraService _cameraService;
        private readonly ISelectableService _selectableService;

        private CancellationTokenSource _token;

        public CatHousePresenter(
            string id, 
            CatHouseView houseView, 
            IEntityRepository entityRepository,
            ICameraService cameraService,
            ISelectableService selectableService)
        {
            _cat = entityRepository.Get<Cat>(id);
            _houseView = houseView ?? throw new ArgumentNullException(nameof(houseView));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public override void Enable()
        {
            _token = new CancellationTokenSource();
            _cat.Selected += SelectView;
        }

        public override void Disable()
        {
            _token.Cancel();
            _cat.Selected -= SelectView;
        }

        private void SelectView() =>
            _selectableService.Select(_houseView);

        public async void Select()
        {
            Debug.Log($"Select Jeep");
            _cameraService.ShowCamera(CameraId.Jeep);
            _houseView.HighlightEffect.highlighted = true;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            _houseView.HighlightEffect.HitFX();
        }

        public void Deselect()
        {
            _houseView.HighlightEffect.highlighted = false;
        }
    }
}