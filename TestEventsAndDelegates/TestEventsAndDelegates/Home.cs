using System;

namespace TestEventsAndDelegates
{
    public delegate void SomeBodyCame(Inhabitant inhabitant);

    public class Home
    {
        private Inhabitant[] homeInhabitants;
        private int currentInhabitantIndex;

        public event SomeBodyCame HandleSomeBodyCome; // (Inhabitant inhabitant);
        

        public Home(int inhabitansCount)
        {
            currentInhabitantIndex = default;
            homeInhabitants = new Inhabitant[inhabitansCount];
        }

        public void AddInhabitant(Inhabitant inhabitant)
        {

            if (currentInhabitantIndex == homeInhabitants.Length)
                return;

            homeInhabitants[currentInhabitantIndex] = inhabitant;
            currentInhabitantIndex++;
            HandleSomeBodyCome += inhabitant.ReactionToSomeBodyCome;

            if (HandleSomeBodyCome != null)
            {
                Console.WriteLine("Before HandleSomeBodyCome");
                HandleSomeBodyCome(inhabitant);
                Console.WriteLine("After HandleSomeBodyCome");
                Console.WriteLine();
            }
        }

        public void ReMoveInhabitant(Inhabitant inhabitant)
        {
            var index = Array.IndexOf(homeInhabitants, inhabitant);

            if (index < 0)
            {
                return;
            }

            currentInhabitantIndex--;
            homeInhabitants[index] = null;
            HandleSomeBodyCome -= inhabitant.ReactionToSomeBodyCome;

            if (index == homeInhabitants.Length - 1)
            {
                return;
            }

            var elementsLeft = homeInhabitants.Length - index - 1;

            Array.Copy(homeInhabitants, index + 1, homeInhabitants, index, elementsLeft);

            homeInhabitants[index + elementsLeft] = null;
        }
    }
}
