using PTS.Application.Common.Mappings;
using PTS.Domain.Entities;
using System.ComponentModel.DataAnnotations;
namespace PTS.Application.Features.BillDetail.DTOs
{
    public class BillDetailDto : IMapFrom<BillDetailEntity>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string? CodeProductDetail { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int BillEntityId { get; set; }
        public int? CrUserId { get; set; }
        public DateTime? CrDateTime { get; set; }
        public int Status { get; set; }
    }
}
