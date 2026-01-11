using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionMonitoringSystem.Model
{
    public class OeeResult
    {
        public double Availability { get; set; }
        public double Performance { get; set; }
        public double Quality { get; set; }
        public double Oee { get; set; }
    }
}
