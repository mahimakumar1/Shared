using System;
using System.Configuration;
using Transportly.Common;
using Transportly.Engines;

namespace Transportly
{
    class Program
    {
        static void Main(string[] args)
        {
            //These file path are in Assets folder (make sure they exist in bin)
            var scheduleFilePath = ConfigurationManager.AppSettings["scheduleFile"];
            var orderFilePath = ConfigurationManager.AppSettings["orderFile"];

            Console.WriteLine("** User Story 1 **");
            Console.WriteLine();
            var scheduleEngine = new ScheduleEngine(scheduleFilePath);
            scheduleEngine.DoWork();

            Console.WriteLine();
            
            Console.WriteLine("** User Story 2 **");
            Console.WriteLine();
            var orderEngine = new OrderEngine(orderFilePath);
            orderEngine.DoWork();
            
            Console.ReadLine();
        }
    }
}
