using System;
using System.Data;
using System.Data.SqlClient;

namespace DataSets
{
    public class DB : IDisposable
    {
        public string ConnectionString;
        public SqlConnection Connection;

        public DB(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.Connection = new SqlConnection(this.ConnectionString);
            this.Connection.Open();
        }

        ~DB()
        {
            this.Dispose();
        }

        public SqlDataReader ReadEmailTable()
        {
            var sqlCommand = new SqlCommand($"select * from dbo.email", this.Connection);
            var sqlReader = sqlCommand.ExecuteReader();

            return sqlReader;
        }

        public int InsertIntoEmailTable()
        {
            var sqlCommand = new SqlCommand($"insert into dbo.email values (5, 'testUser@mail.com', 'L_6')", this.Connection);
            return executeSqlQuery(sqlCommand);
        }

        public int UpdateEmailTable()
        {
            var sqlCommand = new SqlCommand($"update dbo.email set lc_id = 'L_2' where em_Id = 5", this.Connection);
            return executeSqlQuery(sqlCommand);
        }

        public int DeleteFromEmailTable()
        {
            var sqlCommand = new SqlCommand($"delete from dbo.email where em_Id = 5", this.Connection);
            return executeSqlQuery(sqlCommand);
        }

        private int executeSqlQuery(SqlCommand sqlCommand)
        {
            int rowsAffected = sqlCommand.ExecuteNonQuery();

            return rowsAffected;
        }

        public void printEmailValues()
        {
            var sqlReader = this.ReadEmailTable();

            while (sqlReader.Read())
            {
                readSingleRow(sqlReader);
            }

            sqlReader.Close();
        }
        private static void readSingleRow(IDataRecord record)
        {
            Console.WriteLine($"email id: {record[0]}, email: {record[1]}, lecturer id: {record[2]}");
        }

        public void Dispose()
        {
            this.Connection.Close();
        }
    }
}
