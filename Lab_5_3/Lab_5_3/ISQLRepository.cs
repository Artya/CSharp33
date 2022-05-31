using System;
using System.Data.SqlClient;

namespace Lab_5_3
{
    public interface ISQLRepository
    {
        public SqlConnection Connection { get; }
        public string SchemaName { get;  }
        public string TableName { get;  }
    }
}
