using PTS.Application.Common.Mappings;
using PTS.Domain.Entities;
using System.ComponentModel.DataAnnotations;
namespace PTS.Application.Features.Discount.DTOs
{
    public class DiscountDto : IMapFrom<DiscountEntity>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Percentage { get; set; }
    }
}
