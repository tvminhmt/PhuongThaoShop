using PTS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Domain.Entities
{
    [Table("Discount")]
    public class DiscountEntity : BaseAuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Percentage { get; set; }
        public virtual ICollection<ProductDetailEntity> ProductDetailEntities { get; set; }
    }

}
