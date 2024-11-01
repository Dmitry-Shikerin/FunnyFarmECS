using System;
using System.Collections.Generic;
using Doozy.Editor.EditorUI.Components;
using Doozy.Runtime.UIElements.Extensions;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation.Base;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using UnityEngine.Events;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation
{
    public class SoundyDataBaseView : EditorPresentableView<SoundyDataBasePresenter, SoundyDataBaseWindowLayout>, ISoundyDataBaseView
    {
        public IEnumerable<FluidToggleButtonTab> DatabaseButtons => Root.DatabaseButtons.Values;
        public ISoundySettingsView SettingsView { get; private set; }
        public ISoundDataBaseView SoundDataBaseView { get; private set; }

        protected override void Initialize()
        {
            Root.SettingsButton.SetOnClick(() => Presenter.OpenSettings());
            Root.NewDataBaseButton.SetOnClick(() => Presenter.CreateNewDataBase());
            Root.RefreshButton.SetOnClick(() => Presenter.RefreshDataBases());
        }

        protected override void DisposeView()
        {
        }

        public void RefreshDataBasesButtons()
        {
            Root.RefreshDataBasesButtons();
        }

        public void RenameButtons()
        {
            Presenter.RenameButtons();
        }

        public void UpdateDataBase()
        {
            Presenter.UpdateDataBase();
        }

        public void ClearButtons()
        {
            foreach (FluidToggleButtonTab button in DatabaseButtons)
            {
                Root.sideMenu.buttons.Remove(button);
                button.RemoveFromHierarchy();
            }
        }

        public void AddSettings(ISoundySettingsView soundySettingsView)
        {
            SettingsView?.Dispose();
            SoundDataBaseView?.Dispose();
            SettingsView = soundySettingsView ?? throw new ArgumentNullException(nameof(soundySettingsView));
            Root.content.AddChild(SettingsView.Root);
        }

        public void ClickDataBaseButton(string name)
        {
            Root.ClickDataBaseButton(name);
        }

        public void AddDataBaseButton(string name, UnityAction callback)
        {
            Root.AddDataBaseButton(name, callback);
        }

        public void SetSoundDataBase(ISoundDataBaseView dataBaseView)
        {
            SettingsView?.Dispose();
            SoundDataBaseView?.Dispose();
            SoundDataBaseView = dataBaseView;
            Root.content.AddChild(dataBaseView.Root);
        }
    }
}