using System.ComponentModel;

partial class Program
{
    public enum FlightStatusEnum
    {
        [Description("Check-in")]
        CheckIn,
        [Description("Gate Closed")]
        GateClosed,
        [Description("Arrived")]
        Arrived,
        [Description("Departed at")]
        DepartedAt,
        [Description("Unknown")]
        Unknown,
        [Description("Cenceled")]
        Canceled,
        [Description("Expected at")]
        ExpectedAt,
        [Description("Delayed")]
        Delayed,
        [Description("In flight")]
        InFlight
    }
}
