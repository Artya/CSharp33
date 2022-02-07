using System;
using System.ComponentModel;
using Airport2.Enums;

namespace Airport2
{
    public static class Helper
    {
        public static string GetEnumDescription(Enum enumValue)
        {
            return ((DescriptionAttribute)enumValue
                    .GetType()
                    .GetField(enumValue.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), true)[0])
                    .Description;
        }
        public static Flight[] ResizeArray(Flight[] array, int newLength)
        {
            if (newLength < 0)
                throw new InvalidOperationException("New Length can't be less than zero.");

            var length = array.Length;
            var newArray = new Flight[newLength];

            if (length > newLength)
                Array.Copy(array, newArray, newLength);

            if (length < newLength)
                Array.Copy(array, newArray, length);

            return newArray;
        }
        public static Passenger[] ResizeArray(Passenger[] array, int newLength)
        {
            if (newLength < 0)
                throw new InvalidOperationException("New Length can't be less than zero.");

            var length = array.Length;
            var newArray = new Passenger[newLength];

            if (length > newLength)
                Array.Copy(array, newArray, newLength);

            if (length < newLength)
                Array.Copy(array, newArray, length);

            return newArray;
        }
        public static void PrepareFlights(Panel panel)
        {
            var random = new Random();

            var predefinedNames = new string[2][,]
            {
                new string[2, 5]
                {
                    {
                        "Daniil",
                        "Maksim",
                        "Nikita",
                        "Oleg",
                        "Denis"
                    },
                    {
                        "Ivanov",
                        "Smirnov",
                        "Petrov",
                        "Popov",
                        "Kuznecov"
                    }
                },
                new string[2, 5]
                {
                    {
                        "Anna",
                        "Alina",
                        "Olesya",
                        "Alla",
                        "Vera"
                    },
                    {
                        "Ivanova",
                        "Smirnova",
                        "Krilova",
                        "Radionova",
                        "Titova"
                    }
                }
            };

            for (var i = 0; i < 4; i++)
            {
                var arrivalCity = random.Next(1, 7);
                var departureCity = arrivalCity + 1;

                var flight = new Flight(
                        panel.Airline,
                        (uint)random.Next(400, 3000),
                        (City)arrivalCity,
                        (City)departureCity,
                        (Terminal)random.Next(1, 26),
                        DateTime.Now.AddHours(random.Next(5, 10)),
                        DateTime.Now.AddHours(random.Next(3))
                        );

                for (var j = 0; j < random.Next(1, 4); j++)
                {
                    var randomNumber = random.Next() % 2;
                    var year = random.Next(1970, 2021);
                    var month = random.Next(1, 13);
                    var day = random.Next(1, 28);
                    var passport = "nn";
                    for (var k = 0; k < 6; k++)
                        passport += random.Next(10);

                    var passenger = new Passenger(
                                            predefinedNames[randomNumber][0, random.Next(5)],
                                            predefinedNames[randomNumber][1, random.Next(5)],
                                            "Ukrainian",
                                            passport,
                                            new DateTime(year, month, day),
                                            (Gender)(randomNumber + 1)
                                        );

                    passenger.BuyTicket(flight, (TicketType)((random.Next() % 2) + 1));
                    flight.AddPassenger(passenger);
                }

                panel.AddFlight(flight);
            }
        }
    }
}
