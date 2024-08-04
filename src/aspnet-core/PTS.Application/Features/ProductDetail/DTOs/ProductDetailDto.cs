using Nest;
using PTS.Application.Common.Mappings;
using PTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PTS.Application.Features.ProductDetail.DTOs
{
    public class ProductDetailDto : IMapFrom<ProductDetailEntity>
    {
        public int Id { get; set; }
        public DateTime? CrDateTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public int? Status { get; set; } = 1;
        [Required]
        [MaxLength(50)]
        public string? Code { get; set; }
        public decimal Price { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public string? Upgrade { get; set; }
        public string? Description { get; set; }
        public int ProductEntityId { get; set; }
        public int? ColorEntityId { get; set; }
        public int? RamEntityId { get; set; }
        public int? CpuEntityId { get; set; }
        public int? HardDriveEntityId { get; set; }
        public int? ScreenEntityId { get; set; }
        public int? CardVGAEntityId { get; set; }
        public int? DiscountId { get; set; }
    }
}
