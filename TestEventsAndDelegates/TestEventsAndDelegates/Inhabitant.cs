using System;

namespace TestEventsAndDelegates
{
    public class Inhabitant
    {
        public InhabitantType TypeOfInhabitant { get; private set; }
        public string Name { get; private set; }
        public string Message { get; private set; }

        public Inhabitant(InhabitantType type, string name)
        {
            this.TypeOfInhabitant = type;
            this.Name = name;

            if (type == InhabitantType.Dog)
            {
                Message = "Woof, woof, I want to go for a walk and play";
                return;
            }

            if (type == InhabitantType.Cat)
            {
                Message = "Meow, I want to eat, now";
                return;
            }

            if (type == InhabitantType.Human)
            {
                Message = "Hello, how are you?";
            }
        }

        public void ReactionToSomeBodyCome(Inhabitant inhabitant)
        {
            if (inhabitant == this)
            {
                Console.WriteLine($"{this.TypeOfInhabitant} {this.Name}: comes home");
                return;
            }

            Console.WriteLine($"{this.TypeOfInhabitant} {this.Name}: {this.Message}");
        }
    }
}
