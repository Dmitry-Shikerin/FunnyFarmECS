using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure.Factories;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers
{
    public class SoundControllersPool
    {
        private readonly Transform _parentTransform;
        private readonly SoundySettings _settings;
        private readonly SoundyManager _manager;
        private List<SoundyController> _pool = new ();
        private List<SoundyController> _collection = new ();
        
        private Coroutine _idleCheckCoroutine;
        private WaitForSecondsRealtime _interval;
        private SoundyController _tempController;
        private SoundyControllerFactory _factory;

        public SoundControllersPool(
            Transform parentTransform,
            SoundySettings settings,
            SoundyManager manager)
        {
            _parentTransform = parentTransform;
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        public IReadOnlyList<SoundyController> Pool => _pool;
        public IReadOnlyList<SoundyController> Collection => _collection;
        private bool IsAutoKillIdle => _settings.AutoKillIdleControllers;
        private float KillDuration => _settings.ControllerIdleKillDuration;
        private float IdleCheckInterval => _settings.IdleCheckInterval;
        public int MinCount => _settings.MinimumNumberOfControllers;
        

        public void Initialize(SoundyControllerFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _interval = new WaitForSecondsRealtime(IdleCheckInterval < 0 ? 0 : IdleCheckInterval);
            PopulatePool(SoundySettings.Instance.MinimumNumberOfControllers);
            StartIdleCheckInterval();
        }

        public void Destroy()
        {
            StopIdleCheck();
            ClearPool();
        }

        public void ClearPool(bool keepMinimumNumberOfControllers = false)
        {
            if (keepMinimumNumberOfControllers)
            {
                RemoveNullControllers();
                
                if (_pool.Count <= MinCount)
                    return;

                int killedControllersCount = 0;
                
                for (int i = _pool.Count - 1; i >= MinCount; i--)
                {
                    SoundyController controller = _pool[i];
                    _pool.Remove(controller);
                    controller.Destroy();
                    killedControllersCount++;
                }

                return;
            }

            _collection.ForEach(controller => controller.Destroy());
            _pool.Clear();
        }
        
        public SoundyController Get()
        {
            RemoveNullControllers();
            
            if (_pool.Count <= 0)
                ReturnToPool(_factory.Create());
            
            SoundyController controller = _pool[0];
            _pool.Remove(controller);
            controller.gameObject.SetActive(true);
            
            return controller;
        }

        public void AddToCollection(SoundyController controller)
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
        
        public void ReturnToPool(SoundyController controller)
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
            if (IsAutoKillIdle == false)
                return;
            
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
            _pool = _pool.Where(controller => controller != null).ToList();

        private IEnumerator DestroyIdleControllers()
        {
            while (IsAutoKillIdle)
            {
                yield return _interval;
                
                RemoveNullControllers();
                int minCount = MinCount > 0 ? MinCount : 0;
                float killDuration = KillDuration > 0 ? KillDuration : 0;
                
                if (_pool.Count <= minCount) 
                    continue;
                
                for (int i = _pool.Count - 1; i >= minCount; i--)
                {
                    _tempController = _pool[i];
                    
                    if (_tempController.gameObject.activeSelf) 
                        continue;
                    
                    if (_tempController.IdleDuration < killDuration) 
                        continue;
                    
                    _pool.Remove(_tempController);
                    _tempController.Destroy();
                }
            }

            _idleCheckCoroutine = null;
        }
    }
}