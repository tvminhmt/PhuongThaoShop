using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace PTS.Shared.Dto
{
    public class PagedInputDto : IPagedResultRequest
    {
        [Range(1, 999999)]
        public int MaxResultCount { get; set; } = 999999;

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; } = 0;

        public PagedInputDto()
        {
            MaxResultCount = 10;
        }
    }
}