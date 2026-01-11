using System;
using ProductionMonitoringSystem.Repositories;
using ProductionMonitoringSystem.Services;
using ProductionMonitoringSystem.Model;

namespace ProductionMonitoringSystem.UI
{
    public class ConsoleMenu
    {
        private readonly ProductionRunRepository _runRepo = new ProductionRunRepository();
        private readonly MachineRepository _machineRepo = new MachineRepository();
        private readonly DowntimeRepository _downtimeRepo = new DowntimeRepository();
        private readonly OeeService _oeeService = new OeeService();


        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Production Monitoring System ===");
                Console.WriteLine("1. Start Production Run");
                Console.WriteLine("2. End Production Run");
                Console.WriteLine("3. Record Downtime");
                Console.WriteLine("4. Calculate OEE");
                Console.WriteLine("0. Exit");
                Console.Write("Select option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StartRun();
                        break;
                    case "2":
                        EndRun();
                        break;
                    case "3":
                        RecordDowntime();
                        break;
                    case "4":
                        CalculateOee();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        Pause();
                        break;
                }
            }
        }

        private void StartRun()
        {
            Console.Write("Machine ID: ");
            int machineId = int.Parse(Console.ReadLine());

            Console.Write("Planned Duration (minutes): ");
            int duration = int.Parse(Console.ReadLine());

            int runId = _runRepo.StartProductionRun(machineId, duration);
            Console.WriteLine("Run started. RunId = " + runId);
            Pause();
        }

        private void EndRun()
        {
            Console.Write("Run ID: ");
            int runId = int.Parse(Console.ReadLine());

            Console.Write("Actual Units Produced: ");
            int actual = int.Parse(Console.ReadLine());

            Console.Write("Defective Units: ");
            int defective = int.Parse(Console.ReadLine());

            _runRepo.EndProductionRun(runId, actual, defective);
            Console.WriteLine("Run completed.");
            Pause();
        }

        private void RecordDowntime()
        {
            Console.Write("Run ID: ");
            int runId = int.Parse(Console.ReadLine());

            Console.Write("Incident Type: ");
            string type = Console.ReadLine();

            Console.Write("Downtime Minutes: ");
            int minutes = int.Parse(Console.ReadLine());

            var incident = new DowntimeIncident
            {
                RunId = runId,
                IncidentType = type,
                StartTime = DateTime.Now.AddMinutes(-minutes),
                EndTime = DateTime.Now,
                Description = "Recorded via console",
                ResolvedBy = "Operator"
            };

            _downtimeRepo.AddDowntimeIncident(incident);
            Console.WriteLine("Downtime recorded.");
            Pause();
        }

        private void CalculateOee()
        {
            Console.Write("Run ID: ");
            int runId = int.Parse(Console.ReadLine());

            var run = _runRepo.GetRunById(runId);
            int maxOutput = _machineRepo.GetMachineMaxOutput(run.MachineId);

            var result = _oeeService.CalculateOee(run, maxOutput);

            Console.WriteLine("--- OEE RESULT ---");
            Console.WriteLine("Availability: " + result.Availability);
            Console.WriteLine("Performance : " + result.Performance);
            Console.WriteLine("Quality     : " + result.Quality);
            Console.WriteLine("OEE         : " + result.Oee);
            Pause();
        }

        private void Pause()
        {
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
