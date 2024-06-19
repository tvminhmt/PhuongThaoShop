using System.ComponentModel;

namespace PTS.Application.DTOs
{
	public record BaseCreateCommand
	{
		[DisplayName("Thêm tiếp dữ liệu khác")]
		public bool AddMoreData { get; set; }
	}
}
