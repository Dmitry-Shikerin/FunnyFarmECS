using System;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Domain.Domain;
using Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Interfaces;
using Sources.Frameworks.YandexSdkFramework.PlayerAccounts.Interfaces;

namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ButtonCommands.Implementation
{
    public class PlayerAccountAuthorizeButtonCommand : IButtonCommand
    {
        private readonly IPlayerAccountAuthorizeService _playerAccountAuthorizeService;

        public PlayerAccountAuthorizeButtonCommand(
            IPlayerAccountAuthorizeService playerAccountAuthorizeService)
        {
            _playerAccountAuthorizeService = playerAccountAuthorizeService ??
                                             throw new ArgumentNullException(nameof(playerAccountAuthorizeService));
        }

        public ButtonCommandId Id => ButtonCommandId.PlayerAccountAuthorize;

        public void Handle()
        {
            //TODO здесь нужно выкинуть игрока на хад
            _playerAccountAuthorizeService.Authorize(() =>
            {
            });
        }
    }
}