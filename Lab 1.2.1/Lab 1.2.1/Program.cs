using System;

namespace Lab_1._2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            var user1 = new LibraryUser();
            const int bookLimit = 300;
            var user2 = new LibraryUser("Maria", "Ivanenko", "+380447777777", bookLimit);

            Console.WriteLine("user1 " + user1.FirstName + ", " + user1.LastName + ", anonymous: " + user1.IsAnonymous);
            Console.WriteLine("user2 " + user2.FirstName + ", " + user2.LastName + ", anonymous: " + user2.IsAnonymous);

            Console.WriteLine("user1: add Harry Potter");
            user1.AddBook("Harry Potter");
            Console.WriteLine("user2: add Sherlock Holmes");
            user2.AddBook("Sherlock Holmes");

            Console.WriteLine("user1.BooksCount = " + user1.GetBooksCount());
            Console.WriteLine("user2.BooksCount = " + user2.GetBooksCount());

            Console.WriteLine("user2: add Kobzar");
            user2.AddBook("Kobzar");
            Console.WriteLine("user2.BooksCount = " + user2.GetBooksCount());
            Console.WriteLine("user2: add Dorian Gray");
            user2.AddBook("Dorian Gray");
            Console.WriteLine("user2.BooksCount = " + user2.GetBooksCount());

            Console.WriteLine("user1 books:");
            user1.PrintAllBooksName();
            Console.WriteLine("user2 books:");
            user2.PrintAllBooksName();

            Console.WriteLine($"Book info for user1 with index 0: {user1.GetBookInfo(0)}");
            Console.WriteLine($"Book info for user2 with index 1: {user2.GetBookInfo(1)}");

            Console.WriteLine("user1: remove Harry Potter");
            user1.RemoveBook("Harry Potter");
            Console.WriteLine("user1.BooksCount = " + user1.GetBooksCount());
            Console.WriteLine("user2: remove Sherlock Holmes");
            user2.RemoveBook("Sherlock Holmes");
            Console.WriteLine("user2.BooksCount = " + user2.GetBooksCount());

            Console.WriteLine("user2 books:");
            user2.PrintAllBooksName();

            Console.ReadLine();
        }
    }
}
