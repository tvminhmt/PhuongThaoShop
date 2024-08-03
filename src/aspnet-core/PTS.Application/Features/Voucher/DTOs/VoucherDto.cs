using PTS.Application.Common.Mappings;
using PTS.Domain.Entities;
using System.ComponentModel.DataAnnotations;
namespace PTS.Application.Features.Voucher.DTOs
{
    public class VoucherDto : IMapFrom<VoucherEntity>
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string MaVoucher { get; set; }
        [MaxLength(200)]
        public string? TenVoucher { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        public decimal GiaTri { get; set; }
        public int SoLuong { get; set; }
        public int Status { get; set; }
        public DateTime CrDateTime { get; set; }
    }
}
