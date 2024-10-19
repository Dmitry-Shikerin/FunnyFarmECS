using Sources.BoundedContexts.UiSelectables.Domain;

namespace Sources.BoundedContexts.OnionPatches.Domain
{
    public class OnionPatch : Selectable
    {
        public bool HasGrownUp { get; set; }
        public int PumpkinsCount { get; set; }
        public bool CanGrow { get; set; }
    }
}