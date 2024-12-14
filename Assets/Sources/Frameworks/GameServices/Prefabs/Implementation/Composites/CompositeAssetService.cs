using System;
using Cysharp.Threading.Tasks;
using Sources.BoundedContexts.AnimalAnimations.Domain;
using Sources.BoundedContexts.Prefabs;
using Sources.EcsBoundedContexts.DeliveryCars.Domain;
using Sources.EcsBoundedContexts.DeliveryWaterTractors.Domain;
using Sources.EcsBoundedContexts.Farmers.Domain;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Configs;
using Sources.Frameworks.GameServices.ObjectPools.Implementation.Managers;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.MyGameCreator.Achievements.Domain.Configs;
using Sources.Frameworks.MyGameCreator.SkyAndWeawers.Domain;
using Sources.Frameworks.MyLocalization.Domain.Constant;
using Sources.Frameworks.MyLocalization.Domain.Data;

namespace Sources.Frameworks.GameServices.Prefabs.Implementation.Composites
{
    public class CompositeAssetService : ICompositeAssetService
    {
        private readonly IAddressablesAssetLoader _addressablesAssetLoader;
        private readonly IResourcesAssetLoader _resourcesAssetLoader;
        private readonly IAddressablesAssetLoader[] _assetServices;

        public CompositeAssetService(
            IAddressablesAssetLoader addressablesAssetLoader,
            IResourcesAssetLoader resourcesAssetLoader)
        {
            _addressablesAssetLoader = addressablesAssetLoader ??
                                       throw new ArgumentNullException(nameof(addressablesAssetLoader));
            _resourcesAssetLoader =
                resourcesAssetLoader ?? throw new ArgumentNullException(nameof(resourcesAssetLoader));
        }

        public async UniTask LoadAsync()
        {
            await UniTask.WhenAll(
                _resourcesAssetLoader.LoadAsset<AchievementConfigCollector>(PrefabPath.AchievementConfigCollector),
                _resourcesAssetLoader.LoadAsset<SkyAndWeatherCollector>(PrefabPath.SkyAndWeatherCollector),
                _resourcesAssetLoader.LoadAsset<PoolManagerCollector>(PrefabPath.PoolManagerCollector),
                _resourcesAssetLoader.LoadAsset<AnimalConfigCollector>(PrefabPath.AnimalConfigCollector),
                _resourcesAssetLoader.LoadAsset<TreeSwingerCollector>(PrefabPath.TreeSwingerCollector),
                _resourcesAssetLoader.LoadAsset<LocalizationDataBase>(LocalizationConst.LocalizationDataBaseAssetPath),
                _resourcesAssetLoader.LoadAsset<FarmerConfig>(PrefabPath.FarmerConfig),
                _resourcesAssetLoader.LoadAsset<DeliveryCarConfig>(PrefabPath.DeliveryCarConfig),
                _resourcesAssetLoader.LoadAsset<DeliveryWaterTractorConfig>(PrefabPath.DeliveryWaterTractorConfig));
        }

        public void Release()
        {
            _resourcesAssetLoader.ReleaseAll();
            _addressablesAssetLoader.ReleaseAll();
        }
    }
}