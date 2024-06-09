using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Core.Dto
{
    public class ContactDto
    {
        public int Id { get; set; } 
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Message { get; set; }
        public string? CodeManagePost { get; set; }
        public string? Website { get; set; }
    }
}
