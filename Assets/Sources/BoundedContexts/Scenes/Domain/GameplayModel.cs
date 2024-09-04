using System.Collections.Generic;
using Sources.BoundedContexts.PlayerWallets.Domain.Models;
using Sources.BoundedContexts.Tutorials.Domain.Models;
using Sources.Frameworks.GameServices.Volumes.Domain.Models.Implementation;
using Sources.Frameworks.MyGameCreator.Achievements.Domain.Models;

namespace Sources.BoundedContexts.Scenes.Domain
{
    public struct GameplayModel
    {
        public GameplayModel(
            PlayerWallet playerWallet,
            Volume musicVolume,
            Volume soundsVolume,
            IEnumerable<Achievement> achievements,
            Tutorial tutorial)
        {
            PlayerWallet = playerWallet;
            MusicVolume = musicVolume;
            SoundsVolume = soundsVolume;
            Achievements = achievements;
            Tutorial = tutorial;
        }

        public PlayerWallet PlayerWallet { get; }
        public Volume MusicVolume { get; }
        public Volume SoundsVolume { get; }
        public IEnumerable<Achievement> Achievements { get; }
        public Tutorial Tutorial { get; }
    }
}