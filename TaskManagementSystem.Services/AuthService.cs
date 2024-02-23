using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.core.Services.Contract;
using TaskManagementSystem.Core.Entities.Identity;

namespace TaskManagementSystem.Services
{
    public class AuthService  : IAuthService
    {
        private readonly IConfiguration _Config;
        public AuthService(IConfiguration Config)
        {
            _Config = Config;
        }
        public async Task<string> CreateTokenAsync(User userApp, UserManager<User> userManager)
        {

            var _Claims = new List<Claim>()
            {

            new Claim (ClaimTypes.GivenName , userApp.UserName),  

            new Claim (ClaimTypes.Email , userApp.Email)  
                                                          
            };

            var User_Role = await userManager.GetRolesAsync(userApp);
            
            foreach (var role in User_Role)
            {
                _Claims.Add(new Claim(ClaimTypes.Role, role));  
            }

        

            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["JWT:SecretKey"]));
            var token = new JwtSecurityToken(
                audience: _Config["JWT:ValidAudience"],
                issuer: _Config["Jwt:ValidIssuer"],
                expires: DateTime.UtcNow.AddDays(double.Parse(_Config["JWT:expireDay"])),
                claims: _Claims,
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }







    }
}
