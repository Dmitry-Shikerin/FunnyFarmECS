using System;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Controllers.Interfaces;
using Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Interfaces;
using UnityEngine.UIElements;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.View.Implementation.Base
{
    public abstract class EditorPresentableView<TPresenter, TRoot> : IView<TRoot>
        where TPresenter : IPresenter
        where TRoot : VisualElement
    {
        protected TPresenter Presenter { get; private set; }
        public TRoot Root { get; private set; }

        public void Construct(TPresenter presenter, TRoot root)
        {
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            Root = root ?? throw new ArgumentNullException(nameof(root));
            Initialize();
            Presenter.Initialize();
        }

        public virtual void Dispose()
        {
            DisposeView();
            Presenter.Dispose();
        }

        protected abstract void Initialize();
        protected abstract void DisposeView();
    }
}