using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork2_1_Library
{
    interface ILibraryUser
    {
        void AddBook(string book);
        void RemoveBook(string book);
        string BookInfo(int index);
        int BooksCount();
    }
}
