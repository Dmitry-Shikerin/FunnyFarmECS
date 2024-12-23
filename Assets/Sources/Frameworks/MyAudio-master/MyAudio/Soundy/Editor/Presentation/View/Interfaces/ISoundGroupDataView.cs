﻿using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces
{
    public interface ISoundGroupDataView : IView<SoundGroupDataVisualElement>
    {
        void AddAudioData(IAudioDataView audioDataView);
        void SetRandomIsOn();
        void SetSequenceIsOn();
        void SetVolume(Vector2 volume, Vector2 minMaxVolume);
        void SetPitch(Vector2 pitch, Vector2 minMaxPitch);
        void SetSpatialBlend(float spatialBlend, Vector2 minMaxSpatialBlend);
        public void SetSoundName(string name);
        void SetLoop(bool loop);
    }
}