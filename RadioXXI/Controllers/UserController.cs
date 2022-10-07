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

        #region NEW USER

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

        #endregion

        #region LOGIN

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

        #endregion

        #region RECOVERY PASSWORD

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

        #endregion

        #region LIST ALL USERS

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

        #endregion

        #region UPDATE  USER

        [HttpPut("update/{id}")]
        public IActionResult update([FromBody] Users update, int id)
        {
            try
            {
                _business.update(update, id);

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        #endregion

        #region DELETE USER

        [HttpDelete("delete/{id}")]
        public IActionResult delete(int id)
        {
            try
            {
                _business.delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        #endregion
    }
}
