using System;
using System.Collections.Generic;
using Doozy.Editor.EditorUI;
using Doozy.Editor.EditorUI.Components;
using Doozy.Editor.EditorUI.Utils;
using Doozy.Runtime.UIElements.Extensions;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation.Base;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using UnityEditor;
using UnityEngine.Audio;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation
{
    public class SoundDataBaseView : EditorPresentableView<SoundDataBasePresenter, SoundDataBaseVisualElement>, ISoundDataBaseView
    {
        private List<ISoundGroupView> _soundGroups;
        private ISoundyDataBaseView _soundyDataBaseView;

        protected override void Initialize()
        {
            Root.NewSoundContentVisualElement.CreateButton.SetOnClick(() 
                => Presenter.CreateSoundGroup(Root.NewSoundContentVisualElement.SoundGroupTextField.value));
            // _headerVisualElement.PingAssetButton.SetOnClick(() 
            //     => Selection.activeObject = _presenter.GetDataBase());
            Root.HeaderVisualElement.RenameButton.SetOnClick(() => 
                Presenter.RenameDataBase(Root.HeaderVisualElement.SoundGroupTextField.value));
            Root.HeaderVisualElement.RemoveButton.SetOnClick(() => Presenter.RemoveDataBase());
            Root.AudioMixerVisualElement.ObjectField
                .RegisterCallback<ChangeEvent<AudioMixerGroup>>((value) 
                    => Presenter.ChangeAudioMixerGroup(value.newValue));
            _soundGroups = new List<ISoundGroupView>();
        }

        protected override void DisposeView()
        {
            _soundyDataBaseView.UpdateDataBase();
        }

        public void RemoveSoundGroup(ISoundGroupView soundGroupView)
        {
            _soundGroups.Remove(soundGroupView);
            Root.Remove(soundGroupView.Root);
        }

        public void SetName(string name) =>
            Root.HeaderVisualElement.SoundGroupTextField.value = name;

        public void SetAudioMixerGroup(AudioMixerGroup audioMixerGroup) =>
            Root.AudioMixerVisualElement.ObjectField.SetValueWithoutNotify(audioMixerGroup);

        public void RenameDataBaseButtons() =>
            _soundyDataBaseView.RenameButtons();
        
        public void SetSoundyDataBaseView(ISoundyDataBaseView view) =>
            _soundyDataBaseView = view ?? throw new ArgumentNullException(nameof(view));

        public void AddSoundGroup(ISoundGroupView soundGroupView)
        {
            Root.ScrollView
                .AddChild(soundGroupView.Root)
                .AddSpace(4);
            _soundGroups.Add(soundGroupView);
        }
    }
}