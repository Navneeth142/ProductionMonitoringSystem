using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionMonitoringSystem.Model
{
    public class ProductionRun
    {
        public int RunId { get; set; }
        public int MachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int PlannedDuration { get; set; }
        public int ActualUnitsProduced { get; set; }
        public int DefectiveUnits { get; set; }
        public int DowntimeMinutes { get; set; }
        public string RunStatus { get; set; }
    }

}
