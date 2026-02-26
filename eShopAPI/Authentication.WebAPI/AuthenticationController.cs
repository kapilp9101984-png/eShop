using Auth.Application.DTO;
using Auth.Application.Request;
using JWTAuthenticationManager;
using JWTAuthenticationManager.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Authentication.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JWTTokenHandler  _jWtTokenHandler;
        private readonly IMediator _mediator;
        public AuthenticationController(JWTTokenHandler jWTTokenHandler, IMediator mediator)
        {
            _jWtTokenHandler = jWTTokenHandler;
            _mediator = mediator;
        }

        [HttpPost("login")]
        public ActionResult<AuthenticationResponse> Authenticate([FromBody] AuthenticationRequest request)
        {
            //var authenticationResponse = _jWtTokenHandler.GenerateJwtToken(request);

            //if (authenticationResponse == null) 
            //{
            //    return Unauthorized();
            //}

            //return authenticationResponse;

            return Ok(null);
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterUser([FromBody] UserDTO userDTO)
        {
            return await _mediator.Send(new UserRequest(userDTO));
        }
    }
}
