using Sources.BoundedContexts.Abilities.Infrastructure.Factories.Controllers;
using Sources.BoundedContexts.Abilities.Infrastructure.Factories.Views;
using Sources.BoundedContexts.ExplosionBodies.Infrastructure.Factories.Views.Implementation;
using Sources.BoundedContexts.GameCompleted.Infrastructure.Services.Implementation;
using Sources.BoundedContexts.GameCompleted.Infrastructure.Services.Interfaces;
using Sources.BoundedContexts.Healths.Infrastructure.Factories.Views;
using Sources.BoundedContexts.PlayerWallets.Infrastructure.Factories.Views;
using Sources.BoundedContexts.PumpkinsPatchs.Infrastructure;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
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
            
            //ExplosionBodyBloody
            Container.Bind<ExplosionBodyBloodyViewFactory>().AsSingle();
            Container.Bind<ExplosionBodyViewFactory>().AsSingle();
            
            //ApplyAbilities
            Container.Bind<AbilityApplierPresenterFactory>().AsSingle();
            Container.Bind<AbilityApplierViewFactory>().AsSingle();
            
            //Upgrades
            Container.Bind<UpgradeViewFactory>().AsSingle();
            
            //Pumpkins
            Container.Bind<PumpkinsPatchViewFactory>().AsSingle();
            
            //Selectables
            Container.Bind<ISelectableService>().To<SelectableService>().AsSingle();
        }
    }
}