using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Controllers.New;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure.Factories
{
    public class SoundyControllerViewFactory
    {
        private readonly SoundyManager _manager;
        private readonly SoundControllersPool _pool;

        public SoundyControllerViewFactory(SoundyManager manager, SoundControllersPool pool)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _pool = pool ?? throw new ArgumentNullException(nameof(pool));
        }

        public SoundyController Create()
        {
            SoundyController controller = new GameObject(
                "SoundyController", 
                typeof(AudioSource), 
                typeof(SoundyController)).GetComponent<SoundyController>();
            _pool.AddToCollection(controller);
            controller.Construct(_manager, _pool);
            
            return controller;
        }
    }
}