using System;
using System.Collections.Generic;
using Airport3.Enums;

namespace Airport3
{
    public class Flight
    {
        public readonly Guid Number;
        public readonly Airline Airline;

        public uint Price { get; private set; }
        public City ArrivalCity { get; private set; }
        public City DepartureCity { get; private set; }
        public Terminal Terminal { get; private set; }
        public List<Passenger> Passengers { get; private set; }

        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }

        private FlightStatus status;
        public FlightStatus Status
        {
            get
            {
                switch (this.status)
                {
                    case FlightStatus.Normal:
                        break;
                    case FlightStatus.Canceled:
                        return FlightStatus.Canceled;
                    case FlightStatus.Delayed:
                        return FlightStatus.Delayed;
                }

                var time = (DateTime.Now - this.DepartureTime).TotalMinutes;
                var flightTime = (this.ArrivalTime - this.DepartureTime).TotalMinutes;

                if (time >= -30 && time < 0)
                    return FlightStatus.GateClosed;

                if (time >= -60 && time < 0)
                    return FlightStatus.GateOpen;

                if (time >= -120 && time < 0)
                    return FlightStatus.CheckIn;

                if (time > 0 && time < flightTime)
                    return FlightStatus.InFlight;

                return FlightStatus.Normal;
            }
        }

        public Flight
        (
            Airline airline,
            uint price,
            City arrivalCity,
            City departureCity,
            Terminal terminal,
            DateTime arrivalTime,
            DateTime departureTime
        )
        {
            if (arrivalCity == departureCity)
                throw new InvalidOperationException("Departure and arrival city can't be the same city.");

            if (arrivalTime == departureTime)
                throw new ArgumentException("Arrival and departure time can't be the same.");

            this.Number = Guid.NewGuid();
            this.Airline = airline;
            this.Price = price;
            this.ArrivalCity = arrivalCity;
            this.DepartureCity = departureCity;
            this.Terminal = terminal;
            this.ArrivalTime = arrivalTime;
            this.DepartureTime = departureTime;
            this.status = FlightStatus.Normal;
            this.Passengers = new List<Passenger>();
        }

        public void ChangeInfo
        (
            uint price,
            City arrivalCity,
            City departureCity,
            Terminal terminal,
            DateTime arrivalTime,
            DateTime departureTime
        )
        {
            if (arrivalCity == departureCity)
                throw new InvalidOperationException("Departure and arrival city can't be the same city.");

            if (arrivalTime == departureTime)
                throw new ArgumentException("Arrival and departure time can't be the same.");

            this.Price = price;
            this.ArrivalCity = arrivalCity;
            this.DepartureCity = departureCity;
            this.Terminal = terminal;
            this.ArrivalTime = arrivalTime;
            this.DepartureTime = departureTime;
            this.status = FlightStatus.Normal;
        }

        public void SetStatusDelayed()
        {
            this.status = FlightStatus.Delayed;
        }
        public void SetStatusCancled()
        {
            this.status = FlightStatus.Canceled;
        }
        public void SetStatusNormal()
        {
            this.status = FlightStatus.Normal;
        }
        public void AddPassenger(Passenger passenger)
        {
            this.Passengers.Add(passenger);
        }
        public void RemovePassenger(Passenger passenger)
        {
            this.Passengers.Remove(passenger);
        }
    }
}
