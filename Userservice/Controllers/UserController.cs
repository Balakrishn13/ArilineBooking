using Userservice.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Userservice.Controllers
{
    
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Route("/api/v1.0/flight/UserRegister")]
        public ActionResult UserRegister(User user)
        {

            User rep = new User();
            try
            {
                string Msg = rep.AddAirline(user, _configuration);
                return Ok(Msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }


        [HttpPost]
        [Route("/api/v1.0/flight/GetUser")]
        public ActionResult GetUser(GetUser user)
        {

            User rep = new User();
            try
            {
                string Msg = rep.getUser(user, _configuration);
                return Ok(Msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("/api/v1.0/flight/Booking")]
        public ActionResult Booking(Booking booking)
        {

            Booking rep = new Booking();
            try
            {
                string Msg = rep.TicketBooking(booking, _configuration);
                return Ok(Msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("/api/v1.0/flight/ticket/{PNR}")]
        public ActionResult PNRSearch(string PNR)
        {

            Booking rep = new Booking();
            try
            {
                string Msg = rep.GetPNR(PNR, _configuration);
                return Ok(Msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete]
        [Route("/api/v1.0/flight/booking/cancel/{PNR}")]
        public ActionResult PNRDelete(string PNR)
        {

            Booking rep = new Booking();
            try
            {
                string Msg = rep.PNRDelete(PNR, _configuration);
                return Ok(Msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

    }
}
