using App.Shared.Core.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PTS.Application.Dto;
using PTS.Application.DTOs;
using PTS.Domain.Entities;
using PTS.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Application.Features.Account.Queries
{
	public record LoginQuery : IRequest<LoginResponse>
	{
		public string Username { get; set; }
		public string Password { get; set;}
		public bool IsRemember {  get; set; }	
	}
	internal class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
	{
		private readonly UserManager<UserEntity> _userManager;
		private readonly SignInManager<UserEntity> _signInManager;
		private readonly IConfiguration _configuration;
		public LoginQueryHandler(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
		}
		public async Task<LoginResponse> Handle(LoginQuery queryInput, CancellationToken cancellationToken)
		{
			var result = await _signInManager.PasswordSignInAsync(queryInput.Username,queryInput.Password, queryInput.IsRemember, false);

			if (result.Succeeded)
			{
				var user = _userManager.Users.SingleOrDefault(r => r.UserName == queryInput.Username);
		     	var roles = await _userManager.GetRolesAsync(user);
		      	bool isAdmin =	roles.Contains("Admin");
				var token = GenerateJwtToken(user);
				return new LoginResponse(user.Id, true, user.UserName, user.FullName, user.PhoneNumber, "", user.Email, roles.FirstOrDefault(), isAdmin, token);
			}
			return new LoginResponse(0,false, null, null, null, null, null, null, false, null);
		}
		private string GenerateJwtToken(UserEntity user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new Claim(ClaimTypes.NameIdentifier, user.FullName)
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
