namespace Airport3.Interfaces
{
    public interface IPanel
    {
        Airline Airline { get; }
        void Start();
        void AddFlight(Flight flight);
    }
}
