using Doozy.Runtime.UIElements.Extensions;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data.New;
using UnityEditor;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Editors
{
    [CustomEditor(typeof(SoundGroupData))]
    public class SoundGroupDataEditor : UnityEditor.Editor
    {
        private SoundDataBase _soundDatabase;
        private SoundGroupData _soundGroupData;
        private VisualElement _root;

        public override VisualElement CreateInspectorGUI()
        {
            FindProperties();
            ISoundGroupDataView view = new SoundGroupDataViewFactory().Create(_soundGroupData, _soundDatabase);
            _root = new VisualElement()
                .AddChild(view.Root);

            return _root;
        }

        private void FindProperties()
        {
            _soundGroupData = (SoundGroupData)serializedObject.targetObject;
            _soundDatabase = SoundySettings.Database.GetSoundDatabase(_soundGroupData.DatabaseName);
        }
    }
}