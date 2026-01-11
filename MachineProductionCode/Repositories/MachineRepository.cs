using ProductionMonitoringSystem.Data;
using ProductionMonitoringSystem.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionMonitoringSystem.Repositories
{
    public class MachineRepository
    {
        public Machine GetMachineById(int machineId)
        {
            using (var conn = DbConnectionFactory.GetConnection())
            {
                using (var cmd = new SqlCommand(
                    "SELECT * FROM Machine WHERE MachineId=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", machineId);

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        return new Machine
                        {
                            MachineId = (int)reader["MachineId"],
                            MachineName = reader["MachineName"].ToString(),
                            TheoreticalMaxOutput = (int)reader["TheoreticalMaxOutput"],
                            Status = reader["Status"].ToString()
                        };
                    }
                }
            }
        }
        public int GetMachineMaxOutput(int machineId)
        {
            using (var conn = DbConnectionFactory.GetConnection())
            {
                using (var cmd = new SqlCommand(
                    "SELECT TheoreticalMaxOutput FROM Machine WHERE MachineId=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", machineId);

                    conn.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
