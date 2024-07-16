namespace MTS.Application.Features.IdentityFeatures.SysFunctions.Queries
{
	public class SysFunctionGetByParentDto : SysFunctionDto
	{
		public string FunctionNameByLevel
		{
			get => GetNameByLevel();
		}

		public string FunctionNameByLevelSelectItemList
		{
			get => GetNameByLevel(true);
		}
	}
}
