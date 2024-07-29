using PTS.Application.Common.Mappings;
using PTS.Domain.Entities;
using System.ComponentModel.DataAnnotations;
namespace PTS.Application.Features.Image.Queries
{
	public class ImageDto : IMapFrom<ImageEntity>
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string? Description { get; set; }
        public int? CrUserId { get; set; }
        public DateTime? CrDateTime { get; set; }
        public int Status { get; set; }
    }
}
