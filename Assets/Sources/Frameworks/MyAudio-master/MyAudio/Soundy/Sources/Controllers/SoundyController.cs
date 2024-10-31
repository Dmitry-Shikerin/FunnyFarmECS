using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Enums;
using UnityEngine;
using UnityEngine.Audio;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers
{
    [DefaultExecutionOrder(SoundyExecutionOrder.SoundyController)]
    public class SoundyController : MonoBehaviour
    {
        private Transform _transform;
        private Transform _followTarget;
        private bool _autoPaused;
        private float _savedClipTime;
        private SoundControllersPool _pool;
        private SoundyManager _manager;
        
        public string Name { get; set; }
        //Todo добавить это в едитор виндов и устанавливать это значение в фабрике
        public SoundType SoundType { get; private set; }
        public AudioSource AudioSource { get; private set; }
        public bool IsPlaying => AudioSource.isPlaying;
        public bool InUse { get; private set; }
        public float PlayProgress { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsMuted => AudioSource.mute;
        public float LastPlayedTime { get; private set; }
        public float IdleDuration => Time.realtimeSinceStartup - LastPlayedTime;

        public void Construct(SoundyManager manager, SoundControllersPool pool)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _pool = pool ?? throw new ArgumentNullException(nameof(pool));
        }
        
        private void Reset() =>
            ResetController();

        private void Awake()
        {
            _transform = transform;
            AudioSource = gameObject.GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
            ResetController();
        }

        private void OnEnable()
        {
            if (_manager == null)
                return;
            
            _manager.Running += Run;
        }

        private void OnDisable()
        {
            if (_manager == null)
                return;
            
            _manager.Running -= Run;
        }

        public void Run(float deltaTime)
        {
            if (AudioSource.clip == null)
            {
                Stop();
                return;
            }
            
            UpdateLastPlayedTime();
            UpdatePlayProgress();
            FollowTarget();
        }

        public void Destroy()
        {
            Stop();
            Destroy(gameObject);
        }

        public SoundyController Play()
        {
            InUse = true;
            IsPaused = false;
            AudioSource.Play();
            return this;
        }

        public SoundyController SetFollowTarget(Transform followTarget)
        {
            _followTarget = followTarget;
            return this;
        }

        public SoundyController SetOutputAudioMixerGroup(AudioMixerGroup outputAudioMixerGroup)
        {
            if (outputAudioMixerGroup == null)
                return this;
            
            AudioSource.outputAudioMixerGroup = outputAudioMixerGroup;
            return this;
        }

        public void Stop()
        {
            Unpause();
            Unmute();
            AudioSource.Stop();
            ResetController();
            UpdateLastPlayedTime();
            _pool.ReturnToPool(this);
        }
        
        public SoundyController SetName(string soundName, string audioClipName)
        {
            Name = soundName;
            gameObject.name = $"[{soundName}]-({audioClipName})";
            return this;
        }
        
        public SoundyController SetSpatialBlend(float spatialBlend)
        {
            AudioSource.spatialBlend = spatialBlend;
            return this;
        }
        
        public SoundyController SetPitch(float pitch)
        {
            AudioSource.pitch = pitch;
            return this;
        }
        
        public SoundyController SetLoop(bool loop)
        {
            AudioSource.loop = loop;
            return this;
        }

        public SoundyController SetVolume(float volume)
        {
            AudioSource.volume = volume;
            return this;
        }

        public SoundyController SetAudioClip(AudioClip audioClip)
        {
            AudioSource.clip = audioClip;
            return this;
        }

        public SoundyController SetPosition(Vector3 position)
        {
            _transform.position = position;
            return this;
        }

        public SoundyController Mute()
        {
            AudioSource.mute = true;
            return this;
        }

        public SoundyController Unmute()
        {
            AudioSource.mute = false;
            return this;
        }

        public SoundyController Pause()
        {
            _savedClipTime = AudioSource.time;
            AudioSource.Pause();
            IsPaused = true;
            return this;
        }

        public SoundyController Unpause()
        {
            AudioSource.time = _savedClipTime;
            IsPaused = false;
            AudioSource.UnPause();
            return this; 
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
            // Debug.Log($"PlayProgress: {PlayProgress}, AudioSource.time: {AudioSource.time}, AudioSource.clip.length: {AudioSource.clip.length}, IsPlaying: {AudioSource.isPlaying}");

            if (PlayProgress >= 1f) //check if the sound finished playing
            {
                Stop();
                PlayProgress = 0;
            }

            //TODO костыль, но работает
            if (IsPaused == false && IsPlaying == false)
            {
                Stop();
                PlayProgress = 0;
            }
        }
    }
}