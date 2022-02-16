using System.ComponentModel;

namespace Airport3.Enums
{
    public enum FlightStatus
    {
        [Description("Normal")]
        Normal,
        [Description("Canceled")]
        Canceled,
        [Description("Delayed")]
        Delayed,
        [Description("In flight")]
        InFlight,
        [Description("Check-in")]
        CheckIn,
        [Description("Gate open")]
        GateOpen,
        [Description("Gate closed")]
        GateClosed
    }
}
