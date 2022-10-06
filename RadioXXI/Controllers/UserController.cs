using Microsoft.AspNetCore.Mvc;
using RadioXXI.Business.Interfaces;
using RadioXXI.Models;
using RadioXXI.Models.Dtos;
using System;

namespace RadioXXI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUserBusiness _business;

        public UserController(IUserBusiness business)
        {
            _business = business;
        }

        [HttpPost("new")]
        public IActionResult newUser([FromBody] Users newUser)
        {
            try
            {
                _business.newUser(newUser);

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] UserLoginDto userLogin)
        {
            try
            {
                var user = _business.login(userLogin);

                if(user != null)
                {
                    return Ok(user);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("recovery/{email}")]
        public IActionResult Update(string email)
        {
            try
            {
                var user = _business.getByEmail(email);

                if(user != null)
                {
                    return Ok(user);
                }

                return NotFound();

            }
            catch(Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [HttpGet("all")]
        public IActionResult getAll()
        {
            try
            {
                var all = _business.getAll();
                
                if(all.Count != 0)
                {
                    return Ok(all);
                }

                return NotFound("Sin usuarios registrados");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }
    }
}
