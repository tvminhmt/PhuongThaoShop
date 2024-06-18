using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Shared.Dto
{
    public class PagedRequestDto
    {
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }
    }
}
