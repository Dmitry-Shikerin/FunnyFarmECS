using Doozy.Editor.EditorUI;
using Doozy.Editor.EditorUI.Components;
using Doozy.Runtime.UIElements.Extensions;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls
{
    public class SoundySettingsRowVisualElement : VisualElement
    {
        public SoundySettingsRowVisualElement()
        {
            this
                .SetStyleFlexDirection(FlexDirection.Row)
                .SetStyleFlexGrow(1)
                .ResetLayout();

            Label = new Label()
                .SetStyleMinWidth(130)
                .SetStyleMaxHeight(20);
            Slider = new FluidIntSlider()
                .SetStyleMaxHeight(20);
            IntegerField = new IntegerField().SetStyleMaxHeight(20).SetStyleMinWidth(20);
            ResetButton = new FluidButton()
                .SetElementSize(ElementSize.Normal)
                .SetIcon(EditorSpriteSheets.EditorUI.Icons.Reset)
                .SetAccentColor(EditorSelectableColors.Default.Remove);

            this
                .AddChild(Label)
                .AddChild(Slider)
                .AddChild(IntegerField)
                .AddChild(ResetButton);
        }
        
        public FluidIntSlider Slider { get; }
        public Label Label { get; }
        public IntegerField IntegerField { get; }
        public FluidButton ResetButton { get; }
    }
}