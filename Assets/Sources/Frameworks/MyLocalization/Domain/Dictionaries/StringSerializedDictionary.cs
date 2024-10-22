using System;
using Sirenix.OdinInspector;
using Sources.Frameworks.Utils.Dictionaries;

namespace Sources.Frameworks.MyLocalization.Domain.Dictionaries
{
    [Serializable] [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.ExpandedFoldout)]
    public class StringSerializedDictionary : SerializedDictionary<string, string>
    {
    }
}