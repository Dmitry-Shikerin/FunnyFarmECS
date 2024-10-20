using Sources.BoundedContexts.UiSelectables.Domain;

namespace Sources.BoundedContexts.PigPens.Domain
{
    public class PigPen : Selectable
    {
        public bool HasGrownUp { get; set; }
        public int PumpkinsCount { get; set; }
        public bool CanGrow { get; set; }
    }
}