using System;
using ProductionMonitoringSystem.Repositories;

namespace ProductionMonitoringSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            var repo = new MachineRepository();
            var machine = repo.GetMachineById(1);

            if (machine == null)
                Console.WriteLine("Machine not found");
            else
                Console.WriteLine(machine.MachineName);
        }
    }
}
