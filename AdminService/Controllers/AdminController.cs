using AdminService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Controllers
{
    
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("/api/v1.0/flight/airline/register")]
        [HttpPost]
        public ActionResult  Registor(Airline airline)
        {
            Airline rep = new Airline();

            try
            {
                string Msg = rep.AddAirline(airline, _configuration);
                return Ok(Msg);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }


        [Route("/api/v1.0/flight/admin/login")]
        [HttpPost]
        public ActionResult login(Login AdminLogin)
        {
            Login rep = new Login();

            try
            {
                string Msg = rep.Admin(AdminLogin, _configuration);
                return Ok(Msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
        [Route("/api/v1.0/flight/airline/inventory/add")]
        [HttpPost]
        public ActionResult AddFlight(Flight flight)
        {
            Flight rep = new Flight();

            try
            {
                string Msg = rep.AddFlight(flight, _configuration);
                return Ok(Msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("/api/v1.0/flight/search")]
        [HttpPost]
        public ActionResult SearchFlight(FlightSearch flightSearch)
        {
            FlightSearch rep = new FlightSearch();

            try
            {
                string Msg = rep.SearchFlight(flightSearch, _configuration);
                return Ok(Msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }


    }
}
