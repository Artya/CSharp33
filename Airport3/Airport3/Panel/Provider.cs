using System;
using System.Collections.Generic;
using System.Linq;
using Airport3.Interfaces;

namespace Airport3.Panel
{
    public class Provider : IProvider, IObservable<Message>, IDisposable
    {
        private IConsumer consumer;

        public List<Flight> Flights { get; private set; }

        public Provider()
        {
            this.Flights = new List<Flight>();
        }

        public IDisposable Subscribe(IObserver<Message> consumer)
        {
            if (consumer == null)
                throw new ArgumentNullException("consumer can't be null.");

            if (this.consumer != null)
                throw new InvalidOperationException("You already have consumer.");

            if (!(consumer is IConsumer))
                throw new ArgumentException("consumer must implement not only IObserver, but IConsumer as well.");
    
            this.consumer = consumer as Consumer;

            return this;
        }

        public void SendMessage(Message message)
        {
            consumer.OnNext(message);
        }

        public void SendError(Exception exception)
        {
            consumer.OnError(exception);
        }

        public void EndTransmition()
        {
            consumer.OnCompleted();
        }

        public void Recieve(Message message)
        {
            switch (message.MessageType)
            {
                case MessageType.QueryAllFlights:
                    this.onQueryAllFlights();
                    break;
                case MessageType.QueryAllPassengers:
                    this.onQueryAllPassengers(message.Text);
                    break;
                case MessageType.QueryFlightByFlightNumber:
                    this.onQueryFlightByFlightNumber(message.Text);
                    break;
                case MessageType.QueryFlightsByFlightPrice:
                    this.onQueryFlightsByFlightPrice(message.Text);
                    break;
                case MessageType.QueryFlightsByFlightArrivalCity:
                    this.onQueryFlightsByFlightArrivalCity(message.Text);
                    break;
                case MessageType.QueryFlightsByFlightDepartureCity:
                    this.onQueryFlightsByFlightDepartureCity(message.Text);
                    break;
                case MessageType.QueryPassengersByFirtstName:
                    this.onQueryPassengersByFirtstName(message.Text, message.AdditionalText);
                    break;
                case MessageType.QueryPassengersBySecondName:
                    this.onQueryPassengersBySecondName(message.Text, message.AdditionalText);
                    break;
                case MessageType.QueryPassengerByPassport:
                    this.onQueryPassengerByPassport(message.Text, message.AdditionalText);
                    break;
                case MessageType.AddFlight:
                    this.onAddFlight(message.Flight);
                    break;
                case MessageType.EditFlight:
                    this.onEditFlight();
                    break;
                case MessageType.DeleteFlight:
                    this.onDeleteFlight(message.Text);
                    break;
                case MessageType.AddPassenger:
                    this.onAddPassenger(message.Passenger);
                    break;
                case MessageType.DeletePassenger:
                    this.onDeletePassenger(message.Passenger);
                    break;
                default:
                    this.SendError(new Exception("Not implemented."));
                    break;
            }
        }

