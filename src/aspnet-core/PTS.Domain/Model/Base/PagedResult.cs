
using System.Collections.Generic;
namespace PTS.Domain.Model.Base
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Items { set; get; }

        public PagedResult()
        {
            Items = new List<T>();
        }
    }
}
