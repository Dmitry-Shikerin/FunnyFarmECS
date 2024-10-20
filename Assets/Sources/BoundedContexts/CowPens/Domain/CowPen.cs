using Sources.BoundedContexts.UiSelectables.Domain;

namespace Sources.BoundedContexts.CowPens.Domain
{
    public class CowPen : Selectable
    {
        public bool HasGrownUp { get; set; }
        public int PumpkinsCount { get; set; }
        public bool CanGrow { get; set; }
    }
}