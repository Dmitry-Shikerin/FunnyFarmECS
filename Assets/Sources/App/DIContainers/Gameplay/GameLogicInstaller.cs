using Sources.BoundedContexts.Abilities.Infrastructure.Factories.Controllers;
using Sources.BoundedContexts.Abilities.Infrastructure.Factories.Views;
using Sources.BoundedContexts.CharacterHealths.Infrastructure.Factories.Views;
using Sources.BoundedContexts.ExplosionBodies.Infrastructure.Factories.Views.Implementation;
using Sources.BoundedContexts.GameCompleted.Infrastructure.Services.Implementation;
using Sources.BoundedContexts.GameCompleted.Infrastructure.Services.Interfaces;
using Sources.BoundedContexts.Healths.Infrastructure.Factories.Views;
using Sources.BoundedContexts.NukeAbilities.Infrastructure.Factories.Controllers;
using Sources.BoundedContexts.NukeAbilities.Infrastructure.Factories.Views;
using Sources.BoundedContexts.PlayerWallets.Infrastructure.Factories.Views;
using Sources.BoundedContexts.Upgrades.Infrastructure.Factories.Views;
using Zenject;

namespace Sources.App.DIContainers.Gameplay
{
    public class GameLogicInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //GameCompleted
            Container.Bind<IGameCompletedService>().To<GameCompletedService>().AsSingle();
            
            //PlayerWallet
            Container.Bind<PlayerWalletViewFactory>().AsSingle();
            
            //Healths
            Container.Bind<HealthBarViewFactory>().AsSingle();
            
            //Characters
            Container.Bind<CharacterHealthViewFactory>().AsSingle();
            
            //ExplosionBodyBloody
            Container.Bind<ExplosionBodyBloodyViewFactory>().AsSingle();
            Container.Bind<ExplosionBodyViewFactory>().AsSingle();
            
            //ApplyAbilities
            Container.Bind<AbilityApplierPresenterFactory>().AsSingle();
            Container.Bind<AbilityApplierViewFactory>().AsSingle();
            
            Container.Bind<NukeAbilityPresenterFactory>().AsSingle();
            Container.Bind<NukeAbilityViewFactory>().AsSingle();
            
            //Upgrades
            Container.Bind<UpgradeViewFactory>().AsSingle();
        }
    }
}