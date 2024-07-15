using PTS.Application.Common.Mappings;
using PTS.Domain.Entities;
using System.ComponentModel.DataAnnotations;
namespace PTS.Application.Features.Ram.DTOs
{
    public class RamDto : IMapFrom<RamEntity>
    {
        public int Id { get; set; }
        public string Ma { get; set; }
        [MaxLength(100)]
        public string ThongSo { get; set; }
        public int? CrUserId { get; set; }
        public DateTime? CrDateTime { get; set; }
        public int Status { get; set; }
    }
}
