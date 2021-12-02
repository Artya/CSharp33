using System;

namespace FarmerWolfGoatCabbage
{
    class Program
    {
        enum Personages { farmer = 0, wolf, goat, cabbage };

        static void Main(string[] args)
        {
            bool farmerOnLeftBank = true;
            bool wolfOnLeftBank = true;
            bool goatOnLeftBank = true;
            bool cabbagOnLeftBank = true;

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
                Console.WriteLine("Other to exit....");

                var strChoise = Console.ReadLine();
                var intChoise = int.Parse(strChoise);

                switch (intChoise)
                {
                    case 1:
                        {
                            if (!CheckCorrectMovePersonageToBank(Personages.farmer, false, farmerOnLeftBank))
                                break;


                            if (!CheckCorrectMovePersonageToBank(Personages.wolf, false, wolfOnLeftBank))
                                break;

                            farmerOnLeftBank = false;
                            wolfOnLeftBank = false;
                            break;
                        }
                    case 2:
                        {
                            if (!CheckCorrectMovePersonageToBank(Personages.farmer, false, farmerOnLeftBank))
                                break;


                            if (!CheckCorrectMovePersonageToBank(Personages.cabbage, false, cabbagOnLeftBank))
                                break;

                            farmerOnLeftBank = false;
                            cabbagOnLeftBank = false;
                            break;
                        }
                    case 3:
                        {
                            if (!CheckCorrectMovePersonageToBank(Personages.farmer, false, farmerOnLeftBank))
                                break;


                            if (!CheckCorrectMovePersonageToBank(Personages.goat, false, goatOnLeftBank))
                                break;

                            farmerOnLeftBank = false;
                            goatOnLeftBank = false;
                            break;
                        }
                    case 4:
                        {
                            if (!CheckCorrectMovePersonageToBank(Personages.farmer, false, farmerOnLeftBank))
                                break;

                            farmerOnLeftBank = false;
                            break;
                        }
                    case 5:
                        {
                            if (!CheckCorrectMovePersonageToBank(Personages.farmer, true, farmerOnLeftBank))
                                break;


                            if (!CheckCorrectMovePersonageToBank(Personages.wolf, true, wolfOnLeftBank))
                                break;

                            farmerOnLeftBank = true;
                            wolfOnLeftBank = true;
                            break;
                        }
                    case 6:
                        {
                            if (!CheckCorrectMovePersonageToBank(Personages.farmer, true, farmerOnLeftBank))
                                break;


                            if (!CheckCorrectMovePersonageToBank(Personages.cabbage, true, cabbagOnLeftBank))
                                break;

                            farmerOnLeftBank = true;
                            cabbagOnLeftBank = true;
                            break;
                        }
                    case 7:
                        {

                            if (!CheckCorrectMovePersonageToBank(Personages.farmer, true, farmerOnLeftBank))
                                break;


                            if (!CheckCorrectMovePersonageToBank(Personages.goat, true, goatOnLeftBank))
                                break;

                            farmerOnLeftBank = true;
                            goatOnLeftBank = true;
                            break;
                        }
                    case 8:
                        {
                            if (!CheckCorrectMovePersonageToBank(Personages.farmer, true, farmerOnLeftBank))
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
                    ShowAteGameOver(Personages.wolf, Personages.goat);
                    return;
                }
                else if (farmerOnLeftBank != goatOnLeftBank && goatOnLeftBank == cabbagOnLeftBank)
                {
                    ShowAteGameOver(Personages.goat, Personages.cabbage);
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
            string leftBank = "";
            string rightBank = "";

            if (farmerState)
            {
                leftBank += GetSeparator(leftBank) + Personages.farmer;
            }
            else
                rightBank += GetSeparator(rightBank) + Personages.farmer;

            if (wolfState)
            {
                leftBank += GetSeparator(leftBank) + Personages.wolf;
            }
            else
                rightBank += GetSeparator(rightBank) + Personages.wolf;

            if (goatState)
            {
                leftBank += GetSeparator(leftBank) + Personages.goat;
            }
            else
                rightBank += GetSeparator(rightBank) + Personages.goat;

            if (cabbageState)
            {
                leftBank += GetSeparator(leftBank) + Personages.cabbage;
            }
            else
                rightBank += GetSeparator(rightBank) + Personages.cabbage;

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
