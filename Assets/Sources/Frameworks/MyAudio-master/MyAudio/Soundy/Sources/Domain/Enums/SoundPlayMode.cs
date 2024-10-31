namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Enums
{
    /// <summary> The order in which clips will be played when you repeatedly fire the Play (sound) method </summary>
    public enum SoundPlayMode
    {
        /// <summary> Sounds are played randomly from a sounds list and refilled after all have been played. This uses true no-repeat, so even when all the sounds in the list have been played, it will not play the previous sound again on the next pass </summary>
        Random = 0,
        /// <summary> Sounds are played in the order they have been added to the sounds list. This option has additional settings </summary>
        Sequence = 1
    }
}