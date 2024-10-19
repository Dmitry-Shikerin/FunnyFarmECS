using Sirenix.OdinInspector;
using Sources.BoundedContexts.UiSelectables.Presentation;
using Sources.Frameworks.GameServices.Volumes.Presentations;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.UI.Huds;
using Sources.Frameworks.UiFramework.Views.Presentations.Implementation;
using UnityEngine;

namespace Sources.BoundedContexts.Huds.Presentations
{
    public class GameplayHud : MonoBehaviour, IHud
    {
        [FoldoutGroup("UiFramework")]
        [Required] [SerializeField] private UiCollector _uiCollector;
        
        //[FoldoutGroup("Upgrades")]
        //[Required] [SerializeField] private UpgradeView _characterAttackUpgradeView;
        //[FoldoutGroup("Upgrades")]
        //[Required] [SerializeField] private UpgradeView _characterHealthUpgradeView;
        //[FoldoutGroup("Upgrades")]
        //[Required] [SerializeField] private UpgradeView _nukeAbilityUpgradeVieew;
        //[FoldoutGroup("Upgrades")]
        //[Required] [SerializeField] private UpgradeView _flamethrowerAbilityUpgradeView;
        
        //[FoldoutGroup("Wallet")]
        //[Required] [SerializeField] private PlayerWalletView _playerWalletView;
        
        [FoldoutGroup("Volume")]
        [Required] [SerializeField] private VolumeView _musicVolumeView;
        [FoldoutGroup("Volume")]
        [Required] [SerializeField] private VolumeView _soundVolumeView;
        
        [FoldoutGroup("Locations")] 
        [FoldoutGroup("Locations/First")] 
        [Required] [SerializeField] private UiSelectableView _pumpkinPatchView;
        [FoldoutGroup("Locations/First")] 
        [Required] [SerializeField] private UiSelectableView _tomatoPatchView;
        [FoldoutGroup("Locations/First")] 
        [Required] [SerializeField] private UiSelectableView _chickenCorralView;
        [FoldoutGroup("Locations/First")] 
        [Required] [SerializeField] private UiSelectableView _onionPatchView;
        [FoldoutGroup("Locations/First")] 
        [Required] [SerializeField] private UiSelectableView _cabbagePatchView;
        [FoldoutGroup("Locations/First")] 
        [Required] [SerializeField] private UiSelectableView _jeepView;
        [FoldoutGroup("Locations/First")] 
        [Required] [SerializeField] private UiSelectableView _truckView;
        [FoldoutGroup("Locations/First")] 
        [Required] [SerializeField] private UiSelectableView _dogView;
        [FoldoutGroup("Locations/First")]
        [Required] [SerializeField] private UiSelectableView _catView;
        [FoldoutGroup("Locations/First")]
        [Required] [SerializeField] private UiSelectableView _houseView;
        [FoldoutGroup("Locations/First")]
        [Required] [SerializeField] private UiSelectableView _woodshedView;
        [FoldoutGroup("Locations/First")]
        [Required] [SerializeField] private UiSelectableView _stableView;
        
        // [FoldoutGroup("Locations/Second")]
        // [Required] [SerializeField] private UiSelectableView _pigPenView;
        // [FoldoutGroup("Locations/Second")]
        // [Required] [SerializeField] private UiSelectableView _cowPenView;
        // [FoldoutGroup("Locations/Second")]
        // [Required] [SerializeField] private UiSelectableView _rabbitPenView;
        // [FoldoutGroup("Locations/Second")]
        // [Required] [SerializeField] private UiSelectableView _sheepPenView;
        // [FoldoutGroup("Locations/Second")]
        // [Required] [SerializeField] private UiSelectableView _goosePenView;
        // [FoldoutGroup("Locations/Second")]
        // [Required] [SerializeField] private UiSelectableView _watermillView;
        
        //[FoldoutGroup("Achievements")]
        //[Required] [SerializeField] private AchievementView _popUpAchievementView;
        //[FoldoutGroup("Achievements")]
        //[Required] [SerializeField] private List<AchievementView> _achievementViews;
        
        //[FoldoutGroup("Ad")]
        //[Required] [SerializeField] private AdvertisingAfterWaveView _advertisingView;
        
        public UiCollector UiCollector => _uiCollector;
        
        //public UpgradeView CharacterHealthUpgradeView => _characterHealthUpgradeView;
        //public UpgradeView CharacterAttackUpgradeView => _characterAttackUpgradeView;
        //public UpgradeView NukeAbilityUpgradeView => _nukeAbilityUpgradeVieew;
        //public UpgradeView FlamethrowerAbilityUpgradeView => _flamethrowerAbilityUpgradeView;
        //
        //public PlayerWalletView PlayerWalletView => _playerWalletView;
        public VolumeView MusicVolumeView => _musicVolumeView;
        public VolumeView SoundVolumeView => _soundVolumeView;
        //public AchievementView PopUpAchievementView => _popUpAchievementView;
        //public IReadOnlyList<AchievementView> AchievementViews => _achievementViews;
        //public AdvertisingAfterWaveView AdvertisingView => _advertisingView;
        
        public UiSelectableView PumpkinPatchView => _pumpkinPatchView;
        public UiSelectableView TomatoPatchView => _tomatoPatchView;
        public UiSelectableView ChickenCorralView => _chickenCorralView;
        public UiSelectableView OnionPatchView => _onionPatchView;
        public UiSelectableView CabbagePatchView => _cabbagePatchView;
        public UiSelectableView JeepView => _jeepView;
        public UiSelectableView TruckView => _truckView;
        public UiSelectableView DogView => _dogView;
        public UiSelectableView CatView => _catView;
        public UiSelectableView HouseView => _houseView;
        public UiSelectableView WoodshedView => _woodshedView;
        public UiSelectableView StableView => _stableView;
        // public UiSelectableView PigPenView => _pigPenView;
        // public UiSelectableView CowPenView => _cowPenView;
        // public UiSelectableView RabbitPenView => _rabbitPenView;
        // public UiSelectableView SheepPenView => _sheepPenView;
        // public UiSelectableView GoosePenView => _goosePenView;
    }
}