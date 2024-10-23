using MyAudios.Soundy.Editor.SoundySetting.Controllers;
using MyAudios.Soundy.Editor.SoundySetting.Presentation.Views.Implementation;
using MyAudios.Soundy.Editor.SoundySetting.Presentation.Views.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;

namespace MyAudios.Soundy.Editor.SoundySetting.Infrastructure.Factories
{
    public class SoundySettingsViewFactory
    {
        public ISoundySettingsView Create(SoundySettings soundySettings)
        {
            SoundySettingsView view = new SoundySettingsView();
            SoundySettingsPresenter presenter = new SoundySettingsPresenter(soundySettings, view);
            view.Construct(presenter);

            return view;
        }
    }
}