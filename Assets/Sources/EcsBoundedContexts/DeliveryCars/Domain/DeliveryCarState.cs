namespace Sources.EcsBoundedContexts.DeliveryCars.Domain
{
    public enum DeliveryCarState
    {
        Default = 0,
        HomeIdle = 1,
        MoveToHome = 2,
        ExitIdle = 3,
        MoveToExit = 4,
    }
}