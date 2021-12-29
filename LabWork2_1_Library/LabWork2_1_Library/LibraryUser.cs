using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork2_1_Library
{
    class LibraryUser : ILibraryUser
    {
        private string[] bookList;
        private static int NextId = 0;
        private int bookCount = 0;
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Phone { get; set; }
        public int BookLimit { get; }
        public string this [int index]
        {
            get
            {
                return bookList[index];
            }
            set 
            {
                bookList[index] = value;
            } 
        }
        public LibraryUser()
        {
            Id = NextId++;
            FirstName = string.Empty;
            LastName = string.Empty;
            Phone = string.Empty;
            BookLimit = 10;

            bookList = new string[BookLimit];
        }
        public LibraryUser(string firstName, string lastName, string phone, int bookLimit)
        {
            Id = NextId++;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            BookLimit = bookLimit;

            bookList = new string[BookLimit];
        }
        public void AddBook(string book)
        {
            if (bookCount == BookLimit)
                return;
            if (Array.IndexOf(bookList, book) >= 0)
                return;
            bookList[bookCount] = book;
            bookCount++;
        }
        public void RemoveBook(string book)
        {
            if (!bookList.Contains(book))
            {
                Console.WriteLine("There is no such book in the library.");
                return; 
            }
            var bookPosition = Array.IndexOf(bookList, book);
            Array.Copy(bookList, bookPosition + 1, bookList, bookPosition, bookList.Length - bookPosition - 1);
            bookList[bookList.Length-1] = null;
            bookCount--;
        }
        public string BookInfo(int index)
        {
            if (bookList.Length - 1 < index || index < 0)
            {
                return "Index is out of range.";
            }
            return bookList[index];
        }
        public int BooksCount()
        {
             return bookCount;
        }
    }
}
