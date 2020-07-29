using System;
using System.Collections.Generic;
using System.Text;
using Transportly.Common;
using Transportly.Common.DTO;
using Transportly.Manager;

namespace Transportly.Engines
{
    /// <summary>
    /// This Engine handles Schedules. All actions related to Schedules should run through this engine
    /// </summary>
    public class ScheduleEngine : EngineBase
    {
        private ScheduleManager ScheduleManager;
        public ScheduleEngine(string path)
        {
            FilePath = path;
            ScheduleManager = new ScheduleManager();
        }
        public override void DoWork()
        {
            // Import Schedule : assumption from a json file
            var fileProcessor = FileProcessorFactory.GetFileProcessor(FilePath);
            var content = fileProcessor.GetFileContent();

            // Import Schedule
            var schedule = ScheduleManager.Import(content);

            // Display Schedule
            ScheduleManager.Print(schedule);
        }

    }
}
