using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("CartDetail")]
    public class CartDetailEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime? CreationTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public int? Status { get; set; } = 1;
        public int CartEntityId { get; set; }
        public int ProductDetailEntityId { get; set; }
        public int Quantity { get; set; }
        public virtual CartEntity? CartEntity { get; set; }
        public virtual ProductDetailEntity? ProductDetailEntity { get; set; }


    }
}
