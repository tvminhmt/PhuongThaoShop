namespace PTS.Shared
{
	public class PaginatedResult2<T> : Result<T>
    {
        public PaginatedResult2() { }

		public PaginatedResult2(List<T> data)
        {
            Data = data;
        }

        public PaginatedResult2(bool succeeded, List<T> data = default, List<string> messages = null, int count = 0, int pageNumber = 1, int pageSize = 10)
        {
            Data = data;
            CurrentPage = pageNumber;
            Succeeded = succeeded;
            Messages = messages;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        public new List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int TotalStatus { get; set; }
        public int TotalStatus2 { get; set; }
        public int TotalStatus3 { get; set; }
        public int TotalStatus4 { get; set; }
        public int TotalStatus5 { get; set; }
        public int TotalStatus6 { get; set; }
        public int TotalStatus7 { get; set; }
        public int TotalStatus8 { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public static PaginatedResult2<T> Create(List<T> data, int count, int pageNumber, int pageSize)
        {
            return new PaginatedResult2<T>(true, data, null, count, pageNumber, pageSize);
        }

    }
}
