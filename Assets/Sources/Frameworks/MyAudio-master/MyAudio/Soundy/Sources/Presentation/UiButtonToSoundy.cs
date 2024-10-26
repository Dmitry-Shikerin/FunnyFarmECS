using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers.New;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Presentation
{
    [RequireComponent(typeof(UIButton))]
    public class UiButtonToSoundy : MonoBehaviour
    {
        [SerializeField] private UIButton _button;
        [SerializeField] private SoundyData _soundyData;

        private void OnEnable()
        {
            _button.onClickEvent.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClickEvent.RemoveListener(OnClick);
        }

        private void OnClick() =>
            SoundyManager.Play(_soundyData, true);
        
        [OnInspectorInit]
        private void SetButton() =>
            _button = GetComponent<UIButton>();
    }
}