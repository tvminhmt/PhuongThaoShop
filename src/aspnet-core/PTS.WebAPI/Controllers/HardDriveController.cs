using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;
using PTS.Shared.Dto;

namespace PTS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HardDriveController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public HardDriveController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _unitOfWork._hardDriveRepository.GetList());
        }
        [HttpPost("GetPaged")]
        public async Task<IActionResult> GetPaged(PagedRequestDto request)
        {
            return Ok(await _unitOfWork._hardDriveRepository.GetPagedAsync(request));
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _unitOfWork._hardDriveRepository.GetById(id));
        }
        [HttpPost("CreateOrUpdateAsync")]
        public async Task<IActionResult> CreateOrUpdateAsync(HardDriveDto objDto)
        {
            var obj = _mapper.Map<HardDriveEntity>(objDto);
            if (objDto.Id > 0)
            {
                return Ok(await _unitOfWork._hardDriveRepository.Update(obj));
            }
            else
            {
                return Ok(await _unitOfWork._hardDriveRepository.Create(obj));
            }
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _unitOfWork._hardDriveRepository.Delete(id));
        }
    }
}
