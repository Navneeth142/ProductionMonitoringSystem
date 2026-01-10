using ProductionMonitoringSystem.Data;
using ProductionMonitoringSystem.Model;
using System;
using System.Data.SqlClient;

namespace ProductionMonitoringSystem.Repositories
{
    public class ProductionRunRepository
    {
        public int StartProductionRun(int machineId, int plannedDuration)
        {
            using (var conn = DbConnectionFactory.GetConnection())
            {
                using (var cmd = new SqlCommand(
                    @"INSERT INTO ProductionRun
                      (MachineId, StartTime, PlannedDuration, RunStatus)
                      OUTPUT INSERTED.RunId
                      VALUES (@machineId, @startTime, @plannedDuration, 'Running')", conn))
                {
                    cmd.Parameters.AddWithValue("@machineId", machineId);
                    cmd.Parameters.AddWithValue("@startTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@plannedDuration", plannedDuration);

                    conn.Open();
                    int runId = (int)cmd.ExecuteScalar();
                    return runId;
                }
            }
        }
    }
}
