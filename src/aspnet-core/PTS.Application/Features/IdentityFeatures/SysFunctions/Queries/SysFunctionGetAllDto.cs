namespace MTS.Application.Features.IdentityFeatures.SysFunctions.Queries
{
    public class SysFunctionGetAllDto : SysFunctionDto
    {
        public string FunctionNameByLevel
        {
            get => GetNameByLevel();
        }

        public string FunctionNameByLevelSelectItemList
        {
            get => GetNameByLevel(true);
        }

        public List<SysFunctionGetAllDto> SysFunctionChildren { get; set; }
	}
}
