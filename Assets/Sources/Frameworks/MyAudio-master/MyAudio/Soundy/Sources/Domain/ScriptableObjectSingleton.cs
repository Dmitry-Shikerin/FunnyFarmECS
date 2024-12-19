using MyAudios.MyUiFramework.Utils;
using UnityEngine;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain
{
    public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        private static T s_instance;

        public static T Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = MyAssetUtils.GetScriptableObject<T>(AssetName, ResourcesPath);
                
                return s_instance;
            }
        }
        
        protected static string AssetName { get; set; }
        protected static string ResourcesPath { get; set; }
    }
}