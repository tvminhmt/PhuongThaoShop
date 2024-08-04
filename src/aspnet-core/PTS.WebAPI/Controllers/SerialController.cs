using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;
using OfficeOpenXml;
using PTS.Application.Features.Serial.Commands;
using PTS.Application.Features.Serial.Queries;

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
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new SerialGetAllQuery()));
        }

        [HttpPost("GetPage")]
        public async Task<IActionResult> GetPage(SerialGetPageQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(SerialGetByIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate(SerialCreateOrUpdateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update(SerialUpdateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteSerial(SerialDeleteCommand command)
        {
            return Ok(await Mediator.Send(command));
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
        [HttpPost("CreateMany")]
        public async Task<IActionResult> CreateMany(List<SerialDto> listObjDto)
        {
            var listObj = _mapper.Map<List<SerialEntity>>(listObjDto);
            return Ok(await _unitOfWork._serialRepository.CreateMany(listObj));
        }
    }
}
