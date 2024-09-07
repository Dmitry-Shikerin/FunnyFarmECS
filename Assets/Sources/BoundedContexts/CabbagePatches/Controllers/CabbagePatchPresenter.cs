using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;
using Sources.BoundedContexts.CabbagePatches.Domain;
using Sources.BoundedContexts.CabbagePatches.Presentation;
using Sources.BoundedContexts.Inventories.Domain;
using Sources.BoundedContexts.Items.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.Frameworks.GameServices.Cameras.Domain;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.MVPPassiveView.Controllers.Implementation;
using Sources.Frameworks.Utils.Extensions;
using UnityEngine;

namespace Sources.BoundedContexts.CabbagePatches.Controllers
{
    public class CabbagePatchPresenter : PresenterBase
    {
        private readonly CabbagePatch _cabbagePatch;
        private readonly Inventory _inventory;
        private readonly CabbagePatchView _view;
        private readonly ICameraService _cameraService;
        private readonly ISelectableService _selectableService;
        
        private CancellationTokenSource _token;

        public CabbagePatchPresenter(
            string id, 
            IEntityRepository entityRepository, 
            CabbagePatchView view,
            ICameraService cameraService,
            ISelectableService selectableService)
        {
            _cabbagePatch = entityRepository.Get<CabbagePatch>(id);
            _inventory = entityRepository.Get<Inventory>(ModelId.Inventory);
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public override void Enable()
        {
            _token = new CancellationTokenSource();
            _view.SowButton.onClickEvent.AddListener(Sow);
            _view.HarvestButton.onClickEvent.AddListener(Harvest);
            _view.SelectableButton.onClickEvent.AddListener(SelectView);
        }

        private void SelectView() =>
            _selectableService.Select(_view);

        public override void Disable()
        {
            _token.Cancel();
            _view.SowButton.onClickEvent.RemoveListener(Sow);
            _view.HarvestButton.onClickEvent.RemoveListener(Harvest);
            _view.SelectableButton.onClickEvent.RemoveListener(SelectView);
        }

        private async void Sow()
        {
            if (_cabbagePatch.CanGrow == false)
                return;
            
            _cabbagePatch.CanGrow = false;
            SetStartScale();
            _view.ProgressBarr.SetFillAmount(0);
            Show();
            await Grow(_token.Token);
        }

        private void Harvest()
        {
            if (_cabbagePatch.HasGrownUp == false)
                return;
            
            Hide();
            _inventory.Add(ModelId.Pumpkin, _cabbagePatch.PumpkinsCount);
            _cabbagePatch.CanGrow = true;
            _cabbagePatch.HasGrownUp = false;
            _cabbagePatch.PumpkinsCount = 0;
        }

        private async UniTask Grow(CancellationToken token)
        {
            while (_view.Cabbages.Any(item => item.transform.localScale != item.StartScale))
            {
                foreach (ItemView item in _view.Cabbages)
                {
                    item.transform.localScale = Vector3.MoveTowards(
                        item.transform.localScale, item.StartScale, 0.005f);
                }

                float filled = _view.Cabbages[0]
                    .transform
                    .localScale
                    .x.
                    FloatToPercent(_view.Cabbages[0].StartScale.x - 0.5f, _view.Cabbages[0].StartScale.x)
                    .FloatPercentToUnitPercent();
                Debug.Log($"{filled}");
                _view.ProgressBarr.SetFillAmount(filled);
                
                await UniTask.Yield(token);
            }

            _cabbagePatch.HasGrownUp = true;
            //TODO сделать это значение изменямым
            _cabbagePatch.PumpkinsCount = 3;
        }

        private void Show() =>
            _view.Cabbages.ForEach(item => item.Show());

        private void Hide() =>
            _view.Cabbages.ForEach(item => item.Hide());

        private void SetStartScale() =>
            _view.Cabbages.ForEach(item => item.SetScale(item.StartScale - new Vector3(0.5f, 0.5f, 0.5f)));

        public async void Select()
        {
            _cameraService.ShowCamera(CameraId.FirstPumpkins);
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