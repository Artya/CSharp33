using System.ComponentModel;

namespace Airport2.Enums
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
