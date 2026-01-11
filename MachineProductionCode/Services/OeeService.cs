using System;
using ProductionMonitoringSystem.Model;

namespace ProductionMonitoringSystem.Services
{
    public class OeeService
    {
        public OeeResult CalculateOee(
            ProductionRun run,
            int theoreticalMaxOutputPerHour)
        {
            if (run.RunStatus != "Completed")
                throw new Exception("OEE can be calculated only for completed runs.");

            // 1️⃣ Availability Efficiency
            int operatingTime = run.PlannedDuration - run.DowntimeMinutes;
            double availability =
                (double)operatingTime / run.PlannedDuration * 100;

            // 2️⃣ Performance Efficiency
            double plannedHours = run.PlannedDuration / 60.0;
            double theoreticalMaxUnits =
                theoreticalMaxOutputPerHour * plannedHours;

            double performance =
                run.ActualUnitsProduced / theoreticalMaxUnits * 100;

            // 3️⃣ Quality Rate
            int goodUnits =
                run.ActualUnitsProduced - run.DefectiveUnits;

            double quality =
                (double)goodUnits / run.ActualUnitsProduced * 100;

            // 4️⃣ Final OEE
            double oee =
                availability * performance * quality / 10000;

            return new OeeResult
            {
                Availability = availability,
                Performance = performance,
                Quality = quality,
                Oee = oee
            };
        }
    }
}
