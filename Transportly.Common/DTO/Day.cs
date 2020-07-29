using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportly.Common.DTO
{
    public class Day
    {
        public int Number { get; set; }

        public IEnumerable<Flight> Flights { get; set; }
    }
}
