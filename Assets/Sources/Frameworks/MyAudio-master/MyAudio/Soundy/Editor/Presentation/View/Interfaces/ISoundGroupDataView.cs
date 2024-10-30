using System.Collections.Generic;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Enums;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces
{
    public interface ISoundGroupDataView : IView<SoundGroupDataVisualElement>
    {
        IReadOnlyList<IAudioDataView> AudioDataViews { get; }
        
        void AddAudioData(IAudioDataView audioDataView);
        void SetIsOnButtonTab(SoundPlayMode playMode);
        void SetVolume(Vector2 volume, Vector2 minMaxVolume);
        void SetPitch(Vector2 pitch, Vector2 minMaxPitch);
        void SetSpatialBlend(float spatialBlend, Vector2 minMaxSpatialBlend);
        public void SetSoundName(string name);
        void StopAllAudioData();
        void SetLoop(bool loop);
    }
}