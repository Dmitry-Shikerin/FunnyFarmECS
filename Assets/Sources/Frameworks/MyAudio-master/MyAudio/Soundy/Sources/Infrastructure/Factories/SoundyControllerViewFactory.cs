using System;
using JetBrains.Annotations;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers.New;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using UnityEngine;
using UnityEngine.Audio;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure.Factories
{
    public class SoundyControllerViewFactory
    {
        private readonly SoundyManager _manager;
        private readonly SoundControllersPool _pool;

        public SoundyControllerViewFactory(SoundyManager manager, SoundControllersPool pool)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _pool = pool ?? throw new ArgumentNullException(nameof(pool));
        }

        public SoundyController Create()
        {
            SoundyController controller = new GameObject(
                "SoundyController", 
                typeof(AudioSource), 
                typeof(SoundyController)).GetComponent<SoundyController>();
            _pool.AddToCollection(controller);
            controller.Construct(_manager, _pool);
            
            return controller;
        }

        public void Create(
            SoundGroupData soundGroupData,
            SoundyController controller, 
            AudioMixerGroup outputAudioMixerGroup = null,
            Transform followTarget = null, 
            Vector3 position = default)
        {
            soundGroupData.ChangeLastPlayedAudioData();
            controller.SetSourceProperties(
                soundGroupData.LastPlayedAudioData.AudioClip,
                soundGroupData.RandomVolume,
                soundGroupData.RandomPitch,
                soundGroupData.Loop,
                soundGroupData.SpatialBlend);
            controller.SetOutputAudioMixerGroup(outputAudioMixerGroup);
            controller.SetPosition(position);

            if (soundGroupData.LastPlayedAudioData == null)
                return;

            controller.gameObject.name = "[" + soundGroupData.SoundName + "]-(" +
                                        soundGroupData.LastPlayedAudioData.AudioClip.name + ")";
            controller.Name = soundGroupData.SoundName;
            controller.SetFollowTarget(followTarget);
        }

        public void Create(SoundyController controller, 
            AudioClip audioClip,
            AudioMixerGroup outputAudioMixerGroup,
            Vector3 position,
            float volume = 1,
            float pitch = 1,
            bool loop = false,
            float spatialBlend = 1) 
        {
            controller.SetSourceProperties(audioClip, volume, pitch, loop, spatialBlend);
            controller.SetOutputAudioMixerGroup(outputAudioMixerGroup);
            controller.SetPosition(position);
            controller.gameObject.name = "[AudioClip]-(" + audioClip.name + ")";
            controller.Name = audioClip.name;
        }
        
        public void Create(SoundyController controller, 
            AudioClip audioClip,
            AudioMixerGroup outputAudioMixerGroup,
            Transform followTarget,
            float volume = 1,
            float pitch = 1,
            bool loop = false,
            float spatialBlend = 1) 
        {
            controller.SetSourceProperties(audioClip, volume, pitch, loop, spatialBlend);
            controller.SetOutputAudioMixerGroup(outputAudioMixerGroup);
            controller.SetFollowTarget(followTarget);
            controller.gameObject.name = "[AudioClip]-(" + audioClip.name + ")";
            controller.Name = audioClip.name;
        }
    }
}