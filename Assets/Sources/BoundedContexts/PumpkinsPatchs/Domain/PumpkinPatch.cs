using Sources.BoundedContexts.UiSelectables.Domain;

namespace Sources.BoundedContexts.PumpkinsPatchs.Domain
{
    public class PumpkinPatch : Selectable
    {
        public bool HasGrownUp { get; set; }
        public int PumpkinsCount { get; set; }
        public bool CanGrow { get; set; }
    }
}