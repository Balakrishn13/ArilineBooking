using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Model
{
    public class GetAirline
    {
        public string Id { get; set; }
        public string AirlineName { get; set; }
        public string ContactNumber { get; set; }
        public string ContactAddress { get; set; }
        public string Status { get; set; }
    }
}
