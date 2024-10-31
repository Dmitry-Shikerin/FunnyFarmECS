using Doozy.Editor.EditorUI;
using Doozy.Editor.EditorUI.Components;
using Doozy.Editor.EditorUI.Utils;
using Doozy.Runtime.UIElements.Extensions;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New;
using UnityEditor;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Editors
{
    [CustomEditor(typeof(SoundDataBase))]
    public class SoundDataBaseEditor : UnityEditor.Editor
    {
        private SerializedProperty Name { get; set; }
        private SerializedProperty DatabaseNames { get; set; }
        private SerializedProperty SoundDatabases { get; set; }

        private VisualElement Root { get; set; }
        private FluidComponentHeader Header { get; set; }

        private FluidListView DatabaseNamesField { get; set; }
        private FluidListView SoundDatabasesField { get; set; }
        
        public override VisualElement CreateInspectorGUI()
        {
            FindProperties();
            InitializeEditor();
            Compose();

            return Root;
        }

        private void Refresh()
        {
            InitializeNames();
            InitializeDataBases();
        }

        private void FindProperties()
        {
            Name = serializedObject.FindProperty(nameof(SoundDataBase.Name));
        }

        private void InitializeEditor()
        {
            Root = new VisualElement();

            InitializeHeader();
            InitializeNames();
            InitializeDataBases();
        }

        private void Compose()
        {
            Label label = DesignUtils
                .NewLabel((string)Name.stringValue)
                .SetStyleFontSize(16);
            VisualElement labelRow = DesignUtils
                .row
                .AddChild(label);

            FluidFoldout namesFoldout =
                new FluidFoldout()
                    .AddContent(DatabaseNamesField)
                    .SetLabelText("Names");

            FluidFoldout dataBaseFoldout =
                new FluidFoldout()
                    .AddContent(SoundDatabasesField)
                    .SetLabelText("Databases");
            
            Root
                .AddChild(Header)
                .AddChild(labelRow)
                .AddSpaceBlock(2)
                .AddChild(namesFoldout)
                .AddSpaceBlock()
                .AddChild(dataBaseFoldout)
                ;
        }

        private void InitializeNames()
        {
            DatabaseNamesField =
                DesignUtils.NewStringListView(DatabaseNames, "Database Names", "");
        }

        private void InitializeDataBases()
        {
            SoundDatabasesField = DesignUtils.NewObjectListView(
                SoundDatabases, "Sound Databases", "", typeof(SoundGroupData));
        }

        private void InitializeHeader()
        {
            Header =
                FluidComponentHeader
                    .Get()
                    .SetComponentNameText("Sound Database")
                    .SetIcon(EditorSpriteSheets.EditorUI.Icons.Sound)
                    .SetAccentColor(EditorColors.EditorUI.Orange)
                ;
        }
    }
}