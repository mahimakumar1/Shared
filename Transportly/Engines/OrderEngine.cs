using System;
using System.Collections.Generic;
using System.Text;
using Transportly.Common;
using Transportly.Manager;
using System.Configuration;
using Transportly.Common.DTO;

namespace Transportly.Engines
{
    /// <summary>
    /// This Engine handles Orders. All actions related to Orders should run through this engine
    /// </summary>
    public class OrderEngine : EngineBase
    {
        private OrderManager OrderManager;
        
       public OrderEngine(string path)
        {
            FilePath = path;
            this.OrderManager = new OrderManager();
        }

        public override void DoWork()
        {
            // Get File contect
            var fileProcessor = FileProcessorFactory.GetFileProcessor(FilePath);
            var content = fileProcessor.GetFileContent();

            // Import order
            var orders = OrderManager.Import(content);

            // as part of this engine schedule needs to be referenced too, so we can assign order to scheduled flights 
            // so loaded schedule again 
            var schedule = GetScheduleToLoadOrder();
            // Map Order to Schedule
            var mapOrderToFlights = OrderManager.MapOrderToFlights(schedule, orders);

            OrderManager.Print(mapOrderToFlights);
        }

        //Note: Ideally once schedule is loaded through schedule engine, it will be persisted in a DB so we would not need to load it again from file system
        Schedule GetScheduleToLoadOrder()
        {
            var fileProcessor = FileProcessorFactory.GetFileProcessor(ConfigurationManager.AppSettings["scheduleFile"]);
            var content = fileProcessor.GetFileContent();

            var scheduleManager = new ScheduleManager();
            return scheduleManager.Import(content);
        }

    }
}
