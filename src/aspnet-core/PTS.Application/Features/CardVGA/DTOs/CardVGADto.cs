using PTS.Application.Common.Mappings;
using PTS.Domain.Entities;
using System.ComponentModel.DataAnnotations;
namespace PTS.Application.Features.CardVGA.DTOs
{
    public class CardVGADto : IMapFrom<CardVGAEntity>
    {
        public int Id { get; set; }
        public string? Ma { get; set; }
        public string? Ten { get; set; }
        public string? ThongSo { get; set; }
        public int? CrUserId { get; set; }
        public DateTime? CrDateTime { get; set; }
        public int Status { get; set; }
    }
}
