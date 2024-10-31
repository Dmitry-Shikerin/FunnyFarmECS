using System.Collections.Generic;
using Doozy.Editor.EditorUI.Components;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories
{
    public class SoundyDataBaseViewFactory
    {

        
        public ISoundyDataBaseView Create(SoundyDataBase soundyDatabase, SoundySettings soundySettings)
        {
            SoundySettingsViewFactory soundySettingsViewFactory = new SoundySettingsViewFactory();
            SoundDataBaseViewFactory soundDataBaseViewFactory = new SoundDataBaseViewFactory();
            
            var fluidWindowLayout = new SoundyDataBaseWindowLayout();
            fluidWindowLayout
                .sideMenu
                .SetMenuLevel(FluidSideMenu.MenuLevel.Level_2)
                .IsCollapsable(false);
            SoundyDataBaseView view = new SoundyDataBaseView();
            SoundyDataBasePresenter presenter = new SoundyDataBasePresenter(
                soundyDatabase, soundySettings, view, soundDataBaseViewFactory, soundySettingsViewFactory);
            view.Construct(presenter, fluidWindowLayout);
            
            return view;
        }
    }
}