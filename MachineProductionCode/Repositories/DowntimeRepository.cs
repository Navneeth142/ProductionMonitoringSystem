using System;
using System.Data.SqlClient;
using ProductionMonitoringSystem.Data;
using ProductionMonitoringSystem.Model;

namespace ProductionMonitoringSystem.Repositories
{
    public class DowntimeRepository
    {
        public void AddDowntimeIncident(DowntimeIncident incident)
        {
            int downtimeMinutes =
                (int)(incident.EndTime - incident.StartTime).TotalMinutes;

            using (var conn = DbConnectionFactory.GetConnection())
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1️⃣ Insert downtime incident
                        using (var cmd = new SqlCommand(
                            @"INSERT INTO DowntimeIncident
                              (RunId, IncidentType, StartTime, EndTime, Description, ResolvedBy)
                              VALUES (@runId, @type, @start, @end, @desc, @by)", conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@runId", incident.RunId);
                            cmd.Parameters.AddWithValue("@type", incident.IncidentType);
                            cmd.Parameters.AddWithValue("@start", incident.StartTime);
                            cmd.Parameters.AddWithValue("@end", incident.EndTime);
                            cmd.Parameters.AddWithValue("@desc", incident.Description);
                            cmd.Parameters.AddWithValue("@by", incident.ResolvedBy);

                            cmd.ExecuteNonQuery();
                        }

                        // 2️⃣ Update ProductionRun downtime
                        using (var cmd = new SqlCommand(
                            @"UPDATE ProductionRun
                              SET DowntimeMinutes = DowntimeMinutes + @minutes
                              WHERE RunId = @runId", conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@minutes", downtimeMinutes);
                            cmd.Parameters.AddWithValue("@runId", incident.RunId);

                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
