using Sources.BoundedContexts.UiSelectables.Domain;

namespace Sources.BoundedContexts.Woodsheds.Domain
{
    public class Woodshed : Selectable
    {
        public bool HasGrownUp { get; set; }
        public int PumpkinsCount { get; set; }
        public bool CanGrow { get; set; }
    }
}