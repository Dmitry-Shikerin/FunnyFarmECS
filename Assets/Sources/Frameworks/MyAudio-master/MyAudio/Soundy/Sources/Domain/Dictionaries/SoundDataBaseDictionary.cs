using System;
using Sources.Frameworks.MyAudio_master.MyAudio.MyUiFramework.Dictionaries;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Data;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Sources.Domain.Dictionaries
{
    [Serializable]
    public class SoundDataBaseDictionary : SerializedDictionary<string, SoundDataBase>
    {
    }
}