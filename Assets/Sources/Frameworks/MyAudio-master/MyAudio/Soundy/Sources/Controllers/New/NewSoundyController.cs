using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using UnityEngine;
using UnityEngine.Audio;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers.New
{
    [DefaultExecutionOrder(SoundyExecutionOrder.SoundyController)]
    public class NewSoundyController : MonoBehaviour
    {
        private Transform _transform;
        private Transform _followTarget;
        private bool _autoPaused;
        private float _savedClipTime;
        private SoundControllersPool _pool;
        private NewSoundyManager _manager;
        
        public string Name { get; set; }
        public AudioSource AudioSource { get; private set; }
        public bool IsPlaying => AudioSource.isPlaying;
        public bool InUse { get; private set; }
        public float PlayProgress { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsMuted => AudioSource.mute;
        public float LastPlayedTime { get; private set; }
        public float IdleDuration => Time.realtimeSinceStartup - LastPlayedTime;

        public void Construct(NewSoundyManager manager, SoundControllersPool pool)
        {
            _manager = manager ?? throw new System.ArgumentNullException(nameof(manager));
            _pool = pool ?? throw new System.ArgumentNullException(nameof(pool));
        }
        
        private void Reset() =>
            ResetController();

        private void Awake()
        {
            _transform = transform;
            AudioSource = gameObject.GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
            ResetController();
        }

        public void Run()
        {
            UpdateLastPlayedTime();
            UpdatePlayProgress();
            FollowTarget();
        }

        public void Destroy()
        {
            Stop();
            Destroy(gameObject);
        }

        public void Mute()
        {
            AudioSource.mute = true;
        }

        public void Pause()
        {
            _savedClipTime = AudioSource.time;
            AudioSource.Pause();
            IsPaused = true;
        }

        public void Play()
        {
            InUse = true;
            IsPaused = false;
            AudioSource.Play();
        }

        public void SetFollowTarget(Transform followTarget) =>
            _followTarget = followTarget;

        public void SetOutputAudioMixerGroup(AudioMixerGroup outputAudioMixerGroup)
        {
            if (outputAudioMixerGroup == null)
                return;
            
            AudioSource.outputAudioMixerGroup = outputAudioMixerGroup;
        }

        public void SetPosition(Vector3 position) =>
            _transform.position = position;

        public void SetSourceProperties(AudioClip clip, float volume, float pitch, bool loop, float spatialBlend)
        {
            if (clip == null)
            {
                Stop();
                return;
            }

            AudioSource.clip = clip;
            AudioSource.volume = volume;
            AudioSource.pitch = pitch;
            AudioSource.loop = loop;
            AudioSource.spatialBlend = spatialBlend;
        }

        public void Stop()
        {
            Unpause();
            Unmute();
            AudioSource.Stop();
            ResetController();
            _pool.ReturnToPool(this);
        }

        public void Unmute()
        {
            AudioSource.mute = false;
        }

        public void Unpause()
        {
            AudioSource.time = _savedClipTime;
            IsPaused = false;
            AudioSource.UnPause();
        }

        private void FollowTarget()
        {
            if (_followTarget == null) 
                return;
            
            _transform.position = _followTarget.position;
        }

        private void ResetController()
        {
            InUse = false;
            IsPaused = false;
            _followTarget = null;
            UpdateLastPlayedTime();
        }

        private void UpdateLastPlayedTime()
        {
            if ((IsMuted || IsPaused || AudioSource.isPlaying) == false)
                return;
            
            LastPlayedTime = Time.realtimeSinceStartup;
        }

        private void UpdatePlayProgress()
        {
            if (AudioSource == null)
                return;
            
            if (AudioSource.clip == null)
                return;
            
            PlayProgress = Mathf.Clamp01(AudioSource.time / AudioSource.clip.length);
            
            if (PlayProgress < 1f) //check if the sound finished playing
                return;
            
            Stop();
            PlayProgress = 0;
        }
    }
}