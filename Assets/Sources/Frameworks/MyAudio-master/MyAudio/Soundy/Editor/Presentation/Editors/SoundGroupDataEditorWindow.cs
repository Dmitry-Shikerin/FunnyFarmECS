using Doozy.Editor.EditorUI.Windows.Internal;
using Doozy.Runtime.UIElements.Extensions;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Editors
{
    public class SoundGroupDataEditorWindow : FluidWindow<SoundGroupDataEditorWindow>
    {
        private static SoundGroupData SoundGroupData { get; set; }
        
        public static void Open(SoundGroupData soundGroupData)
        {
            SoundGroupData = soundGroupData;
            SoundGroupDataEditorWindow window = GetWindow<SoundGroupDataEditorWindow>();
            window.titleContent = new GUIContent("Sound Group");
            window.Show();
        }
        
        protected override void CreateGUI()
        {
            
            FindProperties();
            root.Clear();
            
            // SoundGroupDataEditor editor = (SoundGroupDataEditor)UnityEditor.Editor.CreateEditor(SoundGroupData);
            VisualElement editorRoot = EditorServiceLocator
                .Get<SoundGroupDataViewFactory>()
                .Create(SoundGroupData).Root;
            // editorRoot
            //     .Bind(editor.serializedObject);
            //
            root
                .AddChild(editorRoot)
                .SetStylePadding(15, 15, 15, 15)
                ;
        }
        
        private SoundDataBase _soundDatabase;
        private SoundGroupData _soundGroupData;
        private VisualElement _root;

        // public override VisualElement CreateInspectorGUI()
        // {
        //     FindProperties();
        //     ISoundGroupDataView view = new SoundGroupDataViewFactory().Create(_soundGroupData, _soundDatabase);
        //     _root = new VisualElement()
        //         .AddChild(view.Root);
        //
        //     return _root;
        // }
        //
        private void FindProperties()
        {
            // _soundGroupData = (SoundGroupData)serializedObject.targetObject;
            _soundDatabase = SoundySettings.Database.GetSoundDatabase(SoundGroupData.DatabaseName);
        }
    }
}