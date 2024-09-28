using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sirenix.Utilities;
using Sources.BoundedContexts.Inventories.Domain;
using Sources.BoundedContexts.Items.Presentation;
using Sources.BoundedContexts.PumpkinsPatchs.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.BoundedContexts.TomatoPatchs.Domain;
using Sources.BoundedContexts.TomatoPatchs.Presentation;
using Sources.Frameworks.GameServices.Cameras.Domain;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.MVPPassiveView.Controllers.Implementation;
using Sources.Frameworks.Utils.Extensions;
using UnityEngine;

namespace Sources.BoundedContexts.TomatoPatchs.Controllers
{
    public class TomatoPatchPresenter : PresenterBase
    {
        private readonly TomatoPatch _tomatoPatch;
        private readonly Inventory _inventory;
        private readonly TomatoPatchView _view;
        private readonly ICameraService _cameraService;
        private readonly ISelectableService _selectableService;

        private CancellationTokenSource _token;

        public TomatoPatchPresenter(
            string id,
            TomatoPatchView view,
            IEntityRepository entityRepository,
            ICameraService cameraService,
            ISelectableService selectableService)
        {
            _tomatoPatch = entityRepository.Get<TomatoPatch>(id);
            _inventory = entityRepository.Get<Inventory>(ModelId.Inventory);
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public override void Enable()
        {
            _token = new CancellationTokenSource();
            // _view.SowButton.onClickEvent.AddListener(Sow);
            // _view.HarvestButton.onClickEvent.AddListener(Harvest);
            _view.SelectableButton.onClickEvent.AddListener(SelectView);
        }

        public override void Disable()
        {
            _token.Cancel();
            _view.SowButton.onClickEvent.RemoveListener(Sow);
            _view.HarvestButton.onClickEvent.RemoveListener(Harvest);
            _view.SelectableButton.onClickEvent.RemoveListener(SelectView);
        }

        private void SelectView() =>
            _selectableService.Select(_view);

        private async void Sow()
        {
            if (_tomatoPatch.CanGrow == false)
                return;
            
            _tomatoPatch.CanGrow = false;
            SetStartScale();
            _view.ProgressBarr.SetFillAmount(0);
            Show();
            await Grow(_token.Token);
        }

        private void Harvest()
        {
            if (_tomatoPatch.HasGrownUp == false)
                return;
            
            Hide();
            _inventory.Add(ModelId.Tomato, _tomatoPatch.PumpkinsCount);
            _tomatoPatch.CanGrow = true;
            _tomatoPatch.HasGrownUp = false;
            _tomatoPatch.PumpkinsCount = 0;
        }

        private async UniTask Grow(CancellationToken token)
        {
            while (_view.Pumpkins.Any(item => item.transform.localScale != item.StartScale))
            {
                foreach (ItemView item in _view.Pumpkins)
                {
                    item.transform.localScale = Vector3.MoveTowards(
                        item.transform.localScale, item.StartScale, 0.005f);
                }

                float filled = _view.Pumpkins[0]
                    .transform
                    .localScale
                    .x.
                    FloatToPercent(_view.Pumpkins[0].StartScale.x - 0.5f, _view.Pumpkins[0].StartScale.x)
                    .FloatPercentToUnitPercent();
                Debug.Log($"{filled}");
                _view.ProgressBarr.SetFillAmount(filled);
                
                await UniTask.Yield(token);
            }

            _tomatoPatch.HasGrownUp = true;
            //TODO сделать это значение изменямым
            _tomatoPatch.PumpkinsCount = 3;
        }

        private void Show() =>
            _view.Pumpkins.ForEach(item => item.Show());

        private void Hide() =>
            _view.Pumpkins.ForEach(item => item.Hide());

        private void SetStartScale() =>
            _view.Pumpkins.ForEach(item => item.SetScale(item.StartScale - new Vector3(0.5f, 0.5f, 0.5f)));

        public async void Select()
        {
            _cameraService.ShowCamera(CameraId.Tomato);
             _view.HighlightEffect.highlighted = true;
             await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
             _view.HighlightEffect.HitFX();
             Debug.Log($"Select tomato patch");
        }

        public void Deselect()
        {
             _view.HighlightEffect.highlighted = false;
        }
    }
}