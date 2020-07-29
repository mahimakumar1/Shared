using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace Transportly.Common.DTO
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public IList<Order> AssignedOrders { get; set; }
       
    }
}
