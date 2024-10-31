using System.Collections.Generic;
using Doozy.Editor.EditorUI.Components;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using UnityEngine.Events;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces
{
    public interface ISoundyDataBaseView : IView<SoundyDataBaseWindowLayout>
    {
        public IReadOnlyList<FluidToggleButtonTab> DatabaseButtons { get; }
        public ISoundySettingsView SettingsView { get; }
        public ISoundDataBaseView SoundDataBaseView { get; }
        
        void AddDataBaseButton(string name, UnityAction callback);
        void SetSoundDataBase(ISoundDataBaseView dataBaseView);
        void RefreshDataBasesButtons();
        void RenameButtons();
        void UpdateDataBase();
        void ClearButtons();
        void AddSettings(ISoundySettingsView soundySettingsView);
    }
}