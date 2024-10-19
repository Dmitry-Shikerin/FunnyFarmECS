using Sources.BoundedContexts.UiSelectables.Domain;

namespace Sources.BoundedContexts.ChikenCorrals.Domain
{
    public class ChickenCorral : Selectable
    {
        public bool HasGrownUp { get; set; }
        public int PumpkinsCount { get; set; }
        public bool CanGrow { get; set; }
    }
}