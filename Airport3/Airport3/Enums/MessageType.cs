namespace Airport3
{
    public enum MessageType
    {
        Start,
        Succeed,
        Exit,

        QueryAllFlights,
        QueryAllPassengers,

        QueryFlightByFlightNumber,
        QueryFlightsByFlightPrice,
        QueryFlightsByFlightArrivalCity,
        QueryFlightsByFlightDepartureCity,

        QueryPassengersByFirtstName,
        QueryPassengersBySecondName,
        QueryPassengerByPassport,

        AddFlight,
        EditFlight,
        DeleteFlight,

        AddPassenger,
        DeletePassenger,

        ShowFLight,
        ShowPassenger,
    }
}
