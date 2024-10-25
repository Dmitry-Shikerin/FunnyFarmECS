using System;
using JetBrains.Annotations;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers.New;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using UnityEngine;
using UnityEngine.Audio;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure.Factories
{
    public class NewSoundyControllerViewFactory
    {
        private readonly NewSoundyManager _manager;
        private readonly SoundControllersPool _pool;

        public NewSoundyControllerViewFactory(NewSoundyManager manager, SoundControllersPool pool)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _pool = pool ?? throw new ArgumentNullException(nameof(pool));
        }

        public NewSoundyController Create()
        {
            NewSoundyController controller = new GameObject(
                "SoundyController", 
                typeof(AudioSource), 
                typeof(NewSoundyController)).GetComponent<NewSoundyController>();
            _pool.AddToCollection(controller);
            controller.Construct(_manager, _pool);
            
            return controller;
        }

        public void Create(
            SoundGroupData soundGroupData,
            NewSoundyController controller, 
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
    }
}