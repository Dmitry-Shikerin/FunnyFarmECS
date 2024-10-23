using System.Collections;
using Sources.Frameworks.GameServices.Singletones.Monobehaviours;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Infrastructure
{
    public class CoroutineRunner : MonoBehaviourSingleton<CoroutineRunner>
    {
        public static Coroutine Run(IEnumerator coroutine) =>
            Instance.StartCoroutine(coroutine);

        public static void Stop(Coroutine coroutine) =>
            Instance.StopCoroutine(coroutine);
    }
}