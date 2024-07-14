using PTS.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Application.Dto
{
    public class GetPageBillDto : BaseGetPageRequest
    {
        public string Code { get; set; }
    }
}
