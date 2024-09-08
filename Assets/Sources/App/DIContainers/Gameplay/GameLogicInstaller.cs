using Sources.BoundedContexts.Abilities.Infrastructure.Factories.Controllers;
using Sources.BoundedContexts.Abilities.Infrastructure.Factories.Views;
using Sources.BoundedContexts.CabbagePatches.Infrastructure;
using Sources.BoundedContexts.Cats.Infrastructure;
using Sources.BoundedContexts.ChikenCorrals.Infrastructure;
using Sources.BoundedContexts.CowPens.Infrastructure;
using Sources.BoundedContexts.Dogs.Domain;
using Sources.BoundedContexts.Dogs.Infrastructure;
using Sources.BoundedContexts.ExplosionBodies.Infrastructure.Factories.Views.Implementation;
using Sources.BoundedContexts.GameCompleted.Infrastructure.Services.Implementation;
using Sources.BoundedContexts.GameCompleted.Infrastructure.Services.Interfaces;
using Sources.BoundedContexts.GoosePens.Infrastructure;
using Sources.BoundedContexts.Healths.Infrastructure.Factories.Views;
using Sources.BoundedContexts.Houses.Infrastructure;
using Sources.BoundedContexts.Jeeps.Infrastructure;
using Sources.BoundedContexts.OnionPatches.Infrastructure;
using Sources.BoundedContexts.PigPens.Infrastructure;
using Sources.BoundedContexts.PlayerWallets.Infrastructure.Factories.Views;
using Sources.BoundedContexts.PumpkinsPatchs.Infrastructure;
using Sources.BoundedContexts.RabbitPens.Infrastructure;
using Sources.BoundedContexts.SelectableItems.Infrastructure;
using Sources.BoundedContexts.SheepPens.Infrastructure;
using Sources.BoundedContexts.Stables.Implementation;
using Sources.BoundedContexts.TomatoPatchs.Infrastructure;
using Sources.BoundedContexts.Trucks.Infrastructure;
using Sources.BoundedContexts.Upgrades.Infrastructure.Factories.Views;
using Sources.BoundedContexts.Watermills.Infrastructure;
using Sources.BoundedContexts.Woodsheds.Domain;
using Sources.BoundedContexts.Woodsheds.Infrastructure;
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
            
            //FirstLocation
            Container.Bind<PumpkinsPatchViewFactory>().AsSingle();
            Container.Bind<TomatoPatchViewFactory>().AsSingle();
            Container.Bind<ChickenCorralViewFactory>().AsSingle();
            Container.Bind<OnionPatchViewFactory>().AsSingle();
            Container.Bind<CabbagePatchViewFactory>().AsSingle();
            Container.Bind<JeepViewFactory>().AsSingle();
            Container.Bind<TruckViewFactory>().AsSingle();
            Container.Bind<DogViewFactory>().AsSingle();
            Container.Bind<CatViewFactory>().AsSingle();
            Container.Bind<HouseViewFactory>().AsSingle();
            Container.Bind<WoodshedViewFactory>().AsSingle();
            Container.Bind<StableViewFactory>().AsSingle();
            
            //SecondLocation
            Container.Bind<PigPenViewFactory>().AsSingle();
            Container.Bind<CowPenViewFactory>().AsSingle();
            Container.Bind<RabbitPenViewFactory>().AsSingle();
            Container.Bind<SheepPenViewFactory>().AsSingle();
            Container.Bind<GoosePenViewFactory>().AsSingle();
            Container.Bind<WatermillViewFactory>().AsSingle();
            
            //Selectables
            Container.Bind<ISelectableService>().To<SelectableService>().AsSingle();
        }
    }
}