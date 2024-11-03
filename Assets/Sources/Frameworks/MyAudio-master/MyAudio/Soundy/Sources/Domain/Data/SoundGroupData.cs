using System;
using System.Collections.Generic;
using System.Linq;
using MyAudios.MyUiFramework.Attributes;
using MyAudios.Scripts;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Constants;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Enums;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data
{
    [Serializable]
    public class SoundGroupData
    {
        public bool HasMissingAudioClips
        {
            get
            {
                if (SoundName.Equals(SoundyManagerConstant.NoSound))
                    return false;
                
                if (Sounds == null || Sounds.Count == 0)
                    return true;

                foreach (AudioData audioData in Sounds)
                {
                    if (audioData == null || audioData.AudioClip == null)
                        return true;
                }

                return false;
            }
        }
        
        public bool HasSound
        {
            get
            {
                if (SoundName.Equals(SoundyManagerConstant.NoSound))
                    return false; //if SoundName is set to No Sound -> has no sound
                
                if (Sounds == null || Sounds.Count == 0)
                    return false;      //if the Sounds list is null or empty -> has no sound
                
                return HasMissingAudioClips == false;
            }
        }

        /// <summary> Returns a random pitch value between 0f and 4f </summary>
        public float RandomPitch =>
            SoundyUtils.SemitonesToPitch(Random.Range(Pitch.MinValue, Pitch.MaxValue));

        /// <summary> Returns a random volume value between 0f and 1f </summary>
        public float RandomVolume =>
            SoundyUtils.DecibelToLinear(Random.Range(Volume.MinValue, Volume.MaxValue));

        /// <summary> The SoundDatabase name this SoundGroupData belongs to </summary>
        public string DatabaseName;

        /// <summary> Sound name as defined in the database. This is set by default as the first AudioClip name </summary>
        public string SoundName;
        public bool IgnoreListenerPause;
        
        [MinMaxRange(SoundGroupDataConst.MinVolume, SoundGroupDataConst.MaxVolume)]
        public RangedFloat Volume;
        [MinMaxRange(SoundGroupDataConst.MinPitch, SoundGroupDataConst.MaxPitch)]
        public RangedFloat Pitch;
        [Range(SoundGroupDataConst.MinSpatialBlend, SoundGroupDataConst.MaxSpatialBlend)]
        public float SpatialBlend;
        public bool Loop;
        public SoundPlayMode Mode;

        /// <summary> If Mode is set to PlayMode.Sequence and this flag is set to TRUE, then the play sequence will get automatically reset after an inactive time has passed since the last played sound </summary>
        public bool ResetSequenceAfterInactiveTime;

        /// <summary>
        ///     If Mode is set to PlayMode.Sequence and the ResetSequenceAfterInactiveTime flag is set to TRUE, this is the time period that needs to pass in order for the sequence to reset itself and start playing again from the first entry
        ///     <para> Note that the time is measured from the time a sound starts to play, not when it ends </para>
        /// </summary>
        public float SequenceResetTime;
        public List<AudioData> Sounds = new ();
        public bool IsPlaying;
        
        /// <summary> Internal variable that keeps track of the last played sounds index </summary>
        private int _lastPlayedSoundsIndex = -1;
        /// <summary> Internal variable that keeps track of the last played sound time (used by the PlayMode.Sequence with the resetSequenceAfterInactiveTime flag set to TRUE</summary>
        private float _lastPlayedSoundTime;
        /// <summary> Internal data list that keeps track of the played sounds </summary>
        private readonly List<AudioData> _playedSounds = new List<AudioData>();
        /// <summary> Internal variable that holds a reference to the previously played AudioData </summary>
        public AudioData LastPlayedAudioData { get; private set; }
        
        private void Reset()
        {
            SoundName = SoundGroupDataConst.DefaultSoundName;
            IgnoreListenerPause = SoundGroupDataConst.DefaultIgnoreListenerPause;
            Loop = SoundGroupDataConst.DefaultLoop;
            Volume = new RangedFloat
            {
                MinValue = SoundGroupDataConst.DefaultVolume, 
                MaxValue = SoundGroupDataConst.DefaultVolume
            };
            Pitch = new RangedFloat
            {
                MinValue = SoundGroupDataConst.DefaultPitch, 
                MaxValue = SoundGroupDataConst.DefaultPitch
            };
            SpatialBlend = SoundGroupDataConst.DefaultSpatialBlend;
            Mode = SoundGroupDataConst.DefaultPlayMode;
            ResetSequenceAfterInactiveTime = SoundGroupDataConst.DefaultResetSequenceAfterInactiveTime;
            SequenceResetTime = SoundGroupDataConst.DefaultSequenceResetTime;
        }
        
        public bool Contains(AudioClip audioClip)
        {
            if (audioClip == null)
                return false;
            
            return Sounds.Any(data => data.AudioClip == audioClip);
        }
        
        public AudioData AddAudioData(AudioClip audioClip = null)
        {
            AudioData audioData = new AudioData(audioClip);
            Sounds.Add(audioData);

            return audioData;
        }

        public void Remove(AudioData audioData) =>
            Sounds.Remove(audioData);

        public void ChangeLastPlayedAudioData()
        {
            LastPlayedAudioData = GetAudioData(Mode);
        }
        
        /// <summary> Returns the proper AudioData that needs to get played according to the set settings </summary>
        /// <param name="playMode">The play mode.</param>
        private AudioData GetAudioData(SoundPlayMode playMode)
        {
            return playMode switch
            {
                SoundPlayMode.Random => GetRandom(),
                SoundPlayMode.Sequence => GetSequence(),
                _ => throw new InvalidOperationException("Invalid play mode"),
            };
        }

        private AudioData GetRandom()
        {
            if (_playedSounds.Count == Sounds.Count)
                _playedSounds.Clear();

            AudioData foundClip = null; //look for a sound that has not been played
                    
            while (foundClip == null)   //until such a sound is found continue the search
            {
                int randomIndex = Random.Range(0, Sounds.Count);
                foundClip = Sounds[randomIndex];        //get a random sound
                        
                if (_playedSounds.Contains(foundClip)) //check that it has not been played
                {
                    foundClip = null; //it has been played -> discard it
                }
                else
                {
                    _playedSounds.Add(foundClip); //it has not been played -> add it to the _playedSounds list and continue
                    _lastPlayedSoundsIndex = randomIndex;
                }
            }

            return foundClip; //return the sound that will get played
        }

        private AudioData GetSequence()
        {
            if (_playedSounds.Count == Sounds.Count) //if all the sounds in the sounds list were played
                _lastPlayedSoundsIndex = -1;         //-> reset the sequence index

            if (ResetSequenceAfterInactiveTime &&                                      //if resetSequenceAfterInactiveTime 
                Time.realtimeSinceStartup - _lastPlayedSoundTime > SequenceResetTime) //and enough time has passed since the last sound in the sequence has been played //Time.unscaledTime
                _lastPlayedSoundsIndex = -1;                                          //-> reset the sequence index

            if (_lastPlayedSoundsIndex == -1) //if the last played index is in the reset state (-1)
                _playedSounds.Clear();        //-> reset the played sounds list

            _lastPlayedSoundsIndex = _lastPlayedSoundsIndex == -1 || _lastPlayedSoundsIndex >= Sounds.Count - 1
                ? 0 //if the index has been reset (-1)
                : _lastPlayedSoundsIndex + 1; //-> set the last played index as the first entry in the sounds list
                                              //-> otherwise set the last played index as the next entry in the sequence

            _playedSounds.Add(Sounds[_lastPlayedSoundsIndex]); //add the played sound to the playedSounds list
            _lastPlayedSoundTime = Time.realtimeSinceStartup;   //save the last played sound time
                    
            return Sounds[_lastPlayedSoundsIndex];              //return the sound that will get played
        }
    }
}