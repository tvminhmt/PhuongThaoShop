namespace PTS.Base.Application.Dto
{
    public class PagedSortedAndFilteredInputDto : PagedAndSortedInputDto
    {
        public string? Filter { get; set; }
    }
}