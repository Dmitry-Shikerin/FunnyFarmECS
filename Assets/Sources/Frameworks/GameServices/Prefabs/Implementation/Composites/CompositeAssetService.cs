using System;
using Cysharp.Threading.Tasks;
using Sources.BoundedContexts.ExplosionBodies.Presentation.Implementation;
using Sources.BoundedContexts.Prefabs;
using Sources.Frameworks.GameServices.ObjectPools.Implementation.Managers;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.MyGameCreator.Achievements.Domain.Configs;
using Sources.Frameworks.MyGameCreator.SkyAndWeawers.Domain;

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
            _addressablesAssetLoader = addressablesAssetLoader ?? throw new ArgumentNullException(nameof(addressablesAssetLoader));
            _resourcesAssetLoader = resourcesAssetLoader ?? throw new ArgumentNullException(nameof(resourcesAssetLoader));
        }
        
        public async UniTask LoadAsync()
        {
            await _resourcesAssetLoader.LoadAsset<AchievementConfigCollector>(PrefabPath.AchievementConfigCollector);
            await _resourcesAssetLoader.LoadAsset<SkyAndWeatherCollector>(PrefabPath .SkyAndWeatherCollector);
            await _resourcesAssetLoader.LoadAsset<PoolManagerCollector>(PrefabPath.PoolManagerCollector);
            // await _resourcesAssetLoader.LoadAsset<ExplosionBodyBloodyView>(PrefabPath.ExplosionBodyBloody);
            // await _resourcesAssetLoader.LoadAsset<ExplosionBodyView>(PrefabPath.ExplosionBody);
        }

        public void Release()
        {
            _resourcesAssetLoader.ReleaseAll();
            _addressablesAssetLoader.ReleaseAll();
        }
    }
}