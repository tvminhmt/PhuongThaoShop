using PTS.Application.Common.Mappings;
using PTS.Domain.Entities;
using System.ComponentModel.DataAnnotations;
namespace PTS.Application.Features.Address.DTOs
{
    public class AddressDto : IMapFrom<AddressEntity>
    {
        public int AddressId { get; set; }
        public string? AddressName { get; set; }
        public int? CrUserId { get; set; }
        public DateTime? CrDateTime { get; set; }
        public int Status { get; set; }
        public int? UserEntityId { get; set; }
    }
}
