using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Cart")]
    public class CartEntity 
    {
        [Key]
        public int UserEntityId { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        public DateTime? CreationTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public int? Status { get; set; } = 1;
        public virtual UserEntity? UserEntity { get; set; }
        public virtual ICollection<CartDetailEntity>? CartDetailEntities { get; set; }
    }
}
