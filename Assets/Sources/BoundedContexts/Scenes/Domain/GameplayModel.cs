using System.Collections.Generic;
using Sources.BoundedContexts.HealthBoosters.Domain;
using Sources.BoundedContexts.NukeAbilities.Domain.Models;
using Sources.BoundedContexts.PlayerWallets.Domain.Models;
using Sources.BoundedContexts.Tutorials.Domain.Models;
using Sources.BoundedContexts.Upgrades.Domain.Models;
using Sources.Frameworks.GameServices.Volumes.Domain.Models.Implementation;
using Sources.Frameworks.MyGameCreator.Achievements.Domain.Models;

namespace Sources.BoundedContexts.Scenes.Domain
{
    public struct GameplayModel
    {
        public GameplayModel(
            Upgrade characterHealthUpgrade,
            Upgrade characterAttackUpgrade,
            Upgrade nukeAbilityUpgrade,
            Upgrade flamethrowerAbilityUpgrade,
            NukeAbility nukeAbility,
            PlayerWallet playerWallet,
            Volume musicVolume,
            Volume soundsVolume,
            IEnumerable<Achievement> achievements,
            HealthBooster healthBooster,
            Tutorial tutorial)
        {
            CharacterHealthUpgrade = characterHealthUpgrade;
            CharacterAttackUpgrade = characterAttackUpgrade;
            NukeAbilityUpgrade = nukeAbilityUpgrade;
            FlamethrowerAbilityUpgrade = flamethrowerAbilityUpgrade;
            NukeAbility = nukeAbility;
            PlayerWallet = playerWallet;
            MusicVolume = musicVolume;
            SoundsVolume = soundsVolume;
            Achievements = achievements;
            HealthBooster = healthBooster;
            Tutorial = tutorial;
        }

        public Upgrade CharacterHealthUpgrade { get; }
        public Upgrade CharacterAttackUpgrade { get; }
        public Upgrade NukeAbilityUpgrade { get; }
        public Upgrade FlamethrowerAbilityUpgrade { get; }
        public NukeAbility NukeAbility { get; }
        public PlayerWallet PlayerWallet { get; }
        public Volume MusicVolume { get; }
        public Volume SoundsVolume { get; }
        public IEnumerable<Achievement> Achievements { get; }
        public HealthBooster HealthBooster { get; }
        public Tutorial Tutorial { get; }
    }
}