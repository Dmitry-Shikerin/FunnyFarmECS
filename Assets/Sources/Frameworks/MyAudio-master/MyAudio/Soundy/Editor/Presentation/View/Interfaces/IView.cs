using System;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces
{
    public interface IView : IDisposable
    {
        VisualElement Root { get; }
        
        void Initialize();
        void CreateView();
    }
}