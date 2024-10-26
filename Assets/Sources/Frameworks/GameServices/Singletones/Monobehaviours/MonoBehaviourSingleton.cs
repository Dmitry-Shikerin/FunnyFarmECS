using UnityEngine;

namespace Sources.Frameworks.GameServices.Singletones.Monobehaviours
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        protected static T s_instance;

        public static T Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = FindObjectOfType<T>();

                if (s_instance == null)
                    s_instance = new GameObject(typeof(T).Name).AddComponent<T>();

                return s_instance;
            }
        }
    }
}