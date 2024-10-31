using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public class SoundGroupViewFactory
    {
        public ISoundGroupView Create(SoundGroupData soundGroup, SoundDataBase soundDatabase)
        {
            SoundGroupView view = new SoundGroupView();
            SoundGroupPresenter presenter = new SoundGroupPresenter(soundGroup, soundDatabase, view);
            SoundGroupVisualElement visualElement = new SoundGroupVisualElement();
            view.Construct(presenter, visualElement);
            
            return view;
        }
    }
}