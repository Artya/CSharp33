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
        public int BookCount { get => this.bookList.Count; }

        public readonly string FirstName;
        public readonly string LastName;
        public readonly int Id;
        public readonly int BookLimit;
        public readonly bool IsAnonymous;

        private List<string> bookList;

        public string this[int index]
        {
            get
            {
                if (index >= this.BookCount || index >= this.BookLimit || index < 0)
                    throw new IndexOutOfRangeException();
                return this.bookList[index];
            }
            set
            {
                if (index >= this.BookCount || index >= this.BookLimit || index < 0)
                    throw new IndexOutOfRangeException();
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
            this.bookList = new List<string>();
        }

        public LibraryUser(string firstName, string lastName, string phone, int bookLimit)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
            this.IsAnonymous = false;
            this.BookLimit = bookLimit;
            this.Id = LibraryUser.GetNextId();
            this.bookList = new List<string>();
        }

        public void AddBook(string bookName)
        {
            this.bookList.Add(bookName);
        }

        public void RemoveBook(string bookName)
        {
            this.bookList.Remove(bookName);
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
