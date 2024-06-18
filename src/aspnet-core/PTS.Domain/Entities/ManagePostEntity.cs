using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PTS.Domain.Entities
{
    [Table("ManagePost")]
    public class ManagePostEntity : BaseAuditableEntity
	{
		[Key]
		public int Id { get; set; }
		public string? Code { get; set; }
        public string? LinkImage { get; set; }
        public string? Description { get; set; }
		public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int Status { get; set; }
	}
}
