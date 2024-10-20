using Sources.BoundedContexts.UiSelectables.Domain;

namespace Sources.BoundedContexts.TomatoPatchs.Domain
{
    public class TomatoPatch : Selectable
    {
        public bool HasGrownUp { get; set; }
        public int PumpkinsCount { get; set; }
        public bool CanGrow { get; set; }
    }
}