using System;
using Sources.Frameworks.GameServices.InputServices;
using Sources.Frameworks.GameServices.Pauses.Services.Implementation;
using Sources.Frameworks.MyGameCreator.Movements.Infrastructure.Factories;
using UnityEngine;

namespace Sources.Frameworks.MyGameCreator
{
    public class MyGameCreator : MonoBehaviour
    {
        private static MyGameCreator s_instance;
        
        public static MyGameCreator Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new GameObject("MyGameCreator").AddComponent<MyGameCreator>();
                
                return s_instance;
            }
        }

        public event Action<float> OnUpdate;
        
        public static NewInputService InputService { get; } = new NewInputService(new PauseService());
        public MovementStateMachineFactory MovementStateMachineFactory { get; } = new MovementStateMachineFactory(InputService);
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            InputService.Initialize();
        }
        
        private void Update()
        {
            InputService.Update(Time.deltaTime);
            OnUpdate?.Invoke(Time.deltaTime);
        }
        
        private void OnDestroy()
        {
            s_instance = null;
            InputService.Destroy();
        }
    }
}