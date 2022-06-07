using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Hello_DataSets
{
    class DBWork
    {
        private CommonDb commonDb;

        public DBWork(string connectionString)
        {
            this.commonDb = new CommonDb(connectionString);
        }

        public void DBConnect()
        {
            if (!commonDb.MyConnect())
                throw new ApplicationException("Failed to Connect database");

            if (!commonDb.MyDisConnect())
                throw new ApplicationException("Failed to Disconnect database");

            Console.WriteLine("Connect to DB succesfully");
        }

        public void CoursesUpdateDataSet(string tableName, string key, string keyValue, string column, string columnValue)
        {
            this.commonDb.MyTableUpdateDataSet(tableName, key, keyValue, column, columnValue);
        }

        public void CoursesInsertBuilder(string tableName, string key, string[] columns, string[] columnValues)
        {
            this.commonDb.MyTableInsertBuilder(tableName, key, columns, columnValues);
        }

        public void CoursesUpdateBuilder(string tableName, string key, string keyValue, string column, string columnValue)
        {
            this.commonDb.MyTableUpdateDataBuilder(tableName, key, keyValue, column, columnValue);
        }

        public void CoursesUpdate(string tableName, string key, string keyValue, string column, string columnValue)
        {
            var dataTable = new DataTable(tableName);
            this.commonDb.MyTableUpdate(dataTable, key, keyValue, column, columnValue);
        }

        public void CoursesRead(string tableName)
        {
            try
            {
                var dataTable = new DataTable(tableName);
                this.commonDb.MyTableRead(dataTable);

                foreach (var column in dataTable.Columns)
                {
                    Console.Write($"| {column} |");
                }
                Console.WriteLine();

                foreach (DataRow row in dataTable.Rows)
                {
                    for (var i = 0; i < dataTable.Columns.Count; i++)
                    {
                        Console.Write($"{row[dataTable.Columns[i].ToString()]} | ");
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CoursesDelete(string tableName, string key, string keyValue)
        {
            var dataTable = new DataTable(tableName);
            
            this.commonDb.MyTableDelete(dataTable, key, keyValue);
        }
    }
}
