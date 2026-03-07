using Auth.Application.DTO;
using Auth.Application.Request;
using Auth.Application.Response;
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
        private readonly JWTTokenHandler _jWtTokenHandler;
        private readonly IMediator _mediator;
        public AuthenticationController(JWTTokenHandler jWTTokenHandler, IMediator mediator)
        {
            _jWtTokenHandler = jWTTokenHandler;
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Authenticate([FromBody] LoginRequest request)
        {
            AuthResponse result = await _mediator.Send(new LoginRequest(request.UserName, request.Password));

            if (result.Status == ResponseStatus.Success)
            {
                return Ok(new AuthenticationResponse
                {
                    UserName = result.UserName,
                    Token = result.Token,
                    ExpireIn = result.ExpiresIn,
                    RefreshToken = result.RefreshToken                    
                });
            }
            else
            {
                return Unauthorized(new { Message = result.Message });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterUser([FromBody] UserDTO userDTO)
        {
            return await _mediator.Send(new UserRequest(userDTO));
        }
    }
}
