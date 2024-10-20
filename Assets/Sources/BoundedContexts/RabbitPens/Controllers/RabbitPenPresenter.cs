using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;
using Sources.BoundedContexts.CowPens.Domain;
using Sources.BoundedContexts.CowPens.Presentation;
using Sources.BoundedContexts.Inventories.Domain;
using Sources.BoundedContexts.Items.Presentation;
using Sources.BoundedContexts.RabbitPens.Domain;
using Sources.BoundedContexts.RabbitPens.Presentation;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.Frameworks.GameServices.Cameras.Domain;
using Sources.Frameworks.GameServices.Cameras.Infrastructure.Services.Interfaces;
using Sources.Frameworks.GameServices.Loads.Domain.Constant;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.MVPPassiveView.Controllers.Implementation;
using Sources.Frameworks.Utils.Extensions;
using UnityEngine;

namespace Sources.BoundedContexts.RabbitPens.Controllers
{
    public class RabbitPenPresenter : PresenterBase
    {
        private readonly RabbitPen _rabbitPen;
        private readonly Inventory _inventory;
        private readonly RabbitPenView _view;
        private readonly ICameraService _cameraService;
        private readonly ISelectableService _selectableService;

        private CancellationTokenSource _token;

        public RabbitPenPresenter(
            string id,
            RabbitPenView view,
            IEntityRepository entityRepository,
            ICameraService cameraService,
            ISelectableService selectableService)
        {
            _rabbitPen = entityRepository.Get<RabbitPen>(id);
            _inventory = entityRepository.Get<Inventory>(ModelId.Inventory);
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _selectableService = selectableService ?? throw new ArgumentNullException(nameof(selectableService));
        }

        public override void Enable()
        {
            _token = new CancellationTokenSource();
            _rabbitPen.Selected += SelectView;
        }

        public override void Disable()
        {
            _token.Cancel();
            _rabbitPen.Selected -= SelectView;
        }

        private void SelectView() =>
            _selectableService.Select(_view);

        private async void Sow()
        {
            if (_rabbitPen.CanGrow == false)
                return;
            
            _rabbitPen.CanGrow = false;
            SetStartScale();
            _view.ProgressBarr.SetFillAmount(0);
            Show();
            await Grow(_token.Token);
        }

        private void Harvest()
        {
            if (_rabbitPen.HasGrownUp == false)
                return;
            
            Hide();
            _inventory.Add(ModelId.Tomato, _rabbitPen.PumpkinsCount);
            _rabbitPen.CanGrow = true;
            _rabbitPen.HasGrownUp = false;
            _rabbitPen.PumpkinsCount = 0;
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

            _rabbitPen.HasGrownUp = true;
            //TODO сделать это значение изменямым
            _rabbitPen.PumpkinsCount = 3;
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