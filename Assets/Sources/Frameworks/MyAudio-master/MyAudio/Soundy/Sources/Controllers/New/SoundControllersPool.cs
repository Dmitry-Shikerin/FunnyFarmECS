using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure.Factories;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers.New
{
    public class SoundControllersPool
    {
        private readonly Transform _parentTransform;
        private readonly NewSoundyManager _manager;
        private List<NewSoundyController> _pool = new ();
        private List<NewSoundyController> _collection = new ();
        
        private Coroutine _idleCheckCoroutine;
        private WaitForSecondsRealtime _idleCheckIntervalWaitForSecondsRealtime;
        private NewSoundyController _tempController;
        private NewSoundyControllerViewFactory _factory;

        public SoundControllersPool(Transform parentTransform, NewSoundyManager manager)
        {
            _parentTransform = parentTransform;
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        public IReadOnlyList<NewSoundyController> Pool => _pool;
        public IReadOnlyList<NewSoundyController> Collection => _collection;
        private bool AutoKillIdleControllers => SoundySettings.Instance.AutoKillIdleControllers;
        private float ControllerIdleKillDuration => SoundySettings.Instance.ControllerIdleKillDuration;
        private float IdleCheckInterval => SoundySettings.Instance.IdleCheckInterval;
        public int MinimumNumberOfControllers => SoundySettings.Instance.MinimumNumberOfControllers;
        

        public void Initialize(NewSoundyControllerViewFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            
            if (AutoKillIdleControllers == false)
                return;
            
            StartIdleCheckInterval();
        }

        public void Destroy() =>
            StopIdleCheck();
        
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
                    controller.Destroy();
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
                ReturnToPool(_factory.Create());
            
            NewSoundyController controller = _pool[0];
            _pool.Remove(controller);
            controller.gameObject.SetActive(true);
            
            return controller;
        }

        public void AddToCollection(NewSoundyController controller)
        {
            _collection.Add(controller);
        }
        
        public void PopulatePool(int count)
        {
            RemoveNullControllers();
            
            if (count < 1) 
                return;
            
            for (int i = 0; i < count; i++)
                ReturnToPool(_factory.Create());
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
            _idleCheckCoroutine = CoroutineRunner.Run(DestroyIdleControllers());
        }

        private void StopIdleCheck()
        {
            if (_idleCheckCoroutine == null) 
                return;
            
            CoroutineRunner.Stop(_idleCheckCoroutine);
            _idleCheckCoroutine = null;
        }
        
        private void RemoveNullControllers() =>
            _pool = _pool.Where(p => p != null).ToList();
        

        private IEnumerator DestroyIdleControllers()
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
                    _tempController.Destroy();
                }
            }

            _idleCheckCoroutine = null;
        }
    }
}