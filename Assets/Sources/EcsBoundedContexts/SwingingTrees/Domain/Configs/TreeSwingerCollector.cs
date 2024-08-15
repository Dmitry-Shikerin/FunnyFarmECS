using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Sources.SwingingTrees.Domain.Configs
{
    [CreateAssetMenu(
        fileName = "TreeSwingerCollector", 
        menuName = "Configs/TreeSwingerCollector", 
        order = 51)]
    public class TreeSwingerCollector : ScriptableObject
    {
        [field: SerializeField] public List<TreeSwingerConfig> Configs { get; private set; }
        [PropertySpace(10)]
        [SerializeField] private string _configId;
        
#if UNITY_EDITOR
        public void RemoveConfig(TreeSwingerConfig wave)
        {
            AssetDatabase.RemoveObjectFromAsset(wave);
            Configs.Remove(wave);
            AssetDatabase.SaveAssets();
        }
        
        [UsedImplicitly]
        [ResponsiveButtonGroup("Buttons")]
        private void CreateConfig()
        {
            if (string.IsNullOrWhiteSpace(_configId))
                return;

            if (string.IsNullOrEmpty(_configId))
                return;

            if (Configs.Any(config => config.Id == _configId))
            {
                Debug.Log($"this id not unique: {_configId}");
                return;
            }
            
            TreeSwingerConfig treeSwingerConfig = CreateInstance<TreeSwingerConfig>();
            treeSwingerConfig.Parent = this;
            AssetDatabase.AddObjectToAsset(treeSwingerConfig, this);
            treeSwingerConfig.Id = _configId;
            treeSwingerConfig.name = $"{_configId}_TreeSwingerConfig";
            Configs.Add(treeSwingerConfig);
            AssetDatabase.SaveAssets();
        }

        public void CreateConfig(string id, string titleId, string descriptionId, Sprite sprite)
        {
            _configId = id;
            CreateConfig();
        }
#endif
    }
}