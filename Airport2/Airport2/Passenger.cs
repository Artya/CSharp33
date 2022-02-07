using System;
using Airport2.Enums;

namespace Airport2
{
    public class Passenger
    {
        public readonly Guid Id;

        public Guid FlightNumber { get => this.Flight == null ? Guid.Empty : this.Flight.Number; }

        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public string Nationality { get; private set; }
        public DateTime Birthday { get; private set; }
        public Gender Gender { get; private set; }
        public Flight Flight { get; private set; }
        public string Passport { get; private set; }
        public TicketType? TicketType { get; private set; }

        public Passenger
        (
            string firstName,
            string secondName,
            string nationality,
            string passport,
            DateTime birthday,
            Gender gender
        )
        {
            if (birthday.CompareTo(DateTime.Now) >= 0)
                throw new InvalidOperationException("Birthday can't be this day or in future.");

            this.Id = Guid.NewGuid();
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.Nationality = nationality;
            this.Passport = passport;
            this.Birthday = birthday;
            this.Gender = gender;
        }

        public void BuyTicket(Flight flight, TicketType ticketType)
        {
            this.Flight = flight;
            this.TicketType = ticketType;
        }

        public void ChangePassport(string passport)
        {
            this.Passport = passport;
        }
    }
}
