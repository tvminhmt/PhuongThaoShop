using MTS.Application.Common.Mappings;
using MTS.Domain.Entities.Identity;
using System.ComponentModel;

namespace MTS.Application.Features.IdentityFeatures.SysFunctions.Queries
{
	public class SysFunctionDto : IMapFrom<SysFunction>
	{
		public int Id { get; set; }
		public string FunctionName { get; set; }
		public string FunctionDesc { get; set; }
		public int ParentItemId { get; set; }
		public string Url { get; set; }
		public string IconPath { get; set; }
		public int DisplayOrder { get; set; }

		[DisplayName("Hiển thị")]
		public bool IsShow { get; set; }

		[DisplayName("Kích hoạt")]
		public bool IsEnable { get; set; }
		public bool HasChild { get; set; }
		public string CssMenuActive { get; set; }
		public string CssMenuOpen { get; set; }
		public int TreeOrder { get; set; }
		public byte TreeLevel { get; set; }

		public string GetNameByLevel(bool forDDL = false)
		{
			var itemName = FunctionName;
			var textPrefix = forDDL ? "-" : "&nbsp;&nbsp;";
			if (TreeLevel == 1)
			{
				if (!forDDL) itemName = "<b>" + itemName + "</b>";
			}
			else
			{
				textPrefix = string.Join("", Enumerable.Repeat(textPrefix, TreeLevel).ToArray());
				itemName = $"{textPrefix}{itemName}";
			}

			return itemName;
		}
	}
}
