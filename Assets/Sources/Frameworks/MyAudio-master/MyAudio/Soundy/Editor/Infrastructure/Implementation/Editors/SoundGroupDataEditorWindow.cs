using Doozy.Editor.EditorUI.Windows.Internal;
using Doozy.Runtime.UIElements.Extensions;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Editors
{
    public class SoundGroupDataEditorWindow : FluidWindow<SoundGroupDataEditorWindow>
    {
        public static void Open()
        {
            SoundGroupDataEditorWindow window = GetWindow<SoundGroupDataEditorWindow>();
            window.titleContent = new GUIContent("Sound Group");
            window.Show();
        }
        
        protected override void CreateGUI()
        {
            root.Clear();
            string soundGroupDataName = EditorServiceLocator.Get<SoundyPrefsStorage>().GetLastSoundGroupData();
            var soundGroupData = SoundySettings.Database.GetSoundGroupData(soundGroupDataName);
            
            VisualElement editorRoot = EditorServiceLocator
                .Get<ISoundGroupDataViewFactory>()
                .Create(soundGroupData, SoundySettings.Database).Root;

            root
                .AddChild(editorRoot)
                .SetStylePadding(15, 15, 15, 15)
                ;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            SoundySettings.Database.Save();
        }
    }
}