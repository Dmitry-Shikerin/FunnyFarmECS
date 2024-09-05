using Sirenix.OdinInspector;
using Sources.BoundedContexts.ChikenCorrals.Presentation;
using Sources.BoundedContexts.PumpkinsPatchs.Presentation;
using Sources.BoundedContexts.TomatoPatchs.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.RootGameObjects.Presentation
{
    public class RootGameObject : MonoBehaviour
    {
        [FoldoutGroup("FirstLocation")]
        [FoldoutGroup("FirstLocation/Patchs")]
        [Required] [SerializeField] private PumpkinPatchView _pumpkinPatchView;
        [FoldoutGroup("FirstLocation/Patchs")]
        [Required] [SerializeField] private TomatoPatchView _tomatoPatchView;

        [FoldoutGroup("FirstLocation/Patchs")] 
        [Required] [SerializeField] private ChickenCorralView _chickenCorralView;
        
        //FirstLokation
        public PumpkinPatchView PumpkinPatchView => _pumpkinPatchView;
        public TomatoPatchView TomatoPatchView => _tomatoPatchView;
        public ChickenCorralView ChickenCorralView => _chickenCorralView;
    }
}