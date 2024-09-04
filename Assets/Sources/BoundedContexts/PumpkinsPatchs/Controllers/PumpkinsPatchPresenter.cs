using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;
using Sources.BoundedContexts.Items.Presentation;
using Sources.BoundedContexts.PumpkinsPatchs.Domain;
using Sources.BoundedContexts.PumpkinsPatchs.Presentation;
using Sources.Frameworks.GameServices.Repositories.Services.Interfaces;
using Sources.Frameworks.MVPPassiveView.Controllers.Implementation;
using Sources.Frameworks.Utils.Extensions;
using UnityEngine;

namespace Sources.BoundedContexts.PumpkinsPatchs.Controllers
{
    public class PumpkinsPatchPresenter : PresenterBase
    {
        private readonly PumpkinPatchView _view;
        private readonly PumpkinPatch _pumpkinPatch;

        private CancellationTokenSource _token;

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
            _token = new CancellationTokenSource();
            _view.SowButton.onClickEvent.AddListener(Sow);
        }

        public override void Disable()
        {
            _token.Cancel();
            _view.SowButton.onClickEvent.RemoveListener(Sow);
        }

        private async void Sow()
        {
            Debug.Log($"On Click");
            SetStartScale();
            _view.ProgressBarr.SetFillAmount(0);
            Show();
            await Grow(_token.Token);
        }

        private void Harvest()
        {
            Hide();
        }

        private async UniTask Grow(CancellationToken token)
        {
            Debug.Log($"Start grow");
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

            Debug.Log($"End grow");
        }

        private void Show() =>
            _view.Pumpkins.ForEach(item => item.Show());

        private void Hide() =>
            _view.Pumpkins.ForEach(item => item.Hide());

        private void SetStartScale() =>
            _view.Pumpkins.ForEach(item => item.SetScale(item.StartScale - new Vector3(0.5f, 0.5f, 0.5f)));
    }
}