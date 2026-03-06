using JWTAuthenticationManager.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthenticationManager
{
    public class JWTTokenHandler
    {
        public const string JWT_SECURITY_KEY = "sfsdfwrwer34r34fsfsfs3434dsfsdf343434";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        

        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest request)
        {
            
            var tokenExpireTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimIdnetity = new ClaimsIdentity(new List<Claim> { 
                new Claim(JwtRegisteredClaimNames.Name, request.UserName),
                new Claim("Role",request.Role[0])
            });

            var signingCredential =  new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimIdnetity,
                Expires = tokenExpireTimeStamp,
                SigningCredentials = signingCredential
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse 
            { 
                UserName = request.UserName,
                ExpireIn = (int)tokenExpireTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                Token = token
            };

        }

    }
}
