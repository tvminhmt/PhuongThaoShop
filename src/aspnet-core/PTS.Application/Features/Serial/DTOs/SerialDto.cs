using PTS.Application.Common.Mappings;
using PTS.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PTS.Application.Features.Serial.DTOs
{
    public class SerialDto : IMapFrom<SerialEntity>
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public int? CrUserId { get; set; }
        public DateTime? CrDateTime { get; set; }
        public int? UpdUserId { get; set; }
        public DateTime? UpdDateTime { get; set; }
        public int Status { get; set; }
        public int? ProductDetailEntityId { get; set; }
        public int? BillDetailEntityId { get; set; }
    }
}
