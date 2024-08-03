using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Services;
using PTS.Application.Features.Account.Commands;
using PTS.Application.Features.Account.Queries;

namespace PTS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
		private readonly UserManager<UserEntity> _userManager;
		private readonly SignInManager<UserEntity> _signInManager;
		private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager,IConfiguration configuration, IMediator mediator, IMapper mapper)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
            _mediator = mediator;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpPost("CreateOrUpdateAccount")]
        public async Task<IActionResult> CreateOrUpdateAccount(CreateOrUpdateAccountQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
		[AllowAnonymous]
		[HttpPost("Login")]
		public async Task<IActionResult> Login(LoginQuery query)
		{
			return Ok(await _mediator.Send(query));
		}
		[HttpPost("Logout")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok(new { message = "Đã đăng xuất thành công" });
		}
		[AllowAnonymous]
        [HttpGet("GetMyInfor")]
        public async Task<IActionResult> GetMyInfor()
        {
         //   var x = AppSettings.RootPath;
            var UserName = User.Identity.Name;
            return Ok(UserName);
        }
        [HttpPost("isAdmin")]
       // [Authorize]
        public async Task<IActionResult> IsAdmin()
        {
            //var user = await _userManager.GetUserAsync(User);
            //if (user == null)
            //{
            //    return Unauthorized();
            //}

            //var roles = await _userManager.GetRolesAsync(user);
            //var isAdmin = roles.Contains("Admin");
            //return Ok(isAdmin);
            return Ok(true);
        }
    }
}
