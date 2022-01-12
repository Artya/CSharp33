using System;

namespace TestEventsAndDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            var home = new Home(4);

            var dog = new Inhabitant(InhabitantType.Dog, "Bobik");
            home.AddInhabitant(dog);

            var cat = new Inhabitant(InhabitantType.Cat, "Murzik");
            home.AddInhabitant(cat);

            var father = new Inhabitant(InhabitantType.Human, "Father");
            home.AddInhabitant(father);

            var mother = new Inhabitant(InhabitantType.Human, "Mother");
            home.AddInhabitant(mother);

            home.ReMoveInhabitant(mother);
            home.ReMoveInhabitant(father);

            home.AddInhabitant(mother);
            home.AddInhabitant(father);
        }
    }
}
