using Doozy.Editor.EditorUI.Windows.Internal;
using Doozy.Runtime.UIElements.Extensions;
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
            root.Clear();
            
            SoundGroupDataEditor editor = (SoundGroupDataEditor)UnityEditor.Editor.CreateEditor(SoundGroupData);
            VisualElement editorRoot = editor.CreateInspectorGUI();
            editorRoot
                .Bind(editor.serializedObject);
            
            root
                .AddChild(editorRoot)
                .SetStylePadding(15, 15, 15, 15)
                ;
        }
    }
}