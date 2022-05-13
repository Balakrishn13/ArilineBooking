using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Userservice.Model
{
    public class BookingData
    {
        public string FlightId { get; set; }
        public string Journyfrom { get; set; }
        public string Journeyto { get; set; }

        public string PNR { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Food { get; set; }
        
    }
}
