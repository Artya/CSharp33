using System;

namespace AirportPanel2
{
    public class PassengerContainer : ITableContainer
    {
        private readonly int bordersCount = 7;
        public int FirstNameWidth { get; set; } = 9;
        public int LastNameWidth { get; set; } = 9;
        public int PassprortWidth { get; set; } = 8;
        public int DateOfBirthWidh { get; set; } = 10;
        public int GenderWidth { get => 8; }
        public int NationalityWidth { get; set; } = 11;
        public Passenger[] PassengersArray { get; set; }
        public int Length { get { return PassengersArray == null ? 0 : PassengersArray.Length; } }
        public int TableWidth { get; private set; }
        public Airline Airline { get; }
        public string TableName => "Passengers";

        public PassengerContainer(Airline airline)
        {
            this.Airline = airline;
        }

        public void PrintTableRow(int rowIndex, ConsoleColor borderColor)
        {
            var passenger = PassengersArray[rowIndex];

            TablePrinter.WriteCell(passenger.FirstName, this.FirstNameWidth, borderColor, CellTextLevelling.Left);
            TablePrinter.WriteCell(passenger.LastName, this.LastNameWidth, borderColor, CellTextLevelling.Left);
            TablePrinter.WriteCell(passenger.Nationality.ToString(), this.NationalityWidth, borderColor, CellTextLevelling.Left);
            TablePrinter.WriteCell(passenger.Passport, this.PassprortWidth, borderColor, CellTextLevelling.Left);
            TablePrinter.WriteCell(passenger.DateOfBirth.ToString(Constants.DateOfBirthFormatString), this.DateOfBirthWidh, borderColor);
            TablePrinter.WriteCell(passenger.Gender.ToString(), this.GenderWidth, borderColor, CellTextLevelling.Left);
            Console.WriteLine();
        }

        public void PrintTableTitle(ConsoleColor borderColor)
        {
            TablePrinter.WriteCell("Name", this.FirstNameWidth, borderColor);
            TablePrinter.WriteCell("Last name", this.LastNameWidth, borderColor);
            TablePrinter.WriteCell("Nationality", this.NationalityWidth, borderColor);
            TablePrinter.WriteCell("Passport", this.PassprortWidth, borderColor);
            TablePrinter.WriteCell("Date of Birth", this.DateOfBirthWidh, borderColor);
            TablePrinter.WriteCell("Gender", this.GenderWidth, borderColor);
            Console.WriteLine();
        }

        public void AddPassenger(Passenger passenger)
        {
            this.PassengersArray = (Passenger[])ServiceHelper.AddElementToArray(this.PassengersArray, typeof(Passenger), passenger);

            FirstNameWidth = Math.Max(FirstNameWidth, passenger.FirstName.Length);
            LastNameWidth = Math.Max(LastNameWidth, passenger.LastName.Length);
            PassprortWidth = Math.Max(PassprortWidth, passenger.Passport.Length);
            DateOfBirthWidh = Math.Max(DateOfBirthWidh, passenger.DateOfBirth.ToString().Length);
            NationalityWidth = Math.Max(NationalityWidth, passenger.Nationality.ToString().Length);

            RecalculateTotalTableWidth();
        }

        public void WorkWithMenu()
        {
            while (true)
            {
                var operation = (PassengerMenu)UserInteraction.ChooseEnumValue("Passenger menu", typeof(PassengerMenu));

                switch (operation)
                {
                    case PassengerMenu.ReturnToMainMenu:
                        return;

                    case PassengerMenu.ShowPassengers:
                        TablePrinter.PrintTable(this, ConsoleColor.Blue);
                        break;

                    case PassengerMenu.AddNewPassenger:

                        var newPassenger = Passenger.GetPassengerFromUser();
                        this.AddPassenger(newPassenger);

                        break;

                    case PassengerMenu.DeletePassenger:

                        var passengerToDelete = UserInteraction.ChooseElemementFromArray(this.PassengersArray, "Choose passenger to delete");
                        this.PassengersArray = (Passenger[])ServiceHelper.DeleteElementFromArray(this.PassengersArray, passengerToDelete, typeof(Passenger));

                        break;

                    case PassengerMenu.EditPassenger:

                        var passengerToEdit = (Passenger)UserInteraction.ChooseElemementFromArray(this.PassengersArray, "Choose passenger to edit");
                        Passenger.EditPassenger(passengerToEdit);
                        break;

                    default:
                        Console.WriteLine($"Operation {operation} non implemented");
                        break;
                }
            }
        }

        public void RecalculateTotalTableWidth()
        {
            var sum = bordersCount * Constants.BorderWidth;

            sum += FirstNameWidth;
            sum += LastNameWidth;
            sum += NationalityWidth;
            sum += PassprortWidth;
            sum += DateOfBirthWidh;
            sum += GenderWidth;

            TableWidth = sum;
        }
    }
}
