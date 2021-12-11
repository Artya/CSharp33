using System;

namespace CSharp_Net_module1_2_1_lab
{

    // 1) declare interface ILibraryUser
    public interface ILibraryUser
    {
        // declare method's signature for methods of class LibraryUser
        bool AddBook(string bookName);
        void RemoveBook(string bookName);
        string BookInfo(int bookIndex);
        int BooksCount();
    }

    // 2) declare class LibraryUser, it implements ILibraryUser
    public class LibraryUser : ILibraryUser
    {
        private static int objectCounter = 0;

        private readonly string firstName;
        private readonly string lastName;
        private readonly int id;
        private readonly int bookLimit;
        // 4) declare field (bookList) as a string array
        private string[] bookList;


        // 3) declare properties: FirstName (read only), LastName (read only), 
        // Id (read only), Phone (get and set), BookLimit (read only)
        public string FirstName { get => firstName; }
        public  string LastName { get => lastName; }

        public int ID { get => id;  }

        public string Phone { get; set; }

        public int BookLimit { get => bookLimit;  }
       
        // 5) declare indexer BookList for array bookList
         public string this[int index]
        {
            get
            {
                if (ChekIndexInRange(index))
                    return bookList[index];

                throw new IndexOutOfRangeException("Index incorrect");
            }
        }

        // 6) declare constructors: default and parameter
        public LibraryUser()
        {
            this.id = ++objectCounter;
            this.firstName = "<No name>";
            this.lastName = "<No last name>";
            this.bookLimit = 1;
            this.Phone = "<no phone>";

            this.bookList = new string[bookLimit];
        }

        public LibraryUser(string firstName, string lastName, string phone, int bookLimit)
        {
            this.id = ++objectCounter;
            this.firstName = firstName;
            this.lastName = lastName;
            this.bookLimit = bookLimit;
            this.Phone = phone;

            this.bookList = new string[bookLimit];
        }

        // 7) declare methods: 
        public bool AddBook(string bookName) // – add new book to array bookList
        {
            var booksCount = BooksCount();

            if (booksCount >= bookLimit)
            {
                Console.WriteLine("Sorry, but you already have maximum count of books... Maybe you need to remove some books?");
                return false;
            }

            bookList[booksCount] = bookName;
            return true;

        }
        public void RemoveBook(string bookName)// – remove book from array bookList
        {
            var bookFound = false;

            for (var i = 0; i < bookLimit; i++)
            {
                if (!bookFound && bookName == bookList[i])
                {
                    bookFound = true;
                    bookList[i] = null;
                }

                if (bookFound && (i + 1) < bookLimit)
                {
                    bookList[i] = bookList[i + 1];
                    bookList[i + 1] = null;
                }
            }
        }
        public string BookInfo(int bookIndex)// – returns book info by index
        {
            if (ChekIndexInRange(bookIndex))
               return bookList[bookIndex];

            throw new IndexOutOfRangeException ("Incorrect index");
        }
        public int BooksCount()// – returns current count of books
        {
            for (var i = 0; i < bookLimit; i++)
            {
                if (bookList[i] == null)
                    return i;
            }

            return bookLimit;
        }

        private bool ChekIndexInRange(int index)
        {
            return index >= 0 && index < bookList.Length;
        }
    }
  
}
