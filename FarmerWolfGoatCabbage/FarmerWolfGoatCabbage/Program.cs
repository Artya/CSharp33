using System;

namespace FarmerWolfGoatCabbage
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var farmerOnLeftBank = true;
            var wolfOnLeftBank = true;
            var goatOnLeftBank = true;
            var cabbagOnLeftBank = true;

            ShowBanksState(farmerOnLeftBank, wolfOnLeftBank, goatOnLeftBank, cabbagOnLeftBank);

            while (true)
            {
                Console.WriteLine("Choose your next step:");
                Console.WriteLine("1. There: farmer and wolf");
                Console.WriteLine("2. There: farmer and cabbage");
                Console.WriteLine("3. There: farmer and goat");
                Console.WriteLine("4. There: farmer");
                Console.WriteLine("5. Back: farmer and wolf");
                Console.WriteLine("6. Back: farmer and cabbage");
                Console.WriteLine("7. Back: farmer and goat");
                Console.WriteLine("8. Back: farmer");
                Console.WriteLine("Other number to exit....");

                var strChoiсe = Console.ReadLine();
                var intChoiсe = int.Parse(strChoiсe);

                switch (intChoiсe)
                {
                    case 1:
                        {
                            if (!CheckCorrectMovePersonageToBank(Personages.Farmer, false, farmerOnLeftBank))
                                break;

                            if (!CheckCorrectMovePersonageToBank(Personages.Wolf, false, wolfOnLeftBank))
                                break;

                            farmerOnLeftBank = false;
                            wolfOnLeftBank = false;
                            break;
                        }
                    case 2:
                        {
                            if (!CheckCorrectMovePersonageToBank(Personages.Farmer, false, farmerOnLeftBank))
                                break;

                            if (!CheckCorrectMovePersonageToBank(Personages.Cabbage, false, cabbagOnLeftBank))
                                break;

                            farmerOnLeftBank = false;
                            cabbagOnLeftBank = false;
                            break;
                        }
                    case 3:
                        {
                            if (!CheckCorrectMovePersonageToBank(Personages.Farmer, false, farmerOnLeftBank))
                                break;

                            if (!CheckCorrectMovePersonageToBank(Personages.Goat, false, goatOnLeftBank))
                                break;

                            farmerOnLeftBank = false;
                            goatOnLeftBank = false;
                            break;
                        }
                    case 4:
                        {
                            if (!CheckCorrectMovePersonageToBank(Personages.Farmer, false, farmerOnLeftBank))
                                break;

                            farmerOnLeftBank = false;
                            break;
                        }
                    case 5:
                        {
                            if (!CheckCorrectMovePersonageToBank(Personages.Farmer, true, farmerOnLeftBank))
                                break;

                            if (!CheckCorrectMovePersonageToBank(Personages.Wolf, true, wolfOnLeftBank))
                                break;

                            farmerOnLeftBank = true;
                            wolfOnLeftBank = true;
                            break;
                        }
                    case 6:
                        {
                            if (!CheckCorrectMovePersonageToBank(Personages.Farmer, true, farmerOnLeftBank))
                                break;

                            if (!CheckCorrectMovePersonageToBank(Personages.Cabbage, true, cabbagOnLeftBank))
                                break;

                            farmerOnLeftBank = true;
                            cabbagOnLeftBank = true;
                            break;
                        }
                    case 7:
                        {
                            if (!CheckCorrectMovePersonageToBank(Personages.Farmer, true, farmerOnLeftBank))
                                break;

                            if (!CheckCorrectMovePersonageToBank(Personages.Goat, true, goatOnLeftBank))
                                break;

                            farmerOnLeftBank = true;
                            goatOnLeftBank = true;
                            break;
                        }
                    case 8:
                        {
                            if (!CheckCorrectMovePersonageToBank(Personages.Farmer, true, farmerOnLeftBank))
                                break;

                            farmerOnLeftBank = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Entered step is incorect, game over");
                            return;
                        }
                }

                ShowBanksState(farmerOnLeftBank, wolfOnLeftBank, goatOnLeftBank, cabbagOnLeftBank);

                if (!farmerOnLeftBank && !wolfOnLeftBank && !goatOnLeftBank && !cabbagOnLeftBank)
                {
                    Console.WriteLine("Congrtatulation!!!! Task complete succesful!!!!");
                    return;
                }
                else if (farmerOnLeftBank != wolfOnLeftBank && wolfOnLeftBank == goatOnLeftBank)
                {
                    ShowAteGameOver(Personages.Wolf, Personages.Goat);
                    return;
                }
                else if (farmerOnLeftBank != goatOnLeftBank && goatOnLeftBank == cabbagOnLeftBank)
                {
                    ShowAteGameOver(Personages.Goat, Personages.Cabbage);
                    return;
                }
            }
        }

        private static void ShowAteGameOver(Personages predator, Personages victim)
        {
            Console.WriteLine("{0} ate {1}, Game over", predator, victim);
        }

        static void ShowBanksState(bool farmerState, bool wolfState, bool goatState, bool cabbageState)
        {
            var leftBank = "";
            var rightBank = "";

            if (farmerState)
            {
                leftBank += GetSeparator(leftBank) + Personages.Farmer;
            }
            else
                rightBank += GetSeparator(rightBank) + Personages.Farmer;

            if (wolfState)
            {
                leftBank += GetSeparator(leftBank) + Personages.Wolf;
            }
            else
                rightBank += GetSeparator(rightBank) + Personages.Wolf;

            if (goatState)
            {
                leftBank += GetSeparator(leftBank) + Personages.Goat;
            }
            else
                rightBank += GetSeparator(rightBank) + Personages.Goat;

            if (cabbageState)
            {
                leftBank += GetSeparator(leftBank) + Personages.Cabbage;
            }
            else
                rightBank += GetSeparator(rightBank) + Personages.Cabbage;

            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("On left bank: " + (leftBank == "" ? "Nobody" : leftBank));
            Console.WriteLine("On right bank: " + (rightBank == "" ? "Nobody" : rightBank));
        }

        static string GetSeparator(string bankInfo)
        {
            return bankInfo == "" ? "" : ", ";
        }

        static bool CheckCorrectMovePersonageToBank(Personages personage, bool leftBank, bool personageOnLeftBank)
        {
            if (personageOnLeftBank == leftBank)
            {
                Console.WriteLine("" + personage + " is already on " + (leftBank ? "Left" : "Right") + " bank, this step is impossible");
                return false;
            }

            return true;
        }
    }
}
