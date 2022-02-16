using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Airport3.Enums;
using Airport3.Interfaces;

namespace Airport3
{
    public static class Helper
    {
        public static IEnumerable<string> GetEnumValues<T>()
        {
            var list = new List<string>();

            foreach (var item in Enum.GetValues(typeof(T)))
                list.Add(((T)item).ToString());

            return list;
        }

        public static string GetEnumDescription(Enum enumValue, bool hasCustomAttributes)
        {
            if (!hasCustomAttributes)
                return enumValue.ToString();

            return ((DescriptionAttribute)enumValue
                        .GetType()
                        .GetField(enumValue.ToString())
                        .GetCustomAttributes(typeof(DescriptionAttribute), true)[0])
                        .Description;
        }

        public static T ParseDescriptionToEnum<T>(string description, bool hasCustomAttributes)
        {
            var array = Enum.GetValues(typeof(T));
            var list = new List<T>(array.Length);

            for (int i = 0; i < array.Length; i++)
            {
                list.Add((T)array.GetValue(i));
            }

            var dict = list.Select
            (
                v =>
                new
                {
                    Value = v,
                    Description = GetEnumDescription(v as Enum, hasCustomAttributes)
                }
             ).ToDictionary(x => x.Description, x => x.Value);

            return dict[description];
        }

        public static void PrepareFlights(IPanel panel)
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
