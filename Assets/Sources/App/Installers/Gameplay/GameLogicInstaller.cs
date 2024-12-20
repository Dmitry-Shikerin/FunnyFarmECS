using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sources.BoundedContexts.Abilities.Infrastructure.Factories.Controllers;
using Sources.BoundedContexts.Abilities.Infrastructure.Factories.Views;
using Sources.BoundedContexts.CabbagePatches.Infrastructure;
using Sources.BoundedContexts.Cats.Infrastructure;
using Sources.BoundedContexts.ChikenCorrals.Infrastructure;
using Sources.BoundedContexts.CowPens.Infrastructure;
using Sources.BoundedContexts.Dogs.Infrastructure;
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
using Sources.BoundedContexts.UiSelectables.Infrastructure;
using Sources.BoundedContexts.Upgrades.Infrastructure.Factories.Views;
using Sources.BoundedContexts.Watermills.Infrastructure;
using Sources.BoundedContexts.Woodsheds.Infrastructure;

namespace Sources.App.Installers.Gameplay
{
    public class GameLogicInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            //PlayerWallet
            container.Bind<PlayerWalletViewFactory>();
            
            //Healths
            container.Bind<HealthBarViewFactory>();
            
            //ApplyAbilities
            container.Bind<AbilityApplierPresenterFactory>();
            container.Bind<AbilityApplierViewFactory>();
            
            //Upgrades
            container.Bind<UpgradeViewFactory>();
            
            //FirstLocation
            container.Bind<PumpkinsPatchViewFactory>();
            container.Bind<TomatoPatchViewFactory>();
            container.Bind<ChickenCorralViewFactory>();
            container.Bind<OnionPatchViewFactory>();
            container.Bind<CabbagePatchViewFactory>();
            container.Bind<JeepViewFactory>();
            container.Bind<TruckViewFactory>();
            container.Bind<DogViewFactory>();
            container.Bind<CatViewFactory>();
            container.Bind<HouseViewFactory>();
            container.Bind<WoodshedViewFactory>();
            container.Bind<StableViewFactory>();
            
            //SecondLocation
            container.Bind<PigPenViewFactory>();
            container.Bind<CowPenViewFactory>();
            container.Bind<RabbitPenViewFactory>();
            container.Bind<SheepPenViewFactory>();
            container.Bind<GoosePenViewFactory>();
            container.Bind<WatermillViewFactory>();
            
            //Selectables
            container.Bind<ISelectableService, SelectableService>();
            container.Bind<UiSelectableViewFactory>();
        }
    }
}