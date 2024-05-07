using Abp.Application.Services.Dto;
using PTS.Shared.Utilities;

namespace PTS.Base.Application.Dto
{
    public class PagedAndSortedInputDto : PagedInputDto, ISortedResultRequest
    {
        public string? Sorting { get; set; }
        public PagedAndSortedInputDto()
        {
            MaxResultCount = 10;
        }
    }

    public class PagedFullInputDto : PagedAndSortedInputDto
    {
        public string? Filter { get; set; }
        public string? FilterFullText => $"%{Filter}%";
        public void Format()
        {
            if (!string.IsNullOrEmpty(this.Filter))
            {
                this.Filter = this.Filter
                    .ToLower()
                    .Trim()
                    .Replace("  ", " ");
                this.Filter = StringUtility.ConvertToUnsign(this.Filter);
            }
        }
    }
}