//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using PTS.Application.Dto;
//using PTS.Domain.Entities;
//using PTS.Application.Interfaces.Repositories;

//namespace PTS.WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CardVGAController : PTSBaseController
//    {
//        private readonly IAllRepository<CardVGAEntity> _repository;
//        private readonly IMapper _mapper;
//        public CardVGAController(IAllRepository<CardVGAEntity> repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        [HttpGet("GetAllAsync")]
//        public async Task<ActionResult<IEnumerable<CardVGADto>>> GetAllAsync()
//        {
//            var listObj = await _repository.GetAllAsync();
//            return Ok(listObj.Where(x => x.Status > 0));
//        }
//        [HttpGet("GetByIdAsync")]
//        public async Task<ActionResult<CardVGADto>> GetByIdAsync(int id)
//        {
//            var obj = await _repository.GetByIdAsync(id);
//            return Ok(obj);
//        }
//        [HttpPost("GetPagesAsync")]
//        public async Task<ActionResult<IEnumerable<CardVGADto>>> GetPagesAsync(int page = 1, int pageSize = 10)
//        {
//            var products = await _repository.GetPagedAsync(page, pageSize);
//            return Ok(products);
//        }
//        [HttpPost("CreateOrUpdateAsync")]
//        public async Task<ActionResult> CreateOrUpdateAsync(CardVGADto objDto)
//        {
//            var obj = _mapper.Map<CardVGAEntity>(objDto);
//            if (obj.Id > 0)
//            {
//                if (await _repository.UpdateAsync(obj))
//                    return Ok(obj);
//                return BadRequest();
//            }
//            else
//            {
//                if (await _repository.CreateAsync(obj))
//                    return Ok(obj);
//                return BadRequest();
//            }

//        }
//    }
//}
