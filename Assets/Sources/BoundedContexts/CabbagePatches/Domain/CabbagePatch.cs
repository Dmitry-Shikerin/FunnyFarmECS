using Sources.BoundedContexts.UiSelectables.Domain;

namespace Sources.BoundedContexts.CabbagePatches.Domain
{
    public class CabbagePatch : Selectable
    {
        public bool HasGrownUp { get; set; }
        public int PumpkinsCount { get; set; }
        public bool CanGrow { get; set; }
    }
}