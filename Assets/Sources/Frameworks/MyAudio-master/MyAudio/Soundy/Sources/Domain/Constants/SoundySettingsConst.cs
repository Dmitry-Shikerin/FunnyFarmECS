using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants
{
    public class SoundySettingsConst
    {
        public const string FileName = "SoundySettings";
        public const string ResourcesPath = "Assets/Resources/Soundy/Settings/";
        public const string AssetPath = "Assets/Resources/Soundy/Settings/SoundySettings";
        public const string Asset = "t:SoundySettings";
        public const bool AutoKillIdleControllersDefaultValue = true;
        public const float ControllerIdleKillDurationDefaultValue = 20f;
        public const float ControllerIdleKillDurationMin = 0f;
        public const float ControllerIdleKillDurationMax = 300f;
        public static readonly Vector2Int MinMaxControllersIdleKillDuration = 
            new ((int)ControllerIdleKillDurationMin, (int)ControllerIdleKillDurationMax);
        public const float IdleCheckIntervalDefaultValue = 5f;
        public const float IdleCheckIntervalMin = 0.1f;
        public const float IdleCheckIntervalMax = 60f;
        public static readonly Vector2Int MixMaxIdleCheckInterval = 
            new ((int)IdleCheckIntervalMin, (int)IdleCheckIntervalMax);
        public const int MinimumNumberOfControllersDefaultValue = 3;
        public const int MinimumNumberOfControllersMin = 0;
        public const int MinimumNumberOfControllersMax = 20;
        public static readonly Vector2Int MixMaxMinimumNumberOfControllers = 
            new (MinimumNumberOfControllersMin, MinimumNumberOfControllersMax);
    }
}