using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Transportly.Common.DTO;

namespace Transportly.Manager
{
    /// <summary>
    /// This manages all actions for Orders
    /// </summary>
    public class ScheduleManager : IScheduleManager<Schedule>
    {
        /// <summary>
        /// Import Schedule from file content
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Schedule Import(string content)
        {
            var days = JsonConvert.DeserializeObject<Dictionary<string, Day>>(content);
            foreach (var day in days)
            {
                day.Value.Number = int.Parse(day.Key);

                foreach (var flight in day.Value.Flights) // Once a flight is scheduled, set the Assigned order list, so this can be later loaded
                {
                    flight.AssignedOrders = new List<Order>();
                }
            }
           
            return new Schedule() {Days = days.Values.ToList()};
        }


        /// <summary>
        /// Print Schedule to console
        /// </summary>
        /// <param name="schedule"></param>
        public void Print(Schedule schedule)
        {
            foreach (var day in schedule.Days)
            {
                foreach (var flight in day.Flights)
                {
                    Console.WriteLine($"Flight: {flight.FlightId}, departure: {flight.Departure}, arrival: {flight.Arrival}, day: {day.Number}");
                }
            }
        }

    }
}
