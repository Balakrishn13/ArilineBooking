using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Userservice.Model
{
    public class AirlineData
    {
        public string FlightID { get; set; }
        public string AirlineId { get; set; }
        public string Airline_Name { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string Food { get; set; }
        public string Price { get; set; }
        public string NoOfBUSeats { get; set; }
        public string NoOfNONBUSeats { get; set; }
        public string Remarks { get; set; }
        public string NoOfRows { get; set; }
        public string Sheduled { get; set; }
    }
}
