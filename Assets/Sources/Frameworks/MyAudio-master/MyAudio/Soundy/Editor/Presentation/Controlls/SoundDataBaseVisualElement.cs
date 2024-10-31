using Doozy.Editor.EditorUI;
using Doozy.Editor.EditorUI.Utils;
using Doozy.Runtime.UIElements.Extensions;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls
{
    public class SoundDataBaseVisualElement : VisualElement
    {
        public SoundDataBaseVisualElement()
        {
            HeaderVisualElement = new SoundDataBaseHeaderVisualElement();
            NewSoundContentVisualElement = new NewSoundContentVisualElement();
            AudioMixerVisualElement = new SoundDataBaseAudioMixerVisualElement();
            VisualElement spaseLine = DesignUtils.row
                .SetStyleMaxHeight(4)
                .SetStyleMinHeight(4)
                .SetStyleBackgroundColor(EditorColors.Default.Background)
                .SetStyleBorderRadius(4,4,4,4);
            SpaseElement = DesignUtils.column
                .SetStyleMinWidth(25)
                .SetStyleMaxHeight(25)
                .AddChild(DesignUtils.row)
                .AddChild(spaseLine)
                .AddChild(DesignUtils.row);
            ScrollView = new ScrollView();
            SoundGroupsContainer = DesignUtils
                .column
                .SetStyleBackgroundColor(EditorColors.Default.Background)
                .AddSpace(4)
                .AddChild(ScrollView);
            
            Root = DesignUtils.column
                .AddChild(HeaderVisualElement)
                .AddSpace(4)
                .AddChild(AudioMixerVisualElement)
                .AddSpace(4)
                .AddChild(SpaseElement)
                .AddSpace(4)
                .AddChild(NewSoundContentVisualElement)
                .AddChild(SoundGroupsContainer);
            
            Add(Root);
        }
        
        private VisualElement Root { get; set; }
        public SoundDataBaseHeaderVisualElement HeaderVisualElement { get; private set; }
        public NewSoundContentVisualElement NewSoundContentVisualElement { get; private set; }
        public SoundDataBaseAudioMixerVisualElement AudioMixerVisualElement { get; private set; }
        public VisualElement SpaseElement { get; private set; }
        public ScrollView ScrollView { get; private set; }
        public VisualElement SoundGroupsContainer { get; private set; }
    }
}