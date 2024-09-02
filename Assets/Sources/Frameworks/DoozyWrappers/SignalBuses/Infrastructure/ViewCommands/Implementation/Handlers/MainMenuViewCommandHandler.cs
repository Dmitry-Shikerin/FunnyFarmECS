namespace Sources.Frameworks.DoozyWrappers.SignalBuses.Infrastructure.ViewCommands.Implementation.Handlers
{
    public class MainMenuViewCommandHandler : ViewCommandHandler
    {
        public MainMenuViewCommandHandler(
            PauseCommand pauseCommand,
            UnPauseCommand unPauseCommand,
            SaveVolumeCommand saveVolumeCommand,
            ClearSavesCommand clearSavesCommand)
        {
            Add(pauseCommand);
            Add(unPauseCommand);
            Add(saveVolumeCommand);
            Add(clearSavesCommand);
        }
    }
}