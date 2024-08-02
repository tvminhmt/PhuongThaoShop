using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;
using PTS.WebAPI.Controllers;
using PTS.Domain.Model.Base;
using PTS.Application.Features.CardVGA.Commands;
using PTS.Application.Features.CardVGA.Queries;

namespace PTS.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardVGAController : BaseController
    {

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new CardVGAGetAllQuery()));
        }

        [HttpPost("GetPage")]
        public async Task<IActionResult> GetPage(CardVGAGetPageQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(CardVGAGetByIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate(CardVGACreateOrUpdateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteCardVGA(CardVGADeleteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
