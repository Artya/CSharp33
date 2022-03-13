using System;

namespace AirportPanel2
{
    public class Passenger
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Passport { get; private set; }
        public Nationality Nationality { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Gender Gender { get; private set; }
        public Passenger(string firstName, string lastName, Nationality nationality, string passport, DateTime dateOfBirth, Gender gender)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Passport = passport;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Nationality = nationality;
        }

        public static Passenger GetPassengerFromUser()
        {
            var name = UserInteraction.GetString("Enter passenger name");
            var lastName = UserInteraction.GetString("Enter passenger Last Name");
            var nationality = (Nationality)UserInteraction.ChooseEnumValue("Choose nationality:", typeof(Nationality));
            var dateOfBirth = UserInteraction.GetDate($"Enter date of birth in format {Constants.DateOfBirthFormatString}");
            var passprot = UserInteraction.GetString("Enter passport");
            var gender = UserInteraction.ChooseEnumValue("Enter gender", typeof(Gender));

            return new Passenger(name, lastName, nationality, passprot, dateOfBirth, (Gender)gender);
        }

        public override string ToString()
        {
            var genderView = this.Gender.ToString().Length == 0 ? ' ' : this.Gender.ToString()[0];
            var dateOfBirthView = this.DateOfBirth.ToString(Constants.DateOfBirthFormatString);

            return $"{this.FirstName} {this.LastName} ({genderView}) {this.Nationality} {dateOfBirthView} {this.Passport}";
        }

        public static void EditPassenger(Passenger passenger)
        {
            if (passenger == null)
                return;

            while (true)
            {
                var fieldToEdit = (PassengerEditedFields)UserInteraction.ChooseEnumValue("Choose field to edit", typeof(PassengerEditedFields));

                switch (fieldToEdit)
                {
                    case PassengerEditedFields.FinishEditing:
                        return;

                    case PassengerEditedFields.FirstName:

                        passenger.FirstName = UserInteraction.GetString($"Current name {passenger.FirstName}, enter a new value:");
                        break;

                    case PassengerEditedFields.LastName:
                        passenger.LastName = UserInteraction.GetString($"Current Last name {passenger.LastName}, enter a new value:");
                        break;

                    case PassengerEditedFields.Passport:
                        passenger.Passport = UserInteraction.GetString($"Current passport {passenger.Passport}, enter a new value:");
                        break;

                    case PassengerEditedFields.Nationality:
                        passenger.Nationality = (Nationality)UserInteraction.ChooseEnumValue($"Current Nationality {passenger.Nationality}, enter a new value:", typeof(Nationality));
                        break;

                    case PassengerEditedFields.DateOfBirth:
                        passenger.DateOfBirth = UserInteraction.GetDate($"Current Date of Birth {passenger.DateOfBirth}, enter a new value:");
                        break;

                    case PassengerEditedFields.Gender:
                        passenger.Gender = (Gender)UserInteraction.ChooseEnumValue($"Current gender {passenger.Gender}, enter a new value:", typeof(Gender));
                        break;
                }
            }
        }
    }
}
