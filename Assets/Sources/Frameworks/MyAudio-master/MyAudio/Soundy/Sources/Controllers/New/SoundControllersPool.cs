using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers.New
{
    public class SoundControllersPool
    {
        private readonly Transform _parentTransform;
        private List<NewSoundyController> _pool = new List<NewSoundyController>();
        private Coroutine _idleCheckCoroutine;
        private WaitForSecondsRealtime _idleCheckIntervalWaitForSecondsRealtime;
        private NewSoundyController _tempController;

        public SoundControllersPool(Transform parentTransform)
        {
            _parentTransform = parentTransform;
        }

        public IReadOnlyCollection<NewSoundyController> Pool => _pool;
        private bool AutoKillIdleControllers => SoundySettings.Instance.AutoKillIdleControllers;
        private float ControllerIdleKillDuration => SoundySettings.Instance.ControllerIdleKillDuration;
        private float IdleCheckInterval => SoundySettings.Instance.IdleCheckInterval;
        private int MinimumNumberOfControllers => SoundySettings.Instance.MinimumNumberOfControllers;
        

        private void Initialize()
        {
            if (AutoKillIdleControllers == false)
                return;
            
            StartIdleCheckInterval();
        }

        private void Destroy() =>
            StopIdleCheckInterval();
        
        public void ClearPool(bool keepMinimumNumberOfControllers = false)
        {
            if (keepMinimumNumberOfControllers)
            {
                RemoveNullControllers();
                
                if (_pool.Count <= MinimumNumberOfControllers)
                    return;

                int killedControllersCount = 0;
                
                for (int i = _pool.Count - 1; i >= MinimumNumberOfControllers; i--)
                {
                    NewSoundyController controller = _pool[i];
                    _pool.Remove(controller);
                    controller.Kill();
                    killedControllersCount++;
                }

                return;
            }

            NewSoundyController.KillAll();
            _pool.Clear();
        }
        
        public NewSoundyController Get()
        {
            RemoveNullControllers();
            
            if (_pool.Count <= 0)
                ReturnToPool(NewSoundyController.CreateController());
            
            NewSoundyController controller = _pool[0];
            _pool.Remove(controller);
            controller.gameObject.SetActive(true);
            
            return controller;
        }

        private NewSoundyController Create()
        {
            NewSoundyController controller = new GameObject(
                "SoundyController", 
                typeof(AudioSource), 
                typeof(NewSoundyController)).GetComponent<NewSoundyController>();
            
            return controller;
        }
        
        public void PopulatePool(int count)
        {
            RemoveNullControllers();
            
            if (count < 1) 
                return;
            
            for (int i = 0; i < count; i++)
                ReturnToPool(NewSoundyController.CreateController());
        }
        
        public void ReturnToPool(NewSoundyController controller)
        {
            if (controller == null) 
                return;
            
            if (_pool.Contains(controller) == false) 
                _pool.Add(controller);
            
            controller.gameObject.SetActive(false);
            controller.transform.SetParent(_parentTransform);
        }

        private void StartIdleCheckInterval()
        {
            _idleCheckIntervalWaitForSecondsRealtime = 
                new WaitForSecondsRealtime(IdleCheckInterval < 0 ? 0 : IdleCheckInterval);
            _idleCheckCoroutine = CoroutineRunner.Run(KillIdleControllersEnumerator());
        }

        private void StopIdleCheckInterval()
        {
            if (_idleCheckCoroutine == null) 
                return;
            
            CoroutineRunner.Stop(_idleCheckCoroutine);
            _idleCheckCoroutine = null;
        }
        
        private void RemoveNullControllers() =>
            _pool = _pool.Where(p => p != null).ToList();
        

        private IEnumerator KillIdleControllersEnumerator()
        {
            while (AutoKillIdleControllers)
            {
                yield return _idleCheckIntervalWaitForSecondsRealtime;
                
                RemoveNullControllers();
                int minimumNumberOfControllers = MinimumNumberOfControllers > 0 ? MinimumNumberOfControllers : 0;
                float controllerIdleKillDuration = ControllerIdleKillDuration > 0 ? ControllerIdleKillDuration : 0;
                
                if (_pool.Count <= minimumNumberOfControllers) 
                    continue;
                
                for (int i = _pool.Count - 1; i >= minimumNumberOfControllers; i--)
                {
                    _tempController = _pool[i];
                    
                    if (_tempController.gameObject.activeSelf) 
                        continue;
                    
                    if (_tempController.IdleDuration < controllerIdleKillDuration) 
                        continue;
                    
                    _pool.Remove(_tempController);
                    _tempController.Kill();
                }
            }

            _idleCheckCoroutine = null;
        }
    }
}