using System.Data.Common;

namespace MicroserviceIdentityAPI.Infra.Data.ConfigSqlData
{
    public class ExecutionResultReader
    {
        public DbParameter[] Parameters { get; protected set; }
        public DbDataReader Reader { get; protected set; }

        internal ExecutionResultReader(DbDataReader reader)
                : this(reader, new DbParameter[0])
            {}
        
        internal ExecutionResultReader(DbDataReader reader, params DbParameter[] parameters)
        {
            Reader = reader;
            Parameters = parameters;
        }
    }
}