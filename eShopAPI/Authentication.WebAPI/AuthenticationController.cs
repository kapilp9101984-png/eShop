using JWTAuthenticationManager;
using JWTAuthenticationManager.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Authentication.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JWTTokenHandler  _jWtTokenHandler;
        public AuthenticationController(JWTTokenHandler jWTTokenHandler)
        {
            _jWtTokenHandler = jWTTokenHandler;
        }

        [HttpPost]
        public ActionResult<AuthenticationResponse> Authenticate([FromBody] AuthenticationRequest request)
        {
            var authenticationResponse = _jWtTokenHandler.GenerateJwtToken(request);

            if (authenticationResponse == null) 
            {
                return Unauthorized();
            }

            return authenticationResponse;
        }
    }
}
