using System;
using JetBrains.Annotations;
using Sources.BoundedContexts.Movements.Presentation.Views.Implementation;
using Sources.Frameworks.MyGameCreator.Movements.Domain.Models;
using Sources.OldBoundedContexts.Movements.Presentation.Views.Implementation;
using Zenject;

namespace Sources.BoundedContexts.Movements.Infrastructure.Factories
{
    public class MovementViewFactory
    {
        public MovementView Create(Movement movement, MovementView view)
        {
            if (movement == null) 
                throw new ArgumentNullException(nameof(movement));
            
            if (view == null)
                throw new ArgumentNullException(nameof(view));
            
            return view;
        }
    }
}