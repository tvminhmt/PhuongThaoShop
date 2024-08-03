using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;
using PTS.WebAPI.Controllers;
using PTS.Domain.Model.Base;
using PTS.Application.Features.Discount.Commands;
using PTS.Application.Features.Discount.Queries;

namespace PTS.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : BaseController
    {

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new DiscountGetAllQuery()));
        }

        [HttpPost("GetPage")]
        public async Task<IActionResult> GetPage(DiscountGetPageQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(DiscountGetByIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> Create(DiscountCreateOrUpdateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteDiscount(DiscountDeleteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
