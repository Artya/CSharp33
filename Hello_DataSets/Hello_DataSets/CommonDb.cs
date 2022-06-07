using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Hello_DataSets
{
    public class CommonDb
    {
        public string MyConnectionString { get; set; } = default;
        public CommonDb(string connectionString)
        {
            this.MyConnectionString = connectionString;
        }

        public bool MyTableDelete(DataTable dataTable, string key, string keyValue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(MyConnectionString))
                { 
                    var command = connection.CreateCommand();
                    command.CommandText = $"select * from {dataTable.TableName}";
                    var adapter = new SqlDataAdapter(command);

                    adapter.Fill(dataTable);

                    dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns[key]};
                    dataTable.AcceptChanges();

                    var dataRow = dataTable.Rows.Find(keyValue);

                    var query = $"DELETE from {dataTable.TableName} WHERE {key}='{keyValue}'";

                    connection.Open();
                    adapter.UpdateCommand = connection.CreateCommand();
                    adapter.UpdateCommand.CommandText = query;
                    adapter.UpdateCommand.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool MyTableUpdate(DataTable table, string key, string keyValue, string column, string columnvalue)
        {
            try
            {
                using (var connection = new SqlConnection(MyConnectionString))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = $"select * from {table.TableName}";

                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(table);

                    table.PrimaryKey = new DataColumn[] { table.Columns[key] };
                    table.AcceptChanges();

                    var currentCourse = table.Rows.Find(keyValue);
                    Console.WriteLine($"Key: {currentCourse[key]}, Column {column} : {currentCourse[column]}");

                    var query = $"UPDATE {table.TableName} SET {column}='{columnvalue}' WHERE {key}='{keyValue}'";

                    connection.Open();
                    adapter.UpdateCommand = connection.CreateCommand();
                    adapter.UpdateCommand.CommandText = query;
                    adapter.UpdateCommand.ExecuteNonQuery();

                    Console.WriteLine($"Row updated to {columnvalue}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool MyTableInsertBuilder(string tableName, string key, string[] columns, string[] columnValues)
        {
            try
            {
                using (var connection = new SqlConnection(MyConnectionString))
                { 
                    var command = connection.CreateCommand();
                    command.CommandText = $"select * from {tableName}";
                    var dataAdapter = new SqlDataAdapter(command);
                    var builder = new SqlCommandBuilder(dataAdapter);
                    var dataSet = new DataSet();

                    dataAdapter.Fill(dataSet, tableName);

                    var dataTable = dataSet.Tables[tableName];
                    dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns[key] };
                    dataTable.AcceptChanges();

                    var newKey = NextKeyGen(dataTable, key);

                    var newRow = dataTable.NewRow();
                    newRow[key] = newKey;

                    for (var i = 0; i < columns.Length; i++)
                    {
                        newRow[columns[i]] = columnValues[i];
                    }

                    dataTable.Rows.Add(newRow);
                    dataAdapter.Update(dataSet, tableName);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private string NextKeyGen(DataTable dataTable, string key)
        {
            var privateKeys = new List<string>();
            foreach (DataRow row in dataTable.Rows)
            {
                privateKeys.Add(row[key].ToString());
            }

            privateKeys.Sort();

            var lastKeyValue = privateKeys.Last();
            Console.WriteLine($"Max key: {lastKeyValue} . Write next key value:");
            return Console.ReadLine();
        }

        public bool MyTableUpdateDataBuilder(string tableName, string key, string keyValue, string column, string columnvalue)
        {
            try
            {
                using (var connection = new SqlConnection(MyConnectionString))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = $"select * from {tableName}";

                    var sqlAdapter = new SqlDataAdapter(command);
                    var builder = new SqlCommandBuilder(sqlAdapter);
                    var dataSet = new DataSet();
                    sqlAdapter.Fill(dataSet, tableName);

                    var dataTable = dataSet.Tables[tableName];
                    dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns[key] };
                    dataTable.AcceptChanges();

                    var currentRow = dataTable.Rows.Find(keyValue);
                    Console.WriteLine($"Key: {currentRow[key]}, Column {column} : {currentRow[column]}");

                    currentRow[column] = columnvalue;
          
                    sqlAdapter.Update(dataSet, tableName);

                    Console.WriteLine($"Row updated to {columnvalue}!");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool MyTableUpdateDataSet(string tableName, string key, string keyValue, string column, string columnvalue)
        {
            try
            {
                using (var connection = new SqlConnection(MyConnectionString))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = $"select * from {tableName}";

                    var sqlAdapter = new SqlDataAdapter(command);
                    var dataSet = new DataSet();
                    sqlAdapter.Fill(dataSet, tableName);

                    var dataTable = dataSet.Tables[tableName];
                    dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns[key]};
                    dataTable.AcceptChanges();

                    var currentRow = dataTable.Rows.Find(keyValue);
                    Console.WriteLine($"Key: {currentRow[key]}, Column {column} : {currentRow[column]}");

                    currentRow[column] = columnvalue;
                    var query = $"UPDATE {tableName} SET {column}='{columnvalue}' WHERE {key}='{keyValue}'";

                    sqlAdapter.UpdateCommand = connection.CreateCommand();
                    sqlAdapter.UpdateCommand.CommandText = query;
                    sqlAdapter.Update(dataSet, tableName);

                    Console.WriteLine($"Row updated to {columnvalue}!");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool MyTableRead(DataTable dataTable)
        {
            try
            {
                using (var connection = new SqlConnection(MyConnectionString))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = $"select * from {dataTable.TableName}";

                    var sqlAdapter = new SqlDataAdapter(command);

                    sqlAdapter.Fill(dataTable);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool MyConnect()
        {
            try
            {
                using (var connection = new SqlConnection(MyConnectionString))
                {
                    connection.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool MyDisConnect()
        {
            try
            {
                using (var connection = new SqlConnection(MyConnectionString))
                {
                    connection.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
