using Sources.Frameworks.GameServices.Pauses.Services.Implementation;
using Sources.Frameworks.GameServices.Repositories.Services.Implementation;
using Sources.Frameworks.GameServices.Volumes.Domain.Models.Implementation;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure
{
    public class NewSoundyTestMono : MonoBehaviour
    {
        private Pause _pause;
        private SoundyService _soundyService;
        private Volume _soundsVolume;
        private Volume _musicVolume;

        private void Awake()
        {
            Debug.Log($"Test Awake");
            _pause = new Pause();
            _soundsVolume = new Volume();
            _musicVolume = new Volume();
            _soundyService = new SoundyService(new EntityRepository());
            // _soundyService.Initialize();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _soundyService.Play("Sounds", "Click");
            }
        }

        private void OnDestroy()
        {
            _soundyService.Destroy();
        }
    }
}