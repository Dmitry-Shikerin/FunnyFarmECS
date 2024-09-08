using Sirenix.OdinInspector;
using Sources.BoundedContexts.CabbagePatches.Presentation;
using Sources.BoundedContexts.Cats.Presentation;
using Sources.BoundedContexts.ChikenCorrals.Presentation;
using Sources.BoundedContexts.CowPens.Presentation;
using Sources.BoundedContexts.Dogs.Presentation;
using Sources.BoundedContexts.GoosePens.Presentation;
using Sources.BoundedContexts.Houses.Presentation;
using Sources.BoundedContexts.Jeeps.Presentation;
using Sources.BoundedContexts.OnionPatches.Presentation;
using Sources.BoundedContexts.PigPens.Presentation;
using Sources.BoundedContexts.PumpkinsPatchs.Presentation;
using Sources.BoundedContexts.RabbitPens.Presentation;
using Sources.BoundedContexts.SheepPens.Presentation;
using Sources.BoundedContexts.Stables.Presentation;
using Sources.BoundedContexts.TomatoPatchs.Presentation;
using Sources.BoundedContexts.Trucks.Presentation;
using Sources.BoundedContexts.Watermills.Presentation;
using Sources.BoundedContexts.Woodsheds.Presentation;
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
        [FoldoutGroup("FirstLocation/Patchs")] 
        [Required] [SerializeField] private OnionPatchView _onionPatchView;
        [FoldoutGroup("FirstLocation/Patchs")] 
        [Required] [SerializeField] private CabbagePatchView _cabbagePatchView;
        [FoldoutGroup("FirstLocation/Tracks")]
        [Required] [SerializeField] private JeepView _jeepView;
        [FoldoutGroup("FirstLocation/Tracks")]
        [Required] [SerializeField] private TruckView _truckView;
        [FoldoutGroup("FirstLocation/Animals")]
        [Required] [SerializeField] private DogView _dogView;
        [FoldoutGroup("FirstLocation/Animals")]
        [Required] [SerializeField] private CatView _catView;
        [FoldoutGroup("FirstLocation/Buildings")]
        [Required] [SerializeField] private HouseView _houseView;
        [FoldoutGroup("FirstLocation/Buildings")]
        [Required] [SerializeField] private WoodshedView _woodshedView;
        [FoldoutGroup("FirstLocation/Buildings")]
        [Required] [SerializeField] private StableView _stableView;
        
        [FoldoutGroup("SecondLocation")]
        [FoldoutGroup("SecondLocation/Pens")]
        [Required] [SerializeField] private PigPenView _pigPenView;
        [FoldoutGroup("SecondLocation/Pens")]
        [Required] [SerializeField] private CowPenView _cowPenView;
        [FoldoutGroup("SecondLocation/Pens")]
        [Required] [SerializeField] private RabbitPenView _rabbitPenView;
        [FoldoutGroup("SecondLocation/Pens")]
        [Required] [SerializeField] private SheepPenView _sheepPenView;
        [FoldoutGroup("SecondLocation/Pens")]
        [Required] [SerializeField] private GoosePenView _goosePenView;
        [FoldoutGroup("SecondLocation/Buildings")]
        [Required] [SerializeField] private WatermillView _watermillView;
        
        //FirstLocation
        public PumpkinPatchView PumpkinPatchView => _pumpkinPatchView;
        public TomatoPatchView TomatoPatchView => _tomatoPatchView;
        public ChickenCorralView ChickenCorralView => _chickenCorralView;
        public OnionPatchView OnionPatchView => _onionPatchView;
        public CabbagePatchView CabbagePatchView => _cabbagePatchView;
        public JeepView JeepView => _jeepView;
        public TruckView TruckView => _truckView;
        public DogView DogView => _dogView;
        public CatView CatView => _catView;
        public HouseView HouseView => _houseView;
        public WoodshedView WoodshedView => _woodshedView;
        public StableView StableView => _stableView;
        
        //SecondLocation
        public PigPenView PigPenView => _pigPenView;
        public CowPenView CowPenView => _cowPenView;
        public RabbitPenView RabbitPenView => _rabbitPenView;
        public SheepPenView SheepPenView => _sheepPenView;
        public GoosePenView GoosePenView => _goosePenView;
        public WatermillView WatermillView => _watermillView;
    }
}