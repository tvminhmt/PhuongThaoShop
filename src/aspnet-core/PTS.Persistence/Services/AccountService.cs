using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PTS.Persistence.Services
{
	public class AccountService : IAccountService
	{
		private readonly UserManager<UserEntity> _userManager;
		private readonly SignInManager<UserEntity> _signInManager;
		private readonly IConfiguration _configuration;

		public AccountService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
		}

		public async Task<LoginResponse> Login(string userName, string password)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if (user == null)
			{
				return new LoginResponse(false, null, null, null, null, null, null, false, null);
			}

			var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
			if (!result.Succeeded)
			{
				return new LoginResponse(false, null, null, null, null, null, null, false, null);
			}

			string token = GenerateJwtToken(user);
			IList<string> roles = new List<string>();
			bool isAdmin = false;

			try
			{
				roles = await _userManager.GetRolesAsync(user);
				isAdmin = roles.Contains("admin");
			}
			catch (Exception ex)
			{
				//_logger.LogError(ex, "An error occurred while getting roles for user {UserName}", userName);
				return new LoginResponse(true, user.UserName, user.FullName, user.PhoneNumber, user.Address, user.Email, null, false, token);
			}
			return new LoginResponse(true, user.UserName, user.FullName, user.PhoneNumber, user.Address, user.Email, roles.FirstOrDefault(), isAdmin, token);
		}

		private string GenerateJwtToken(UserEntity user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			//	new Claim(ClaimTypes.NameIdentifier, user.Id)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Issuer"],
				claims: claims,
				expires: DateTime.Now.AddDays(30),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
