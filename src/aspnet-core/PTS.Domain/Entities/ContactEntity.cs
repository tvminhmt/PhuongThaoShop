using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Contact")]
    public class ContactEntity : BaseAuditableEntity
	{
        [Key]
		public int Id { get; set; }
		[Required]
        [EmailAddress(ErrorMessage = "Sai định dạng email!")]
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Message { get; set; }
        public string? CodeManagePost { get; set; }
        public string? Website { get; set; }
		public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int Status { get; set; }
	}
}
