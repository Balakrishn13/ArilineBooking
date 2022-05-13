using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Model
{
    public class AirlineData
    {
        public string FlightID { get; set; }
        public string AirlineId { get; set; }
        public string Airline_Name { get; set; }
      
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string Food { get; set; }
        public string Price { get; set; }
       
        public string Sheduled { get; set; }
        public string Status { get; set; }
    }
}
