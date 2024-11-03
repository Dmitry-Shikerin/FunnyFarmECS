﻿using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Enums;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants
{
    public class SoundGroupDataConst
    {
        public const bool DefaultIgnoreListenerPause = true;
        public const bool DefaultLoop = false;
        public const bool DefaultResetSequenceAfterInactiveTime = false;
        public const float DefaultPitch = 0;
        public const float DefaultSequenceResetTime = 5f;
        public const float DefaultSpatialBlend = 0;
        public const float DefaultVolume = 0;
        public const float MaxPitch = 24;
        public const float MaxSpatialBlend = 1;
        public const float MaxVolume = 0;
        public const float MinPitch = -24;
        public const float MinSpatialBlend = 0;
        public const float MinVolume = -80;
        public const SoundPlayMode DefaultPlayMode = SoundPlayMode.Random;
        public const string DefaultSoundName = SoundyManagerConstant.NoSound;
    }
}