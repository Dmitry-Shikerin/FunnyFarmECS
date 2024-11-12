using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;
using Sources.BoundedContexts.Inventories.Domain;
using Sources.BoundedContexts.Items.Presentation;
using Sources.BoundedContexts.PumpkinsPatchs.Domain;
using Sources.BoundedContexts.PumpkinsPatchs.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.Frameworks.GameServices.Cameras.Domain;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.MVPPassiveView.Controllers.Implementation;
using Sources.Frameworks.Utils.Extensions;
using UnityEngine;

namespace Sources.BoundedContexts.PumpkinsPatchs.Controllers
{
    public class PumpkinsPatchPresenter : PresenterBase
    {
        private readonly PumpkinPatch _pumpkinPatch;
        private readonly Inventory _inventory;
        private readonly PumpkinPatchView _view;
        private readonly ICameraService _cameraService;
        private readonly ISelectableService _selectableService;

        private CancellationTokenSource _token;

        public PumpkinsPatchPresenter(
            string id,
            PumpkinPatchView view,
            IEntityRepository entityRepository,
            ICameraService cameraService,
            ISelectableService selectableService)
        {
            _pumpkinPatch = entityRepository.Get<PumpkinPatch>(id);
            _inventory = entityRepository.Get<Inventory>(ModelId.Inventory);
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public override void Enable()
        {
            _token = new CancellationTokenSource();
            _pumpkinPatch.Selected += SelectView;
        }

        private void SelectView()
        {
            Debug.Log(nameof(SelectView));
            _selectableService.Select(_view);
        }

        public override void Disable()
        {
            _token.Cancel();
            _pumpkinPatch.Selected -= SelectView;
        }

        // private async void Sow()
        // {
        //     if (_pumpkinPatch.CanGrow == false)
        //         return;
        //     
        //     _pumpkinPatch.CanGrow = false;
        //     SetStartScale();
        //     _view.ProgressBarr.SetFillAmount(0);
        //     Show();
        //     await Grow(_token.Token);
        // }
        //
        // private void Harvest()
        // {
        //     if (_pumpkinPatch.HasGrownUp == false)
        //         return;
        //     
        //     Hide();
        //     _inventory.Add(ModelId.Pumpkin, _pumpkinPatch.PumpkinsCount);
        //     _pumpkinPatch.CanGrow = true;
        //     _pumpkinPatch.HasGrownUp = false;
        //     _pumpkinPatch.PumpkinsCount = 0;
        // }
        //
        // private async UniTask Grow(CancellationToken token)
        // {
        //     while (_view.Pumpkins.Any(item => item.transform.localScale != item.StartScale))
        //     {
        //         foreach (ItemView item in _view.Pumpkins)
        //         {
        //             item.transform.localScale = Vector3.MoveTowards(
        //                 item.transform.localScale, item.StartScale, 0.005f);
        //         }
        //
        //         float filled = _view.Pumpkins[0]
        //             .transform
        //             .localScale
        //             .x.
        //             FloatToPercent(_view.Pumpkins[0].StartScale.x - 0.5f, _view.Pumpkins[0].StartScale.x)
        //             .FloatPercentToUnitPercent();
        //         Debug.Log($"{filled}");
        //         _view.ProgressBarr.SetFillAmount(filled);
        //         
        //         await UniTask.Yield(token);
        //     }
        //
        //     _pumpkinPatch.HasGrownUp = true;
        //     //TODO сделать это значение изменямым
        //     _pumpkinPatch.PumpkinsCount = 3;
        // }
        //
        // private void Show() =>
        //     _view.Pumpkins.ForEach(item => item.Show());
        //
        // private void Hide() =>
        //     _view.Pumpkins.ForEach(item => item.Hide());
        //
        // private void SetStartScale() =>
        //     _view.Pumpkins.ForEach(item => item.SetScale(item.StartScale - new Vector3(0.5f, 0.5f, 0.5f)));

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