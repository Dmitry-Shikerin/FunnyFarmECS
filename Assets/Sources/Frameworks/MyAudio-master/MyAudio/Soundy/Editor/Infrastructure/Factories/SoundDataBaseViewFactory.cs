using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public class SoundDataBaseViewFactory
    {
        public ISoundDataBaseView Create(SoundDataBase soundDatabase, SoundyDataBase soundyDatabase)
        {
            SoundGroupViewFactory soundGroupViewFactory = new SoundGroupViewFactory();
            
            SoundDataBaseView view = new SoundDataBaseView();
            SoundDataBasePresenter presenter = new SoundDataBasePresenter(
                soundDatabase, soundyDatabase, view,  soundGroupViewFactory);
            view.Construct(presenter);
            
            return view;
        }
    }
}