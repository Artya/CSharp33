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
            var quantity = 0;
            foreach (var item in bookList)
            {
                if (item == book) return;
                if (item == null) break;
                quantity++;
            }
            if (quantity >= BookLimit) return;
            bookList[quantity] = book;
        }
        public void RemoveBook(string book)
        {
            if (!bookList.Contains(book))
            {
                Console.WriteLine("There is no such book in the library.");
                return; 
            }
            for (int index = Array.IndexOf(bookList, book); index < bookList.Length - 1; index++)
            {
                bookList[index] = bookList[index+1];
            }
            bookList[bookList.Length-1] = null;
        }
        public string BookInfo(int index)
        {
            if (bookList.Length-1 < index || index <0)
            {
                return "Index is out of range.";
            }
            return bookList[index];
        }
        public int BooksCount()
        {
            var quantity = 0;
            foreach (var item in bookList)
            {
                if (item == null) break;
                quantity++;
            }
            return quantity;
        }
    }
}
