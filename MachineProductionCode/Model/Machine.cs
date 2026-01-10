using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProductionMonitoringSystem.Model
{
    public class Machine
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public int ProductionLineId { get; set; }
        public int TheoreticalMaxOutput { get; set; }
        public string Status { get; set; }
    }
}
