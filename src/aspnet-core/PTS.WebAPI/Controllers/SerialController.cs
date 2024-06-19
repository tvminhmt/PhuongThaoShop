using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;

namespace PTS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerialController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public SerialController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _unitOfWork._serialRepository.GetList());
        }
        [HttpPost("GetPaged")]
        public async Task<IActionResult> GetPaged(PagedRequestDto request)
        {
            return Ok(await _unitOfWork._serialRepository.GetPagedAsync(request));
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _unitOfWork._serialRepository.GetById(id));
        }
        [HttpPost("CreateOrUpdateAsync")]
        public async Task<IActionResult> CreateOrUpdateAsync(SerialDto objDto)
        {
            var obj = _mapper.Map<SerialEntity>(objDto);
            if (objDto.Id > 0)
            {
                return Ok(await _unitOfWork._serialRepository.Update(obj));
            }
            else
            {
                return Ok(await _unitOfWork._serialRepository.Create(obj));
            }
        }
        [HttpPost("CreateMany")]
        public async Task<IActionResult> CreateMany(List<SerialDto> listObjDto)
        {
            var listObj = _mapper.Map<List<SerialEntity>>(listObjDto);
            return Ok(await _unitOfWork._serialRepository.CreateMany(listObj));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _unitOfWork._serialRepository.Delete(id));
        }
    }
}
