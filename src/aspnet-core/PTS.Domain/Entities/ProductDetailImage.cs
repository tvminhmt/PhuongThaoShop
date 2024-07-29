using PTS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Domain.Entities
{
    [Table("ProductDetailImage")]
    public class ProductDetailImage : BaseAuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProductDetail")]
        public int ProductDetailId { get; set; }
        public bool IsIndex { get; set; }
        public int Status { get; set; }
        public virtual ProductDetailEntity ProductDetail { get; set; }

        [ForeignKey("Image")]
        public int ImageId { get; set; }
        public virtual ImageEntity Image { get; set; }
    }
}
