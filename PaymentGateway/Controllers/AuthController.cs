using Application.Dtos;
using Application.Helpers;
using Application.IServices;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PaymentGateway.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user =await _userService.GetUserByICNumberAsync(request.IcNumber);

            if(user is null)
            {
                return BadRequest(new { IsSuccess= false , IsExist= false });
            }

            return Ok(new { IsSuccess = true, IsExist = true, User = user });
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest user)
        {
    
           var result = await _userService.AddUserAsync(user);
           
           return Ok(new { IsSuccess =true , Message="User created successfully"});
        }


        [HttpPost("CreatePasscode")]
        public async Task<IActionResult> CreatePasscode( [FromBody] PasscodeRequest request)
        {

            var result = await _userService.CreatePasscode(request);

            if (!result.IsSuccess)
            {
                return BadRequest(new { IsSuccess = false , Message =result.Message});
            }


     
            return Ok(new { IsSuccess = true, Message = result.Message });
        }

        [HttpPost("VerifyPasscode")]
        public async Task<IActionResult> VerifyPasscode( [FromBody] PasscodeRequest request)
        {

            var result = await _userService.VerifyPasscode(request);

            if (!result.IsSuccess)
            {
                return BadRequest(new { IsSuccess = false, Message = result.Message });
            }
    
            return Ok(new { IsSuccess = true, Message = result.Message });
        }


        [HttpPost("ForgetPasscode")]
        public async Task<IActionResult> ForgetPasscode( [FromBody] ForgetPasscodeRequest request)
        {
            var result = await _userService.ForgetPasscode(request);

            if (!result.IsSuccess)
            {
                return BadRequest(new { IsSuccess = false, Message = result.Message});
            }

            return Ok(new { IsSuccess = true, Message = result.Message });
        }


        [HttpPost("SendOTPViaMobile")]
        public async Task<IActionResult> SendOTPViaMobile([FromBody] SendOTPRequest request)
        {
            var result = await _userService.SendOTPViaMobile(request);
            
            if (!result.IsSuccess)
            {
                return BadRequest(new { IsSuccess = false, Message = result.Message });
            }

            return Ok(new { IsSuccess = true, Message = result.Message });
        }

        [HttpPost("VerifyOTPViaMobile")]
        public async Task<IActionResult> VerifyOTPViaMobile([FromBody] VerifyOTPRequest request)
        {

            var result = await _userService.VerifyOTPViaMobile(request);

            if (!result.IsSuccess)
            {
                return BadRequest(new { IsSuccess = false, Message = result.Message });
            }

            return Ok(new { IsSuccess = true, Message = result.Message });
        }

        [HttpPost("SendOTPViaEmail")]
        public async Task<IActionResult> SendOTPViaEmail([FromBody] SendOTPRequest request)
        {
            var result = await _userService.SendOTPViaEmail(request);

            if (!result.IsSuccess)
            {
                return BadRequest(new { IsSuccess = false, Message = result.Message });
            }

            return Ok(new { IsSuccess = true, Message = result.Message });
        }

        [HttpPost("VerifyOTPViaEmail")]
        public async Task<IActionResult> VerifyOTPViaEmail([FromBody] VerifyOTPRequest request)
        {

            var result = await _userService.VerifyOTPViaEmail(request);

            if (!result.IsSuccess)
            {
                return BadRequest(new { IsSuccess = false, Message = result.Message });
            }

            return Ok(new { IsSuccess = true, Message = result.Message });
        }


    }
}

