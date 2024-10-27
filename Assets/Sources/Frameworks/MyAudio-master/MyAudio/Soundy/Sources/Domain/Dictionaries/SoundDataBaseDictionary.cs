using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;
using Sources.Frameworks.Utils.Dictionaries;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Dictionaries
{
    [Serializable]
    public class SoundDataBaseDictionary : SerializedDictionary<string, SoundDatabase>
    {
    }
}