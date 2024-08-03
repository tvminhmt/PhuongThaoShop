using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;
using OfficeOpenXml;

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
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            List<SerialEntity> list = new List<SerialEntity>();
            if (file == null || file.Length == 0)
                return BadRequest("Tệp không hợp lệ");
            try
            {
             using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 2; row < rowCount; row++)
                    {
                            var serialNumberCell = worksheet.Cells[row, 1].Value;
                            if (serialNumberCell == null)
                            {
                                continue; 
                            }

                            var serial = new SerialEntity
                            {
                                SerialNumber = serialNumberCell.ToString().Trim(),
                                Status = 1
                                // ProductDetailEntityId = 1
                            };
                            list.Add(serial);
                    }
                        await _unitOfWork._serialRepository.CreateMany(list);
                }
            }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok("Tệp đã được tải lên và xử lý thành công");
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
