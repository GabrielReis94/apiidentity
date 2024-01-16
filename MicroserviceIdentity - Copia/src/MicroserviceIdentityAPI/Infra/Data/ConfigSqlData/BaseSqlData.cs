using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ZstdSharp.Unsafe;

namespace MicroserviceIdentityAPI.Infra.Data.ConfigSqlData
{
    public abstract class BaseSqlData
    {
        private readonly List<DbParameter> ListParameters;

        public BaseSqlData()
        {
            ListParameters = new List<DbParameter>();
        }

        public static DataBase GetDataBase()
        {
            return DataBase.GetDataBase();
        }

        public DataBase GetConnection()
        {
            DataBase database;

            try
            {
                database = GetDataBase();
                
                return database;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            
            return null;
        }

        public void CreateInputParameters(string name, DbType type, object value)
        {
            SqlParameter sqlParameter = new ()
            {
                ParameterName = name,
                DbType = type,
                Value = value ?? DBNull.Value,
                Direction = ParameterDirection.Input
            };

            ListParameters.Add(sqlParameter);
        }

        public void Parameters(ExecutionParameter executionParameter)
        {
            try
            {
                foreach(var item in ListParameters)
                {
                    executionParameter.Add(item.ParameterName, item.Direction, item.DbType, item.Value);
                }

                ListParameters.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DbParameter AddParameter(string name, object value)
        {
            return new SqlParameter(name, value);
        }

        public DbParameter AddParameter(string name, DbType type, object value)
        {
            return new SqlParameter(name, type) { Value = value };
        }

        public DbCommand MakeCommand(string name, List<DbParameter> parameters)
        {
            DbCommand cmd = new SqlCommand(name);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters.ToArray());

            return cmd;
        }
    }
}