using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public class SoundGroupDataViewFactory
    {
        public ISoundGroupDataView Create(SoundGroupData soundGroupData, SoundDatabase soundDatabase)
        {
            AudioDataViewFactory audioDataViewFactory = new AudioDataViewFactory();
            
            SoundGroupDataView view = new SoundGroupDataView();
            SoundGroupDataPresenter presenter = new SoundGroupDataPresenter(
                soundGroupData, soundDatabase, view, audioDataViewFactory);
            view.Construct(presenter);
            
            return view;
        }
    }
}