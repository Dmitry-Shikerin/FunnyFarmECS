using System;
using Sources.Frameworks.MVPPassiveView.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Items.Presentation
{
    public class ItemView : View
    {
        public Vector3 StartScale { get; private set; }

        private void Awake() =>
            StartScale = transform.localScale;
    }
}