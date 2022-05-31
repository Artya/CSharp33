using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Lab_5_3
{
    public static class SQLRepositoryHelper
    {
        public static void ExecuteInsertingObject(DatabaseTable dbObject, ISQLRepository repository, SqlTransaction transaction = null)
        {
            var fields = dbObject.GetFields();

            var builder = new StringBuilder();

            var query = QueryBuilder.GetInsertQuery(repository.SchemaName, repository.TableName, fields);
            builder.AppendLine(query);
            builder.AppendLine();

            query = QueryBuilder.GetIDGuery(repository.SchemaName, repository.TableName);
            builder.AppendLine(query);

            query = builder.ToString();

            var command = GetSQLCommand(repository.Connection, transaction, query);

            var reader = command.ExecuteReader();

            if (reader != null && reader.Read())
            {
                dbObject.ID = (int)reader[0];
            }

            reader.Close();
        }

        private static SqlCommand GetSQLCommand(SqlConnection connection, SqlTransaction transaction, string query)
        {
            if (transaction == null)
                return new SqlCommand(query, connection);

            return new SqlCommand(query, connection, transaction);
        }

        public static void ExecuteInsertingListObjects(IEnumerable<DatabaseTable> dbObjects, ISQLRepository repository)
        {
            var query = QueryBuilder.GetInsertQueryForList(dbObjects, repository);
            var command = new SqlCommand(query, repository.Connection);
            command.ExecuteNonQuery();
        }

        public static SqlDataReader GetSeletionAllReader(ISQLRepository repository)
        {
            var query = QueryBuilder.GetSelectQuery(repository.SchemaName, repository.TableName);
            var command = new SqlCommand(query, repository.Connection);
            var reader = command.ExecuteReader();

            return reader;
        }

        public static void ExecuteUpdatingObject(DatabaseTable obj, ISQLRepository repository, SqlTransaction transaction = null)
        {
            var fields = obj.GetFields();
            var conditions = QueryBuilder.GetIDConditions(obj.ID);
            var query = QueryBuilder.GetUpdateQuery(repository.SchemaName, repository.TableName, fields, conditions);

            var command = GetSQLCommand(repository.Connection, transaction, query);

            command.ExecuteNonQuery();
        }

        public static void ExecuteDeletingObject(int id, ISQLRepository repository, SqlTransaction transaction = null)
        {
            var conditions = QueryBuilder.GetIDConditions(id);
            var query = QueryBuilder.GetDeleteQuery(repository.SchemaName, repository.TableName, conditions);

            var command = GetSQLCommand(repository.Connection, transaction, query);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static SqlDataReader ExecuteGetObjectByID(int id, ISQLRepository repository)
        {
            var conditions = QueryBuilder.GetIDConditions(id);
            var query = QueryBuilder.GetSelectQuery(repository.SchemaName, repository.TableName, null, conditions);
            var command = new SqlCommand(query, repository.Connection);
            var reader = command.ExecuteReader();

            if (reader.Read())
                return reader;

            reader.Close();

            Console.WriteLine($"{repository.TableName} with ID {id} not found in Database");

            return null;
        }

        public static SqlDataReader ExecuteGetObjectByConditions(ISQLRepository repository, Dictionary<string, string> conditions)
        {
            var query = QueryBuilder.GetSelectQuery(repository.SchemaName, repository.TableName, null, conditions);
            var command = new SqlCommand(query, repository.Connection);
            var reader = command.ExecuteReader();

            return reader;
        }
    }
}
