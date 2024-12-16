using System;
using Sources.Frameworks.Utils.Dictionaries;

namespace Sources.BoundedContexts.Paths.Domain
{
    [Serializable]
    public class PathsSerializedDictionary : SerializedDictionary<PathOwnerType, PathData>
    {
    }
}