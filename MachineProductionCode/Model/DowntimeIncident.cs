using System;

namespace ProductionMonitoringSystem.Model
{
    public class DowntimeIncident
    {
        public int IncidentId { get; set; }
        public int RunId { get; set; }
        public string IncidentType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string ResolvedBy { get; set; }
    }
}