        private void onQueryAllFlights()
        {
            if (this.Flights.Count == 0)
            {
                this.SendError(new Exception("There are no flights."));
                return;
            }

            this.sendOperationSucceed();
            foreach (var flight in this.Flights)
                this.sendFlight(flight);
        }
        private void onQueryAllPassengers(string flightNumber)
        {
            var flight = this.Flights
                .SingleOrDefault(flight => flight.Number.ToString() == flightNumber);

            if (flight == null)
            {
                this.SendError(new Exception($"Flight with number: {flightNumber} not found."));
                return;
            }

            var passengers = flight.Passengers;

            if (passengers.Count == 0)
            {
                this.SendError(new Exception($"There are no passengers for flight: {flightNumber}"));
                return;
            }

            this.sendOperationSucceed();
            foreach (var passenger in flight.Passengers)
                this.sendPassenger(passenger);
        }
        private void onQueryFlightByFlightNumber(string flightNumber)
        {
            if (this.Flights.Count == 0)
            {
                this.SendError(new Exception("There are no flights."));
                return;
            }

            var flight = this.Flights
                .SingleOrDefault(flight => flight.Number.ToString() == flightNumber);

            if (flight == null)
            {
                this.SendError(new Exception($"Flight with number: {flightNumber} not found."));
                return;
            }

            this.sendOperationSucceed();
            this.sendFlight(flight);
        }
        private void onQueryFlightsByFlightPrice(string flightPrice)
        {
            var flights = this.Flights
                .Where(flight => flight.Price.ToString() == flightPrice)
                .ToList();

            if (flights.Count == 0)
            {
                this.SendError(new Exception($"There are no flights with price: {flightPrice}"));
                return;
            }

            this.sendOperationSucceed();
            foreach (var flight in flights)
                this.sendFlight(flight);
        }
        private void onQueryFlightsByFlightArrivalCity(string arrivalCity)
        {
            var flights = this.Flights
                .Where(flight => flight.ArrivalCity.ToString() == arrivalCity)
                .ToList();

            if (flights.Count == 0)
            {
                this.SendError(new Exception($"There are no flights with arrival city: {arrivalCity}"));
                return;
            }

            this.sendOperationSucceed();
            foreach (var flight in flights)
                this.sendFlight(flight);
        }
        private void onQueryFlightsByFlightDepartureCity(string departureCity)
        {
            var flights = this.Flights
                .Where(flight => flight.DepartureCity.ToString() == departureCity)
                .ToList();

            if (flights.Count == 0)
            {
                this.SendError(new Exception($"There are no flights with departure city: {departureCity}"));
                return;
            }

            this.sendOperationSucceed();
            foreach (var flight in flights)
                this.sendFlight(flight);
        }
        private void onQueryPassengersByFirtstName(string flightNumber, string firstName)
        {
            var flight = this.Flights
                .SingleOrDefault(flight => flight.Number.ToString() == flightNumber);

            if (flight == null)
            {
                this.SendError(new Exception($"There is no flight with number: {flightNumber}"));
                return;
            }

            var passengers = flight.Passengers
                .Where(passenger => passenger.FirstName == firstName)
                .ToList();

            if (passengers.Count == 0)
            {
                this.SendError(new Exception($"There are no passengers with first name: {firstName}"));
                return;
            }

            this.sendOperationSucceed();
            foreach (var passenger in passengers)
                this.sendPassenger(passenger);
        }
        private void onQueryPassengersBySecondName(string flightNumber, string secondName)
        {
            var flight = this.Flights
                .SingleOrDefault(flight => flight.Number.ToString() == flightNumber);

            if (flight == null)
            {
                this.SendError(new Exception($"There is no flight with number: {flightNumber}"));
                return;
            }

            var passengers = flight.Passengers
                .Where(passenger => passenger.SecondName == secondName)
                .ToList();

            if (passengers.Count == 0)
            {
                this.SendError(new Exception($"There are no passengers with second name: {secondName}"));
                return;
            }

            this.sendOperationSucceed();
            foreach (var passenger in passengers)
                this.sendPassenger(passenger);
        }
        private void onQueryPassengerByPassport(string flightNumber, string passport)
        {
            var flight = this.Flights
                .SingleOrDefault(flight => flight.Number.ToString() == flightNumber);

            if (flight == null)
            {
                this.SendError(new Exception($"There is no flight with number: {flightNumber}"));
                return;
            }

            var passenger = flight.Passengers
                .FirstOrDefault(passenger => passenger.Passport == passport);

            if (passenger == null)
            {
                this.SendError(new Exception($"There is no passenger with passport: {passport}"));
                return;
            }

            this.sendOperationSucceed();
            this.sendPassenger(passenger);
        }
        private void onAddFlight(Flight flight)
        {
            this.Flights.Add(flight);
            this.sendOperationSucceed();
        }
        private void onEditFlight()
        {
            this.sendOperationSucceed();
        }
        private void onDeleteFlight(string flightNumber)
        {
            var flight = this.Flights
                .SingleOrDefault(flight => flight.Number.ToString() == flightNumber);

            if (flight == null)
            {
                this.SendError(new Exception($"There is no flight with number: {flightNumber}"));
                return;
            }

            this.sendOperationSucceed();
            this.Flights.Remove(flight);
        }
        private void onAddPassenger(Passenger passenger)
        {
            var flight = this.Flights
                .Single(flight => flight.Number == passenger.FlightNumber);

            this.sendOperationSucceed();
            flight.AddPassenger(passenger);
        }
        private void onDeletePassenger(Passenger passenger)
        {
            var flight = this.Flights
                .Single(flight => flight.Number == passenger.FlightNumber);

            this.sendOperationSucceed();
            flight.RemovePassenger(passenger);
        }

        private void sendOperationSucceed()
        {
            var message = new Message(MessageType.Succeed);
            this.SendMessage(message);
        }
        private void sendFlight(Flight flight)
        {
            var message = new Message(MessageType.ShowFLight, flight);
            this.SendMessage(message);
        }
        private void sendPassenger(Passenger passenger)
        {
            var message = new Message(MessageType.ShowPassenger, passenger);
            this.SendMessage(message);
        }

        public void Dispose() { }
    }
}
