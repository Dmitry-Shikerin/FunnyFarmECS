using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public class SoundySettingsViewFactory : ISoundySettingsViewFactory
    {
        public ISoundySettingsView Create(SoundySettings soundySettings)
        {
            SoundySettingsView view = new SoundySettingsView();
            SoundySettingsPresenter presenter = new SoundySettingsPresenter(soundySettings, view);
            SoundySettingsVisualElement visualElement = new SoundySettingsVisualElement();
            view.Construct(presenter, visualElement);

            return view;
        }
    }
}