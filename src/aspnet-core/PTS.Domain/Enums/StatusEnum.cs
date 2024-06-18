using System.ComponentModel.DataAnnotations;

namespace PTS.Core.Enums
{
	public enum StatusEnum
	{
		[Display(Name = "Đã xóa")]
		Delete = 0,
		[Display(Name = "Hoạt động")]
		Active = 1,
		[Display(Name = "Dừng Hoạt động")]
		InActive = 2,

	}
}
