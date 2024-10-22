using System;
using Sirenix.OdinInspector;
using Sources.Frameworks.MyLocalization.Domain.Data;
using Sources.Frameworks.Utils.Dictionaries;

namespace Sources.Frameworks.MyLocalization.Domain.Dictionaries
{
    [Serializable] [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
    public class PhraseSerializedDictionary : SerializedDictionary<LocalizationId, LocalizationPhraseClass>
    {
    }
}