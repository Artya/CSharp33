using System.ComponentModel;

namespace Airport3.Enums
{
    public enum OperationType
    {
        [Description("Show all flights")]
        ShowAllFlights = 1,
        [Description("Show all passengers for specific flight")]
        ShowAllPassengersForSpecificFlight,
        [Description("Search by flight number")]
        SearchByFlightNumber,
        [Description("Search by flight price")]
        SearchByFlightPrice,
        [Description("Search by flight arrival city")]
        SearchByFlightArrivalCity,
        [Description("Search by flight departure city")]
        SearchByFlightDepartureCity,
        [Description("Search by passenger first name")]
        SearchByPassengerFirstName,
        [Description("Search by passenger second name")]
        SearchByPassengerSecondName,
        [Description("Search by passenger passport")]
        SearchByPassengerPassport,
        [Description("Add new flight")]
        AddFlight,
        [Description("Edit existing flight")]
        EditFlight,
        [Description("Delete existing flight")]
        DeleteFlight,
        [Description("Add new passenger")]
        AddPassenger,
        [Description("Delete existing passenger")]
        DeletePassenger,
        [Description("Exit program")]
        Exit,
    }
}
