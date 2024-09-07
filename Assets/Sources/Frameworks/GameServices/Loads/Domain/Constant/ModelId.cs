using System.Collections.Generic;
using System.Linq;
using Sources.BoundedContexts.HealthBoosters.Domain;
using Sources.BoundedContexts.PlayerWallets.Domain.Models;
using Sources.BoundedContexts.PumpkinsPatchs.Domain;
using Sources.BoundedContexts.Tutorials.Domain.Models;
using Sources.BoundedContexts.Upgrades.Domain.Models;
using Sources.Frameworks.Domain.Interfaces.Entities;
using Sources.Frameworks.GameServices.DailyRewards.Domain;
using Sources.Frameworks.GameServices.Volumes.Domain.Models.Implementation;
using Sources.Frameworks.MyGameCreator.Achievements.Domain.Models;

namespace Sources.Frameworks.GameServices.Loads.Domain.Constant
{
    public static class ModelId
    {
        //upgrades
        public const string HealthUpgrade = "HealthUpgrade";
        public const string AttackUpgrade = "AttackUpgrade";
        public const string NukeUpgrade = "NukeUpgrade";
        public const string FlamethrowerUpgrade = "FlamethrowerUpgrade";

        //gameModels
        public const string PlayerWallet = "PlayerWallet";
        public const string Inventory = "Inventory";
        public const string FirstPumpkinsPatch = "FirstPumpinsPatch";
        public const string TomatoPatch = "TomatoPatch";        
        public const string ChickenCorral = "ChickenCorral";
        public const string CabbagePatch = "CabbagePatch";
        public const string OnionPatch = "OnionPatch";
        public const string Woodshed = "Woodshed";
        public const string Dog = "Dog";
        public const string Jeep = "Jeep";        
        public const string Truck = "Truck";
        
        //Items
        public const string Pumpkin = "Pumpkin";
        public const string Tomato = "Tomato";

        //commonModels
        public const string DailyReward = "DailyReward";
        public const string HealthBooster = "HealthBooster";
        public const string ScoreCounter = "ScoreCounter";
        public const string MainMenu = "MainMenu";
        public const string MusicVolume = "MusicVolume";
        public const string SoundsVolume = "SoundVolume";
        public const string Tutorial = "Tutorial";
        public const string Gameplay = "Gameplay";
        
        //Achievements
        public const string FirstEnemyKillAchievement = "FirstEnemyKillAchievement";
        public const string FirstUpgradeAchievement = "FirstUpgradeAchievement";
        public const string FirstHealthBoosterUsageAchievement = "FirstHealthBoosterUsageAchievement";
        public const string FirstWaveCompletedAchievement = "FirstWaveCompletedAchievement";
        public const string ScullsDiggerAchievement = "ScullsDiggerAchievement";
        public const string MaxUpgradeAchievement = "MaxUpgradeAchievement";
        public const string FiftyWaveCompletedAchievement = "FiftyWaveCompletedAchievement";
        public const string AllAbilitiesUsedAchievementCommand = "AllAbilitiesUsedAchievementCommand";
        public const string CompleteGameWithOneHealthAchievementCommand = "CompleteGameWithOneHealthAchievementCommand";
        
        public static IReadOnlyDictionary<string, EntityData> ModelData { get; } = new Dictionary<string, EntityData>()
        {
             [FirstPumpkinsPatch] = new (FirstPumpkinsPatch, typeof(PumpkinPatch), true),
             [HealthUpgrade] = new (HealthUpgrade, typeof(Upgrade), true),
             [AttackUpgrade] = new (AttackUpgrade, typeof(Upgrade), true),
             [NukeUpgrade] = new (NukeUpgrade, typeof(Upgrade), true),
             [FlamethrowerUpgrade] = new (FlamethrowerUpgrade, typeof(Upgrade), true),
             [PlayerWallet] = new (PlayerWallet, typeof(PlayerWallet), true),
             [MusicVolume] = new (MusicVolume, typeof(Volume), false),
             [SoundsVolume] = new (SoundsVolume, typeof(Volume), false),
             [DailyReward] = new (DailyReward, typeof(DailyReward), false),
             [FirstEnemyKillAchievement] = new (FirstEnemyKillAchievement, typeof(Achievement), false),
             [FirstUpgradeAchievement] = new (FirstUpgradeAchievement, typeof(Achievement), false),
             [FirstHealthBoosterUsageAchievement] = new (FirstHealthBoosterUsageAchievement, typeof(Achievement), false),
             [FirstWaveCompletedAchievement] = new (FirstWaveCompletedAchievement, typeof(Achievement), false),
             [ScullsDiggerAchievement] = new (ScullsDiggerAchievement, typeof(Achievement), false),
             [MaxUpgradeAchievement] = new (MaxUpgradeAchievement, typeof(Achievement), false),
             [FiftyWaveCompletedAchievement] = new (FiftyWaveCompletedAchievement, typeof(Achievement), false),
             [AllAbilitiesUsedAchievementCommand] = new (AllAbilitiesUsedAchievementCommand, typeof(Achievement), false),
             [CompleteGameWithOneHealthAchievementCommand] = new (CompleteGameWithOneHealthAchievementCommand, typeof(Achievement), false),
             [Tutorial] = new (Tutorial, typeof(Tutorial), false),
             [HealthBooster] = new (HealthBooster, typeof(HealthBooster), false),
        };
        
        public static IReadOnlyList<string> GetIds<T>() 
            where T : IEntity
        {
            return ModelData.Values
                .Where(data => data.Type == typeof(T))
                .Select(data => data.ID)
                .ToList();
        }

        public static IReadOnlyList<string> GetDeleteIds()
        {
            return ModelData.Values
                .Where(data => data.IsDeleted)
                .Select(data => data.ID)
                .ToList();
        }
    }
}