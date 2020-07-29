using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json;
using Transportly.Common.DTO;

namespace Transportly.Manager
{
    /// <summary>
    /// This manages all actions for Orders
    /// </summary>
    public class OrderManager : IOrderManager<IEnumerable<Order>>
    {
        private int OrderCapacityForFlight => int.Parse(ConfigurationManager.AppSettings["OrderCapacity"]);

        /// <summary>
        ///  Import Orders from file content
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IEnumerable<Order> Import(string content)
        {
            var orders = JsonConvert.DeserializeObject<Dictionary<string, Order>>(content);
            foreach (var order in orders)
            {
                order.Value.OrderId = order.Key;
            }

            return orders.Values.ToList();
        }

        /// <summary>
        /// Loop through Order and assigned them to flights
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        public Tuple<Schedule, IEnumerable<Order>> MapOrderToFlights(Schedule schedule, IEnumerable<Order> orders)
        {
            var unAssignedOrders = new List<Order>();
            var flights = schedule.Days.SelectMany(x => x.Flights);
            foreach (var order in orders)
            {
                var flight = flights.FirstOrDefault(x => 
                    x.Arrival.Equals(order.Destination, StringComparison.InvariantCultureIgnoreCase)
                    && x.AssignedOrders.Count<Order>() < OrderCapacityForFlight);

                if (flight != null)
                {
                    flight.AssignedOrders.Add(order);
                }
                else
                {
                    unAssignedOrders.Add(order);
                }
            }

            return new Tuple<Schedule, IEnumerable<Order>>(schedule, unAssignedOrders);
        }


        /// <summary>
        /// Print Assigned and Unassigned Orders to Console
        /// </summary>
        /// <param name="ordersTuple"></param>
        public void Print(Tuple<Schedule, IEnumerable<Order>> ordersTuple)
        {
            // Display assigned orders
            foreach (var day in ordersTuple.Item1.Days) // loops can be handled in a better way 
            {
                foreach (var flight in day.Flights)
                {
                    foreach (var order in flight.AssignedOrders)
                    {
                        Console.WriteLine(
                            $"order: {order.OrderId}, flight: {flight.FlightId}, departure: {flight.Departure}, arrival: {flight.Arrival}, day: {day.Number}");
                    }
                }
            }

            // display unassigned order
            foreach (var order in ordersTuple.Item2)
            {
                Console.WriteLine(
                    $"order: {order.OrderId}, flightNumber: not scheduled");
            }
        }
    }
}
