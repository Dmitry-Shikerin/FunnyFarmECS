using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;
using Sources.BoundedContexts.CabbagePatches.Domain;
using Sources.BoundedContexts.CabbagePatches.Presentation;
using Sources.BoundedContexts.Inventories.Domain;
using Sources.BoundedContexts.Items.Presentation;
using Sources.BoundedContexts.OnionPatches.Domain;
using Sources.BoundedContexts.OnionPatches.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.Frameworks.GameServices.Cameras.Domain;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.MVPPassiveView.Controllers.Implementation;
using Sources.Frameworks.Utils.Extensions;
using UnityEngine;

namespace Sources.BoundedContexts.OnionPatches.Controllers
{
    public class OnionPatchPresenter : PresenterBase
    {
        private readonly OnionPatch _onionPatch;
        private readonly Inventory _inventory;
        private readonly OnionPatchView _view;
        private readonly ICameraService _cameraService;
        private readonly ISelectableService _selectableService;
        
        private CancellationTokenSource _token;

        public OnionPatchPresenter(
            string id, 
            IEntityRepository entityRepository, 
            OnionPatchView view,
            ICameraService cameraService,
            ISelectableService selectableService)
        {
            _onionPatch = entityRepository.Get<OnionPatch>(id);
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
            _onionPatch.Selected += SelectView;
        }

        private void SelectView() =>
            _selectableService.Select(_view);

        public override void Disable()
        {
            _token.Cancel();
            // _view.SowButton.onClickEvent.RemoveListener(Sow);
            // _view.HarvestButton.onClickEvent.RemoveListener(Harvest);
            _onionPatch.Selected -= SelectView;
        }

        private async void Sow()
        {
            if (_onionPatch.CanGrow == false)
                return;
            
            _onionPatch.CanGrow = false;
            SetStartScale();
            _view.ProgressBarr.SetFillAmount(0);
            Show();
            await Grow(_token.Token);
        }

        private void Harvest()
        {
            if (_onionPatch.HasGrownUp == false)
                return;
            
            Hide();
            _inventory.Add(ModelId.Pumpkin, _onionPatch.PumpkinsCount);
            _onionPatch.CanGrow = true;
            _onionPatch.HasGrownUp = false;
            _onionPatch.PumpkinsCount = 0;
        }

        private async UniTask Grow(CancellationToken token)
        {
            while (_view.Onions.Any(item => item.transform.localScale != item.StartScale))
            {
                foreach (ItemView item in _view.Onions)
                {
                    item.transform.localScale = Vector3.MoveTowards(
                        item.transform.localScale, item.StartScale, 0.005f);
                }

                float filled = _view.Onions[0]
                    .transform
                    .localScale
                    .x.
                    FloatToPercent(_view.Onions[0].StartScale.x - 0.5f, _view.Onions[0].StartScale.x)
                    .FloatPercentToUnitPercent();
                Debug.Log($"{filled}");
                _view.ProgressBarr.SetFillAmount(filled);
                
                await UniTask.Yield(token);
            }

            _onionPatch.HasGrownUp = true;
            //TODO сделать это значение изменямым
            _onionPatch.PumpkinsCount = 3;
        }

        private void Show() =>
            _view.Onions.ForEach(item => item.Show());

        private void Hide() =>
            _view.Onions.ForEach(item => item.Hide());

        private void SetStartScale() =>
            _view.Onions.ForEach(item => item.SetScale(item.StartScale - new Vector3(0.5f, 0.5f, 0.5f)));

        public async void Select()
        {
            _cameraService.ShowCamera(CameraId.Onion);
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