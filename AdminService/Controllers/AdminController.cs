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

        [Route("/api/v1.0/flight/airline/discount")]
        [HttpPost]
        public ActionResult discount(Adddisc adddisc)
        {
            Adddisc rep = new Adddisc();

            try
            {
                string Msg = rep.Add(adddisc, _configuration);
                return Ok(Msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
        [Route("/api/v1.0/flight/airline/getdiscount")]
        [HttpGet]
        public ActionResult getdiscount()
        {
            Adddisc rep = new Adddisc();

            try
            {
                string Msg = rep.GetDisc(_configuration);
                return Ok(Msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("/api/v1.0/flight/airline/deletediscount/{Id}/{Status}")]
        [HttpDelete]
        public ActionResult deletediscount(string Id,string Status)
        {
            Adddisc rep = new Adddisc();

            try
            {
                bool Msg = rep.Delete(Id, Status, _configuration);
                return Ok(Msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("/api/v1.0/flight/airline/getairline")]
        [HttpGet]
        public ActionResult getairline()
        {
            Airline rep = new Airline();

            try
            {
                string Msg = rep.GetAirline(_configuration);
                return Ok(Msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("/api/v1.0/flight/airline/deleteairline/{Id}/{Status}")]
        [HttpDelete]
        public ActionResult deleteairline(string Id, string Status)
        {
            Airline rep = new Airline();

            try
            {
                bool Msg = rep.Deleteairline(Id, Status, _configuration);
                return Ok(Msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("/api/v1.0/flight/airline/deleteflight/{Id}/{Status}")]
        [HttpDelete]
        public ActionResult deleteflight(string Id, string Status)
        {
            Airline rep = new Airline();

            try
            {
                bool Msg = rep.Deleteflight(Id, Status, _configuration);
                return Ok(Msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("/api/v1.0/flight/airline/getflight")]
        [HttpGet]
        public ActionResult getflight()
        {
            Airline rep = new Airline();

            try
            {
                string Msg = rep.GetFlight(_configuration);
                return Ok(Msg);
            }
            catch (Exception ex)
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
                if (Msg != "No Data Found")
                {
                    return Ok(Msg);
                }
                else
                {
                    return Unauthorized(Msg);
                }
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
