using System;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces
{
    public interface IView<TRoot> : IDisposable
        where TRoot : VisualElement
    {
        TRoot Root { get; }
    }
}