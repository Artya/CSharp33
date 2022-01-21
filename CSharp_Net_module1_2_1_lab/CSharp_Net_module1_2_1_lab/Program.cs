using System;

namespace CSharp_Net_module1_2_1_lab
{

    class Program
    {
        static void Main(string[] args)
        {
            var user1 = new LibraryUser();
            var user2 = new LibraryUser("Maria", "Ivanenko", "+380447777777", 3);
            Console.WriteLine("User1 ID " + user1.ID + " " + user1.FirstName + " " + user1.LastName);
            Console.WriteLine ("User2 ID " + user2.ID + " " + user2.FirstName + " " + user2.LastName);
                
            Console.WriteLine("User 1: add Harry Potter");
            user1.AddBook("Harry Potter");
            Console.WriteLine("User 2: add Sherlock Holmes");
            user2.AddBook("Sherlock Holmes");
            Console.WriteLine("user1.BooksCount = " + user1.BooksCount() + "; user2.BooksCount " + user2.BooksCount());
            Console.WriteLine("user2 :");
            Console.WriteLine("Add Kobzar");
            user2.AddBook("Kobzar");
            Console.WriteLine("user2.BooksCount " + user2.BooksCount());
            Console.WriteLine("Add Dorian Gray");
            user2.AddBook("Dorian Gray");
            Console.WriteLine("user2.BooksCount " + user2.BooksCount());

            Console.WriteLine("user2 books: " + user2[0] + "; " + user2[1] + "; " + user2[2]);
            Console.WriteLine("user2 books: " + user2.BookInfo(0) + "; " + user2.BookInfo(1) + "; " + user2.BookInfo(2));
            Console.WriteLine("Remove Sherlock Holmes");
            user2.RemoveBook("Sherlock Holmes");
            Console.WriteLine("user2.BooksCount "+user2.BooksCount());
            Console.WriteLine("user2 books: " + user2[0] + "; " + user2[1]);
            Console.WriteLine("user2 books: " + user2.BookInfo(0) + "; " + user2.BookInfo(1));
        }
    }
}
