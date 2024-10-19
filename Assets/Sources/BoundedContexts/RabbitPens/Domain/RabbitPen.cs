using Sources.BoundedContexts.UiSelectables.Domain;

namespace Sources.BoundedContexts.RabbitPens.Domain
{
    public class RabbitPen : Selectable
    {
        public bool HasGrownUp { get; set; }
        public int PumpkinsCount { get; set; }
        public bool CanGrow { get; set; }
    }
}