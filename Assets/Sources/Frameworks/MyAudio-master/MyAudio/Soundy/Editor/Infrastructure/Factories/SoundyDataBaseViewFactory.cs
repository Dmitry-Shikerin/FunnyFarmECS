using System;
using Doozy.Editor.EditorUI.Components;
using JetBrains.Annotations;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public class SoundyDataBaseViewFactory
    {
        private readonly SoundyPrefsStorage _soundyPrefsStorage;
        private readonly PreviewSoundPlayerService _previewSoundPlayerService;

        public SoundyDataBaseViewFactory(
            SoundyPrefsStorage soundyPrefsStorage,
            PreviewSoundPlayerService previewSoundPlayerService)
        {
            _soundyPrefsStorage = soundyPrefsStorage ?? throw new ArgumentNullException(nameof(soundyPrefsStorage));
            _previewSoundPlayerService = previewSoundPlayerService ?? 
                                         throw new ArgumentNullException(nameof(previewSoundPlayerService));
        }

        public ISoundyDataBaseView Create(SoundyDataBase soundyDatabase, SoundySettings soundySettings)
        {
            SoundySettingsViewFactory soundySettingsViewFactory = new SoundySettingsViewFactory();
            SoundDataBaseViewFactory soundDataBaseViewFactory = new SoundDataBaseViewFactory(
                _soundyPrefsStorage, _previewSoundPlayerService);
            
            var fluidWindowLayout = new SoundyDataBaseWindowLayout();
            fluidWindowLayout
                .sideMenu
                .SetMenuLevel(FluidSideMenu.MenuLevel.Level_2)
                .IsCollapsable(false);
            SoundyDataBaseView view = new SoundyDataBaseView();
            SoundyDataBasePresenter presenter = new SoundyDataBasePresenter(
                soundyDatabase, soundySettings, view, soundDataBaseViewFactory, soundySettingsViewFactory, _soundyPrefsStorage);
            view.Construct(presenter, fluidWindowLayout);
            
            return view;
        }
    }
}