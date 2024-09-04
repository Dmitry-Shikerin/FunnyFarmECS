using Sirenix.OdinInspector;
using Sources.BoundedContexts.PumpkinsPatchs.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.RootGameObjects.Presentation
{
    public class RootGameObject : MonoBehaviour
    {
        [FoldoutGroup("FirstLocation")]
        [FoldoutGroup("FirstLocation/PumpkinPatch")]
        [Required] [SerializeField] private PumpkinPatchView _pumpkinPatchView;
        
        //FirstLokation
        public PumpkinPatchView PumpkinPatchView => _pumpkinPatchView;
    }
}