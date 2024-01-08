using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CallerInvoiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<UserDTO>> GetById([FromRoute] string id) 
        {
            var userDto = await _userService.GetUserByIdentification(id);

            if (userDto is null)
            {
                return NotFound();
            }

            return Ok(userDto);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> RegisterUser([FromBody] UserRegisterDTO userRegisterDTO)
        {
           // await _userService.PostUser(userRegisterDTO);
            return Ok();
        }
    }
}
