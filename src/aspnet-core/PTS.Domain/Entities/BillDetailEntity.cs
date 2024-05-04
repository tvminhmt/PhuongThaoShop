using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("BillDetail")]
    public class BillDetailEntity : BaseEntity
    {
        public string? Code { get; set; }
        public string? CodeProductDetail { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int BillEntityId { get; set; }
        public virtual BillEntity? BillEntity { get; set; }
        public ICollection<SerialEntity>? SerialEntities { get; set; }
    }
}
