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
        private List<SoundyController> _controllers = new List<SoundyController>();
        private Coroutine _idleCheckCoroutine;
        private WaitForSecondsRealtime _idleCheckIntervalWaitForSecondsRealtime;
        private SoundyController _tempController;

        public SoundControllersPool(Transform parentTransform)
        {
            _parentTransform = parentTransform;
        }

        public IReadOnlyCollection<SoundyController> Controllers => _controllers;
        private bool AutoKillIdleControllers => SoundySettings.Instance.AutoKillIdleControllers;
        private float ControllerIdleKillDuration => SoundySettings.Instance.ControllerIdleKillDuration;
        private float IdleCheckInterval => SoundySettings.Instance.IdleCheckInterval;
        private int MinimumNumberOfControllers => SoundySettings.Instance.MinimumNumberOfControllers;
        

        public void Initialize()
        {
            if (AutoKillIdleControllers == false)
                return;
            
            StartIdleCheckInterval();
        }

        public void Destroy() =>
            StopIdleCheckInterval();
        
        public void ClearPool(bool keepMinimumNumberOfControllers = false)
        {
            if (keepMinimumNumberOfControllers)
            {
                RemoveNullControllersFromThePool();
                
                if (_controllers.Count <= MinimumNumberOfControllers)
                    return;

                int killedControllersCount = 0;
                
                for (int i = _controllers.Count - 1; i >= MinimumNumberOfControllers; i--)
                {
                    SoundyController controller = _controllers[i];
                    _controllers.Remove(controller);
                    controller.Kill();
                    killedControllersCount++;
                }

                return;
            }

            SoundyController.KillAll();
            _controllers.Clear();
        }
        
        public SoundyController GetControllerFromPool()
        {
            RemoveNullControllersFromThePool();
            if (_controllers.Count <= 0)
                PutControllerInPool(SoundyController.CreateController());
            
            SoundyController controller = _controllers[0];
            _controllers.Remove(controller);
            controller.gameObject.SetActive(true);
            
            return controller;
        }
        
        public void PopulatePool(int numberOfControllers)
        {
            RemoveNullControllersFromThePool();
            
            if (numberOfControllers < 1) 
                return;
            
            for (int i = 0; i < numberOfControllers; i++)
                PutControllerInPool(SoundyController.CreateController());
        }
        
        public void PutControllerInPool(SoundyController controller)
        {
            if (controller == null) 
                return;
            
            if (_controllers.Contains(controller) == false) 
                _controllers.Add(controller);
            
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
        
        private void RemoveNullControllersFromThePool() =>
            _controllers = _controllers.Where(p => p != null).ToList();
        

        private IEnumerator KillIdleControllersEnumerator()
        {
            while (AutoKillIdleControllers)
            {
                yield return _idleCheckIntervalWaitForSecondsRealtime;
                
                RemoveNullControllersFromThePool();
                int minimumNumberOfControllers = MinimumNumberOfControllers > 0 ? MinimumNumberOfControllers : 0;
                float controllerIdleKillDuration = ControllerIdleKillDuration > 0 ? ControllerIdleKillDuration : 0;
                
                if (_controllers.Count <= minimumNumberOfControllers) 
                    continue;
                
                for (int i = _controllers.Count - 1; i >= minimumNumberOfControllers; i--)
                {
                    _tempController = _controllers[i];
                    
                    if (_tempController.gameObject.activeSelf) 
                        continue;
                    
                    if (_tempController.IdleDuration < controllerIdleKillDuration) 
                        continue;
                    
                    _controllers.Remove(_tempController);
                    _tempController.Kill();
                }
            }

            _idleCheckCoroutine = null;
        }
    }
}