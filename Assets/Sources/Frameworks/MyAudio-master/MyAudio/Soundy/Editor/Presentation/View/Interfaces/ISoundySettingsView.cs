using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces
{
    public interface ISoundySettingsView : IView<SoundySettingsVisualElement>
    {
        void SetAutoKillIdleControllers(bool autoKillIdleControllers);
        void SetControllerAutoKillDuration(Vector2Int minMaxValue, int value);
        void SetIdleCheckInterval(Vector2Int minMaxValue, int value);
        void SetMinimumNumberOfControllers(Vector2Int minMaxValue, int value);
    }
}