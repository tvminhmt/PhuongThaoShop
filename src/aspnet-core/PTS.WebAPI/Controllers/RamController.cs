using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;
using PTS.WebAPI.Controllers;
using PTS.Domain.Model.Base;
using PTS.Application.Features.Ram.Commands;
using PTS.Application.Features.Ram.Queries;

namespace PTS.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamController : BaseController
    {

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new RamGetAllQuery()));
        }

        [HttpPost("GetPage")]
        public async Task<IActionResult> GetPage(RamGetPageQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(RamGetByIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> Create(RamCreateOrUpdateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteRam(RamDeleteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
