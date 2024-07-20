using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTS.Application.Dto;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Services;
using PTS.Domain.Entities;
using PTS.Shared.Dto;

namespace PTS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : BaseController
    {
        private readonly ISendMailService _emailService;
        private readonly ResponseDto _reponse;
        private readonly IConfiguration _config;
        public UtilityController(ISendMailService emailService, IConfiguration config)
        {
            _emailService = emailService;
            _reponse = new ResponseDto();
            _config = config;
        }
    }
}