using System;
using Airport2.Enums;

namespace Airport2
{
    public class Flight
    {
        public Guid Number { get; private set; }
        public Airline Airline { get; private set; }
        public uint Price { get; private set; }
        public City ArrivalCity { get; private set; }
        public City DepartureCity { get; private set; }
        public Terminal Terminal { get; private set; }
        public Passenger[] Passengers { get; private set; }
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

            this.Number = Guid.NewGuid();
            this.Airline = airline;
            this.Price = price;
            this.ArrivalCity = arrivalCity;
            this.DepartureCity = departureCity;
            this.Terminal = terminal;
            this.ArrivalTime = arrivalTime;
            this.DepartureTime = departureTime;
            this.status = FlightStatus.Normal;
            this.Passengers = new Passenger[0];
        }

        public void ChangeInfo
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

            this.Airline = airline;
            this.Price = price;
            this.ArrivalCity = arrivalCity;
            this.DepartureCity = departureCity;
            this.Terminal = terminal;
            this.ArrivalTime = arrivalTime;
            this.DepartureTime = departureTime;
            this.status = FlightStatus.Normal;
            this.Passengers = new Passenger[0];
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
            this.Passengers = Helper.ResizeArray(this.Passengers, this.Passengers.Length + 1);
            this.Passengers[this.Passengers.Length - 1] = passenger;
        }
        public void RemovePassenger(Passenger passenger)
        {
            var found = false;
            var index = default(int);

            for (; index < this.Passengers.Length; index++)
            {
                if (this.Passengers[index] == passenger)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                return;

            var newPassengersArray = new Passenger[this.Passengers.Length - 1];

            Array.Copy(this.Passengers, newPassengersArray, index);
            Array.Copy(this.Passengers, index + 1, newPassengersArray, index, this.Passengers.Length - index - 1);

            this.Passengers = newPassengersArray;
        }
    }
}
