using System;
using System.Collections.Generic;

namespace Lab_1._2._1
{
    public interface ILibraryUser
    {
        void AddBook(string bookName);
        void RemoveBook(string bookName);
        string GetBookInfo(int index);
        int GetBooksCount();
    }

    public class LibraryUser : ILibraryUser
    {
        private static int nextId;
        public static int GetNextId()
        {
            return nextId++;
        }

        public string Phone { get; set; }
        public int BookCount { get; private set; }

        public readonly string FirstName;
        public readonly string LastName;
        public readonly int Id;
        public readonly int BookLimit;
        public readonly bool IsAnonymous;

        private string[] bookList;

        public string this[int index]
        {
            get
            {
                if (index >= this.BookCount || index >= this.BookLimit || index < 0)
                    throw new IndexOutOfRangeException($"Index must be: non negative integer, not greater or equal to book limit ({this.BookLimit}), not greater or equal to book count ({this.BookCount})");

                return this.bookList[index];
            }
            set
            {
                if (index >= this.BookCount || index >= this.BookLimit || index < 0)
                    throw new IndexOutOfRangeException($"Index must be: non negative integer, not greater or equal to book limit ({this.BookLimit}), not greater or equal to book count ({this.BookCount})");

                this.bookList[index] = value;
            }
        }

        public LibraryUser()
        {
            this.FirstName = "No name";
            this.LastName = "No last name";
            this.Phone = "No phone";
            this.IsAnonymous = true;
            this.BookLimit = 10;
            this.Id = LibraryUser.GetNextId();
            this.bookList = new string[this.BookLimit];
        }

        public LibraryUser(string firstName, string lastName, string phone, int bookLimit)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
            this.IsAnonymous = false;
            this.BookLimit = bookLimit;
            this.Id = LibraryUser.GetNextId();
            this.bookList = new string[this.BookLimit];
        }

        public void AddBook(string bookName)
        {
            if (this.BookCount + 1 > this.BookLimit)
                throw new IndexOutOfRangeException("Reached book limit");

            if (Array.IndexOf(this.bookList, bookName) != -1)
                throw new InvalidOperationException($"You already have book: {bookName}");

            this.bookList[this.BookCount] = bookName;
            this.BookCount++;
        }

        public void RemoveBook(string bookName)
        {
            var bookindex = Array.IndexOf(this.bookList, bookName);
            if (bookindex == -1)
                throw new InvalidOperationException($"You don't have book: {bookName}");

            do
            {
                if (bookindex + 1 == this.BookCount)
                {
                    this.bookList[bookindex] = null;
                    break;
                }

                this.bookList[bookindex] = this.bookList[bookindex + 1];
                bookindex++;
            } while (true);

            this.BookCount--;
        }

        public string GetBookInfo(int index)
        {
            return this[index];
        }

        public int GetBooksCount()
        {
            return this.BookCount;
        }

        public void PrintAllBooksName()
        {
            for (int bookIndex = 0; bookIndex < this.BookCount; bookIndex++)
                Console.WriteLine(@$"    {this[bookIndex]}");
        }
    }
}
