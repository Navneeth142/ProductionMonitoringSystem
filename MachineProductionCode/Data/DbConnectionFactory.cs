using System.Configuration;
using System.Data.SqlClient;

namespace ProductionMonitoringSystem.Data
{
    public static class DbConnectionFactory
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(
                ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString
            );
        }
    }
}
