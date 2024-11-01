using Doozy.Editor.EditorUI.Windows.Internal;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Factories;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Infrastructure.Services;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Editors
{
    public class SoundyDataBaseEditorWindow : FluidWindow<SoundyDataBaseEditorWindow>
    {
        private SoundyDataBase _database;
        private AudioClip _plugAudio;

        [MenuItem("Tools/SoundyDataBase")]
        public static void ShowWindow() =>
            GetWindow<SoundyDataBaseEditorWindow>("Soundy Data Base");

        protected override void CreateGUI()
        {
            root.Clear();

            _database = SoundySettings.Database;
            _plugAudio = Resources.Load<AudioClip>("MyAudios/Soundy/Resources/Soundy/Plugs/Christmas Villain Loop");

            ISoundyDataBaseView view = EditorServiceLocator
                .Get<SoundyDataBaseViewFactory>()
                .Create(_database, SoundySettings.Instance);
            
            root
                .Add(view.Root);
        }
    }
}