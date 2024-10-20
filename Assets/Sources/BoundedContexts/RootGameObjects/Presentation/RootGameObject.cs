using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.AnimalMovePoints;
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
        [Required] [SerializeField] private PumpkinPatchView _pumpkinPatchView;
        [FoldoutGroup("FirstLocation")] 
        [Required] [SerializeField] private TomatoPatchView _tomatoPatchView;
        [FoldoutGroup("FirstLocation")] 
        [Required] [SerializeField] private ChickenCorralView _chickenCorralView;
        [FoldoutGroup("FirstLocation")] 
        [Required] [SerializeField] private OnionPatchView _onionPatchView;
        [FoldoutGroup("FirstLocation")] 
        [Required] [SerializeField] private CabbagePatchView _cabbagePatchView;
        [FoldoutGroup("FirstLocation")] 
        [Required] [SerializeField] private JeepView _jeepView;
        [FoldoutGroup("FirstLocation")] 
        [Required] [SerializeField] private TruckView _truckView;
        [FoldoutGroup("FirstLocation")] 
        [Required] [SerializeField] private DogHouseView _dogHouseView;
        [FoldoutGroup("FirstLocation")] 
        [Required] [SerializeField] private List<AnimalMovePoint> _dogMovePoints;
        [FoldoutGroup("FirstLocation")] 
        [Required] [SerializeField] private CatView _catView;
        [FoldoutGroup("FirstLocation")] 
        [Required] [SerializeField] private HouseView _houseView;
        [FoldoutGroup("FirstLocation")] 
        [Required] [SerializeField] private WoodshedView _woodshedView;
        [FoldoutGroup("FirstLocation")] 
        [Required] [SerializeField] private StableView _stableView;
        [FoldoutGroup("FirstLocation")] 
        [Required] [SerializeField] private GameObject _firsLocationRootTrees;
        
        [FoldoutGroup("SecondLocation")]
        [Required] [SerializeField] private PigPenView _pigPenView;
        [FoldoutGroup("SecondLocation")]
        [Required] [SerializeField] private CowPenView _cowPenView;
        [FoldoutGroup("SecondLocation")]
        [Required] [SerializeField] private RabbitPenView _rabbitPenView;
        [FoldoutGroup("SecondLocation")]
        [Required] [SerializeField] private SheepPenView _sheepPenView;
        [FoldoutGroup("SecondLocation")]
        [Required] [SerializeField] private GoosePenView _goosePenView;
        [FoldoutGroup("SecondLocation")]
        [Required] [SerializeField] private WatermillView _watermillView;
        
        //FirstLocation
        public PumpkinPatchView PumpkinPatchView => _pumpkinPatchView;
        public TomatoPatchView TomatoPatchView => _tomatoPatchView;
        public ChickenCorralView ChickenCorralView => _chickenCorralView;
        public OnionPatchView OnionPatchView => _onionPatchView;
        public CabbagePatchView CabbagePatchView => _cabbagePatchView;
        public JeepView JeepView => _jeepView;
        public TruckView TruckView => _truckView;
        public DogHouseView DogHouseView => _dogHouseView;
        public List<AnimalMovePoint> DogMovePoints => _dogMovePoints;
        public CatView CatView => _catView;
        public HouseView HouseView => _houseView;
        public WoodshedView WoodshedView => _woodshedView;
        public StableView StableView => _stableView;
        public GameObject FirstLocationRootTrees => _firsLocationRootTrees;
        
        //SecondLocation
        public PigPenView PigPenView => _pigPenView;
        public CowPenView CowPenView => _cowPenView;
        public RabbitPenView RabbitPenView => _rabbitPenView;
        public SheepPenView SheepPenView => _sheepPenView;
        public GoosePenView GoosePenView => _goosePenView;
        public WatermillView WatermillView => _watermillView;
    }
}