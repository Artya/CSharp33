using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab_5_3
{
    public static class QueryBuilder
    {
        public static string GetSelectQuery(string schema, string table, List<string> fields = null, Dictionary<string, string> conditions = null)
        {
            var builder = new StringBuilder("SELECT ");

            AddFields(fields, builder);

            builder.AppendLine($"FROM {schema}.{table} as CurrentTable");

            AddConditions(conditions, builder);

            return builder.ToString();
        }

        public static string GetDeleteQuery(string schema, string table, Dictionary<string, string> conditions)
        {
            var builder = new StringBuilder($"DELETE FROM {schema}.{table} ");

            AddConditions(conditions, builder, false);

            return builder.ToString();
        }

        public static string GetInsertQueryForList(IEnumerable<DatabaseTable> dbObjects, ISQLRepository repository)
        {
            var totalQuery = new StringBuilder();
            var counter = 0;
            var objects = dbObjects.ToList();

            foreach (var dbObject in dbObjects)
            {
                var fields = dbObject.GetFields();

                var query = QueryBuilder.GetInsertQuery(repository.SchemaName, repository.TableName, fields, counter == 0);

                totalQuery.Append(query.ToString());

                if (counter < objects.Count - 1)
                    totalQuery.Append(',');

                counter++;
            }

            return totalQuery.ToString();
        }

        public static string GetIDGuery(string schema, string table)
        {
            return $"select max(ID)  from {schema}.{table}";
        }

        public static string GetInsertQuery(string schema, string table, Dictionary<string, string> fields, bool addFieldsNames = true)
        {
            var builderNames = new StringBuilder();
            var builderValues = new StringBuilder();

            if (addFieldsNames)
                builderNames.AppendLine($"INSERT INTO {schema}.{table}");

            builderValues.AppendLine();

            var count = 0;

            foreach (var field in fields)
            {
                if (count == 0)
                {
                    if (addFieldsNames)
                    {
                        builderNames.Append('(');
                        builderValues.Append("VALUES");
                    }

                    builderValues.Append("(");
                }

                if (count > 0)
                {
                    if (addFieldsNames)
                        builderNames.Append(", ");

                    builderValues.Append(", ");
                }

                if (addFieldsNames)
                    builderNames.Append(field.Key);

                builderValues.Append($"'{field.Value}'");

                if (count == fields.Count - 1)
                {
                    if (addFieldsNames)
                        builderNames.Append(')');

                    builderValues.Append(')');
                }

                count++;
            }

            return builderNames.ToString() + builderValues.ToString();
        }

        public static string GetUpdateQuery(string schema, string table, Dictionary<string, string> fields, Dictionary<string, string> conditions)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"UPDATE {schema}.{table}");
            builder.Append($"SET ");

            var first = true;

            foreach (var field in fields)
            {
                if (!first)
                    builder.Append(',');

                builder.AppendLine($" {field.Key} = '{field.Value}'");
                first = false;
            }

            AddConditions(conditions, builder, false);

            return builder.ToString();
        }

        private static void AddConditions(Dictionary<string, string> conditions, StringBuilder builder, bool withtAlias = true)
        {
            if (conditions == null)
                return;

            builder.Append("WHERE ");

            var firstCondition = true;

            foreach (var condition in conditions)
            {
                if (!firstCondition)
                    builder.Append("AND ");

                if (withtAlias)
                    builder.Append("CurrentTable.");

                builder.AppendLine($"{condition.Key} = {condition.Value}");

                firstCondition = false;
            }
        }

        private static void AddFields(List<string> fields, StringBuilder builder)
        {
            if (fields == null)
                builder.Append('*');

            if (fields != null)
            {
                var firstField = true;

                foreach (var field in fields)
                {
                    if (!firstField)
                        builder.Append(", ");

                    builder.AppendLine(field);

                    firstField = false;
                }
            }
        }

        public static Dictionary<string, string> GetIDConditions(int id)
        {
            var conditions = new Dictionary<string, string>();
            conditions.Add("ID", id.ToString());
            return conditions;
        }
    }
}
