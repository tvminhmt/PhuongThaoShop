using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Contact")]
    public class ContactEntity : BaseEntity
    {
        [Required]
        [EmailAddress(ErrorMessage = "Sai định dạng email!")]
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Message { get; set; }
        public string? CodeManagePost { get; set; }
        public string? Website { get; set; }
    }
}
