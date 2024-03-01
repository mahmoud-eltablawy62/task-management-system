using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using TaskManagementSystem.api.Dtos;
using TaskManagementSystem.api.Errors;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.core.Services.Contract;
using System.Data;
using TaskManagementSystem.Core.Entities.Identity;
using Microsoft.AspNetCore.Authentication;

namespace TaskManagementSystem.api.Controllers
{

    public class AccountUserController : BController
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly IAuthService _AuthService;
      

        public AccountUserController(UserManager<User> usermanager,
                                 SignInManager<User> Sign_InManager,
                                 IAuthService AuthService,
                                 RoleManager<IdentityRole> roleManager
                                 
            )
        {
            _UserManager = usermanager;
            _SignInManager = Sign_InManager;
            _AuthService = AuthService;
            _RoleManager = roleManager;
            
        }

        [HttpGet("CkeckEmail")]
        public async Task<ActionResult<bool>> CheckedEmail(string Email)
        {
            return await _UserManager.FindByEmailAsync(Email) is not null;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Registration(RegestrationDto Model, string UserRole)
        {
            if (CheckedEmail(Model.Email).Result.Value)
            {
                return BadRequest(new ApiValidation()
                { Errors = new string[] { "this email is exist" } });
            }


            var user = new User()
            {
                Name = Model.DisplayName,
                Email = Model.Email,
                UserName = Model.Email.Split('@')[0],
                PhoneNumber = Model.PhoneNumber
            };

            var Result = await _UserManager.CreateAsync(user, Model.Password);

            await _UserManager.AddToRoleAsync(user, UserRole);

            if (Result.Succeeded is false) { return BadRequest(new ApiResponse(400)); }


            return Ok(new UserDto()
            {
                Name = user.Name,
                Email = user.Email,
                Token = await _AuthService.CreateTokenAsync(user, _UserManager)
            }
            );
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto Login)
        {
            var user = await _UserManager.FindByEmailAsync(Login.Email);
            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var pass = await _SignInManager.CheckPasswordSignInAsync(user, Login.Password, false);
            if (pass.Succeeded is false) { return Unauthorized(new ApiResponse(401)); }

            return Ok(new UserDto()
            {
                Name = user.Name,
                Email = user.Email,
                Token = await _AuthService.CreateTokenAsync(user, _UserManager)

            });
        }

        [Authorize(Roles = "Admin,User")]

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return Ok("Sign-out successful");
        }

       
    }
}






