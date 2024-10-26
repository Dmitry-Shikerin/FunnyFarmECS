using System;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Enums;
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

        private void OnClick()
        {
            Action action = _soundyData.SoundSource switch
            {
                SoundSource.Soundy => () => SoundyManager.Play(_soundyData.DatabaseName, _soundyData.SoundName),
                SoundSource.AudioClip => () => SoundyManager.Play(_soundyData.AudioClip).SetOutputAudioMixerGroup(_soundyData.OutputAudioMixerGroup),
                SoundSource.MasterAudio =>  null, // no action, or throw an exception, or return a default value
                _ => null
            };
            
            action?.Invoke();
        }

        [OnInspectorInit]
        private void SetButton() =>
            _button = GetComponent<UIButton>();
    }
}