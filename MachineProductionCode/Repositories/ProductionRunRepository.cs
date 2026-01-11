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
        public ProductionRun GetRunById(int runId)
        {
            using (var conn = DbConnectionFactory.GetConnection())
            {
                using (var cmd = new SqlCommand(
                    "SELECT * FROM ProductionRun WHERE RunId=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", runId);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        return new ProductionRun
                        {
                            RunId = (int)reader["RunId"],
                            MachineId = (int)reader["MachineId"],
                            StartTime = (DateTime)reader["StartTime"],
                            EndTime = reader["EndTime"] as DateTime?,
                            PlannedDuration = (int)reader["PlannedDuration"],
                            ActualUnitsProduced = (int)reader["ActualUnitsProduced"],
                            DefectiveUnits = (int)reader["DefectiveUnits"],
                            DowntimeMinutes = (int)reader["DowntimeMinutes"],
                            RunStatus = reader["RunStatus"].ToString()
                        };
                    }
                }
            }
        }
        public void EndProductionRun(
    int runId,
    int actualUnitsProduced,
    int defectiveUnits)
        {
            using (var conn = DbConnectionFactory.GetConnection())
            {
                using (var cmd = new SqlCommand(
                    @"UPDATE ProductionRun
              SET EndTime = @endTime,
                  ActualUnitsProduced = @actualUnits,
                  DefectiveUnits = @defectiveUnits,
                  RunStatus = 'Completed'
              WHERE RunId = @runId", conn))
                {
                    cmd.Parameters.AddWithValue("@endTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@actualUnits", actualUnitsProduced);
                    cmd.Parameters.AddWithValue("@defectiveUnits", defectiveUnits);
                    cmd.Parameters.AddWithValue("@runId", runId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
